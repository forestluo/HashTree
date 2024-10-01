using System.IO.MemoryMappedFiles;

using SimpleTeam.IO;

namespace SimpleTeam.Container.File
{
    public abstract class FileContainer
        : SimpleContainer, IFileContainer
    {
        //Max Size
        public const long MAX_SIZE = 1L << (31 + 6);
        //Default Size Type
        const int DEFAULT_SIZE_TYPE = SizeType._64MB;

        //File info.
        protected FileInfo file;
        //Mapped file.
        protected MemoryMappedFile mappedFile;
        //File accessor.
        protected MemoryMappedViewAccessor accessor;

        //Data size.
        protected long dataSize;
        //File length.
        protected long fileLength;

        //Next pages.
        protected long[] nextPages;
        //Last position.
        protected long lastPosition;
        //Safely closed.
        protected bool safelyClosed;

        //Count of read bytes.
        protected long readCount;
        //Count of written bytes.
        protected long writeCount;

        public FileContainer(FileInfo file)
        {
		    //Create next pages.
		    nextPages = new long[FreePageBuffer.DEFAULT_DATA_PAGE_TYPES];
		    //Initialize value.
		    for(int i = 0;i < nextPages.Length;i ++) nextPages[i] = -1L;

            //Set file.
            this.file = file;
            //Set data size.
            dataSize = 0;
            //Get file length.
            fileLength = file.Exists ?
                file.Length : SizeType.GetRealSize(DEFAULT_SIZE_TYPE);
            //Create memory mapped file.
            mappedFile = MemoryMappedFile.
                CreateFromFile(file.FullName, FileMode.OpenOrCreate, null, fileLength);
            //Create file accessor.
            accessor = mappedFile.CreateViewAccessor(0, fileLength);

            //Preprocess.
            Preprocess();
		    //Check result.
		    if(file.Exists)
		    {
			    //Load head page.
			    LoadHeadPage();
                //Load free page.
                LoadFreePage();
            }
		    else
		    {
			    //Keep head page.
			    KeepHeadPage();
                //Keep free page.
                KeepFreePage();
                //Set safely closed.
                safelyClosed = true;
    		}
	    }

        protected virtual void Preprocess()
        {
            //Get size.
            int size =
                HeadPageBuffer.DEFAULT_SIZE
                + FreePageBuffer.DEFAULT_SIZE;
            //Check file length.
            while(size > fileLength) AppendFile(size);
        }

        protected virtual void CheckReadAction(long position, int size)
        {
            //Check position.
            if(position< 0 || position + size > fileLength)
            {
                throw new IOException("invalid position(" + position + ")");
            }
        }

        protected virtual bool CheckWriteAction(long position, int size)
        {
            //Check position.
            if (position + size <= fileLength) return false;
            //Default size.
            int defaultSize =
                SizeType.GetRealSize(DEFAULT_SIZE_TYPE);
            //Padding.
            int padding = (int)(position + size - fileLength);
            //Check padding.
            padding = (padding / defaultSize + 1) * defaultSize;
            //Append file.
            AppendFile(padding); return true;
        }

        private void AppendFile(int length)
        {
            //Check file length.
            if (fileLength + length > MAX_SIZE)
            {
                throw new IOException("too large(" + fileLength + ") for mapping");
            }
            //Dispose accessor.
            accessor.Dispose();
            //Dispose mapped file.
            mappedFile.Dispose();
            //Create memory mapped file.
            mappedFile = MemoryMappedFile.
                CreateFromFile(file.FullName, FileMode.OpenOrCreate, null, fileLength + length);
            //Create file accessor.
            accessor = mappedFile.CreateViewAccessor(0, fileLength + length);
            //Add length.
            fileLength += length;
        }

        internal long AddPage(PageBuffer pageBuffer)
        {
            //Offset.
            long offset = dataSize;
            //Write buffer.
            WriteFully(dataSize, pageBuffer);
            //Increase size and count.
            IncreaseSizeAndCount();
            //Get real size.
            dataSize += SizeType.GetRealSize(pageBuffer.sizeType);
            //Return result.
            return offset;
        }

        internal long MallocPage(PageDescription description)
        {
            //Get offset.
            long offset = nextPages[description.sizeType - 1];
            //Check result.
            if (offset != -1)
            {
                //Backup page type.
                int pageType = description.pageType;
                //Read page description.
                ReadFully(offset, description);
                //Set next pages.
                nextPages[description.sizeType - 1] = description.nextPage;
                //Set next page.
                description.nextPage = -1;
                //Reset page type.
                description.pageType = pageType;
                //Increase count.
                IncreaseCount();
            }
            //Return offset.
            return offset;
        }

        internal void FreePage(long offset, PageBuffer pageBuffer)
        {
            //Backup page type.
            int pageType = pageBuffer.pageType;
            //Set page type.
            pageBuffer.pageType = PageType.DATA_PAGE;
		    //Set occupied size.
		    pageBuffer.occupiedSize = OccupiedSize.NONE;
		    //Get value.
		    pageBuffer.nextPage = nextPages[pageBuffer.sizeType - 1];
		    //Set next page.
		    nextPages[pageBuffer.sizeType - 1] = offset;
		    //Write page buffer.
		    WriteFully(offset, pageBuffer);
            //Reset page type.
            pageBuffer.pageType = pageType;
		    //Decrease count.
		    DecreaseCount();
        }
        
        internal void FreePage(long offset, PageDescription description)
        {
            //Backup page type.
            int pageType = description.pageType;
            //Set page type.
            description.pageType = PageType.DATA_PAGE;
            //Set occupied size.
            description.occupiedSize = OccupiedSize.NONE;
            //Get value.
            description.nextPage = nextPages[description.sizeType - 1];
            //Set next page.
            nextPages[description.sizeType - 1] = offset;
            //Write description.
            WriteFully(offset, description);
            //Reset page type.
            description.pageType = pageType;
            //Decrease count.
            DecreaseCount();
        }

        private void KeepHeadPage()
        {
            //Create head page buffer.
            HeadPageBuffer buffer = new ();
            //Initialize buffer.
            buffer.Initialize();
            //Set safely closed.
            buffer.safelyClosed = (byte)SafelyClosed.OPENED;
            //Set data size.
            buffer.dataSize = dataSize;
            //Set file length.
            buffer.fileLength = fileLength;
            //Set capacity.
            buffer.capacity = GetCapacity();
            //Set size.
            buffer.size = GetSize();
            //Set count.
            buffer.count = GetCount();
            //Write buffer.
            WriteFully(0, buffer);
            //Add data size.
            dataSize += HeadPageBuffer.DEFAULT_SIZE;
        }

        private void KeepFreePage()
        {
            //Create free page buffer.
            FreePageBuffer buffer = new ();
            //Initialize buffer.
            buffer.Initialize();

            //Set next free pages.
            Array.Copy(nextPages, 0,
                buffer.nextDataPages, 0, FreePageBuffer.DEFAULT_DATA_PAGE_TYPES);

            //Write buffer.
            WriteFully(HeadPageBuffer.DEFAULT_SIZE, buffer);
            //Add data size.
            dataSize += FreePageBuffer.DEFAULT_SIZE;
        }

        private void CloseHeadPage()
        {
            //Create head page buffer.
            HeadPageBuffer buffer = new ();
            //Initialize.
            buffer.Initialize();

            //Set safely closed.
            buffer.safelyClosed = (byte)SafelyClosed.CLOSED;
            //Set data size.
            buffer.dataSize = dataSize;
            //Set file length.
            buffer.fileLength = fileLength;
            //Set capacity.
            buffer.capacity = GetCapacity();
            //Set size.
            buffer.size = GetSize();
            //Set count.
            buffer.count = GetCount();

            //Write buffer.
            WriteFully(0, buffer);
        }

        internal PageBuffer LoadPageBuffer(long position)
        {
            //Create description.
            PageDescription description = new ();
            //Read description.
            ReadFully(position, description);

            //Buffer.
            PageBuffer pageBuffer;
            //Check page type.
            if (description.pageType == PageType.HEAD_PAGE)
            {
                //Create page buffer.
                pageBuffer = new HeadPageBuffer();
            }
            else if (description.pageType == PageType.FREE_PAGE)
            {
                //Create page buffer.
                pageBuffer = new FreePageBuffer();
            }
            else if (description.pageType == PageType.DATA_PAGE)
            {
                //Create page buffer.
                pageBuffer = new DataPageBuffer();
            }
            else if (description.pageType == PageType.QUEUE_PAGE)
            {
                //Create page buffer.
                pageBuffer = new QueuePageBuffer();
            }
            else if (description.pageType == PageType.QUEUE_ELEMENT)
            {
                //Create page buffer.
                pageBuffer = new QueueElementBuffer();
            }
            else if (description.pageType == PageType.INDEX_PAGE)
            {
                //Create page buffer.
                pageBuffer = new IndexPageBuffer();
            }
            else if (description.pageType == PageType.INDEX_ELEMENT)
            {
                //Create page buffer.
                pageBuffer = new IndexElementBuffer();
            }
            else
            {
                //Throw exception.
                throw new IOException("page type(" + description.pageType + ") not supported");
            }

            //Create simple buffer.
            SimpleBuffer buffer = SimpleBuffer.CreateBuffer(description.sizeType);
            //Read partially.
            //Read the left bytes in disk file.
            ReadPartially(position + PageDescription.SIZE, buffer,
                    SizeType.GetRealSize(description.sizeType) - PageDescription.SIZE);
            //Unwrap.
            pageBuffer.Unwrap(buffer, description);
            //Check valid.
            pageBuffer.CheckValid(GetDataSize());
            //Return buffer.
            return pageBuffer;
        }

        private void LoadHeadPage()
        {
            //Load page buffer.
            PageBuffer buffer = LoadPageBuffer(0);
            //Check instance.
            if (buffer.GetType() != typeof(HeadPageBuffer))
            {
                throw new IOException("invalid head page buffer encounted");
            }
            //Get head page buffer.
            HeadPageBuffer pageBuffer = (HeadPageBuffer)buffer;
            //Get safely closed.
            safelyClosed = (pageBuffer.safelyClosed & 0xff) == SafelyClosed.CLOSED;
            //Get data size.
            dataSize = pageBuffer.dataSize;
            //Check result.
            if (pageBuffer.fileLength != fileLength)
            {
                throw new IOException("invalid file length(" + pageBuffer.fileLength + ")");
            }
            //Set capacity.
            SetCapacity(pageBuffer.capacity);
            //Set size.
            SetSize(pageBuffer.size);
            //Get count.
            SetCount(pageBuffer.count);
        }

        private void LoadFreePage()
        {
            //Load page buffer.
            PageBuffer buffer = LoadPageBuffer(HeadPageBuffer.DEFAULT_SIZE);
            //Check instance.
            if (buffer.GetType() != typeof(FreePageBuffer))
            {
                throw new IOException("invalid free page buffer encounted");
            }
            //Get free page buffer.
            FreePageBuffer pageBuffer = (FreePageBuffer)buffer;

            //Copy next pages.
            Array.Copy(pageBuffer.nextDataPages, 0,
                nextPages, 0, FreePageBuffer.DEFAULT_DATA_PAGE_TYPES);

            //Set to initialized status.
            pageBuffer.Initialize();
            //Write page buffer.
            WriteFully(HeadPageBuffer.DEFAULT_SIZE, pageBuffer);
        }

        private void CloseFreePage()
        {
            //Create free page buffer.
            FreePageBuffer buffer = new ();
            //Initialize.
            buffer.Initialize();

            //Set next free pages.
            Array.Copy(nextPages, 0,
                buffer.nextDataPages, 0, FreePageBuffer.DEFAULT_DATA_PAGE_TYPES);

            //Write buffer.
            WriteFully(HeadPageBuffer.DEFAULT_SIZE, buffer);
        }

        public void Close()
        {
            //Close head page.
            CloseHeadPage();
            //Close free page.
            CloseFreePage();

            //Close channel.
            accessor.Dispose();
            //Close access file.
            mappedFile.Dispose();
        }

        public long GetReadCount() { return readCount; }
        public long GetWriteCount() { return writeCount; }
        public bool IsSafelyClosed() { return safelyClosed; }
        public long GetLength() { return fileLength; }
        public long GetDataSize() { return dataSize; }
        public long GetPosition() { return lastPosition; }

        public abstract void SetPosition(long position);

        public abstract int ReadInteger(long position);
        public abstract void WriteInteger(long position, int value);

        public abstract void ReadFully(long position, byte[] bytes);
        public abstract void WriteFully(long position, byte[] bytes);
        public abstract void ReadBytes(long position, byte[] bytes, int offset, int length);
        public abstract void WriteBytes(long position, byte[] bytes, int offset, int length);
        public abstract void ReadFully(long position, SimpleBuffer buffer);
        public abstract void WriteFully(long position, SimpleBuffer buffer);
        public abstract void ReadPartially(long position, SimpleBuffer buffer, int length);
        public abstract void WritePartially(long position, SimpleBuffer buffer, int length);

        public void ReadFully(long position, PageDescription description)
        {
            //Create buffer.
            SimpleBuffer buffer = SimpleBuffer.CreateBuffer(PageDescription.DEFAULT_SIZE_TYPE);
            //Read partially.
            ReadPartially(position, buffer, PageDescription.SIZE);
            //Unwrap.
            description.Unwrap(buffer);
            //Check valid.
            description.CheckValid(GetDataSize());
        }

        public void WriteFully(long position, PageDescription description)
        {
            //Check valid.
            description.CheckValid(GetDataSize());
            //Create buffer.
            SimpleBuffer buffer = SimpleBuffer.CreateBuffer(PageDescription.DEFAULT_SIZE_TYPE);
            //Wrap.
            description.Wrap(buffer);
            //Write bytes.
            WritePartially(position, buffer, PageDescription.SIZE);
        }

        public void ReadFully(long position, PageBuffer pageBuffer)
        {
            //Create buffer.
            SimpleBuffer buffer = SimpleBuffer.CreateBuffer(pageBuffer.sizeType);
		    //Read fully.
		    ReadFully(position, buffer);
            //Unwrap.
            pageBuffer.Unwrap(buffer);
		    //Check valid.
		    pageBuffer.CheckValid(GetDataSize());
	    }

        public void WriteFully(long position, PageBuffer pageBuffer)
        {
            //Check valid.
            pageBuffer.CheckValid(GetDataSize());
		    //Create buffer.
		    SimpleBuffer buffer = SimpleBuffer.CreateBuffer(pageBuffer.sizeType);
            //Wrap.
            pageBuffer.Wrap(buffer);
		    //Write fully.
		    WriteFully(position, buffer);
	    }

        public void ReadPartially(long position, PageBuffer pageBuffer, int length)
        {
            //Create buffer.
            SimpleBuffer buffer = SimpleBuffer.CreateBuffer(pageBuffer.sizeType);
            //Read length.
            ReadPartially(position, buffer, length);
            //Unwrap.
            pageBuffer.Unwrap(buffer);
		    //Check valid.
		    pageBuffer.CheckValid(GetDataSize());
	    }

        public void WritePartially(long position, PageBuffer pageBuffer, int length)
        {
            //Check valid.
            pageBuffer.CheckValid(GetDataSize());
		    //Create buffer.
		    SimpleBuffer buffer = SimpleBuffer.CreateBuffer(pageBuffer.sizeType);
            //Wrap.
            pageBuffer.Wrap(buffer);
		    //Write length.
		    WritePartially(position, buffer, length);
	    }
    }
}
