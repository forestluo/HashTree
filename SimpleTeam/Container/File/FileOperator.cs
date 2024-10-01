namespace SimpleTeam.Container.File
{
    public class FileOperator
    {
        //File container.
        protected FileContainer container;

        public FileOperator(FileContainer container)
        {
            //Set container.
            this.container = container;
        }

        public virtual void Flush() { }

        public virtual void Close() { Flush(); }

        protected long AddPage(PageBuffer pageBuffer)
        {
            //Return result.
            return container.AddPage(pageBuffer);
        }

        protected long MallocPage(int pageType, int sizeType)
        {
            //Create page description.
            PageDescription description = new ();
            //Set page type.
            description.pageType = pageType;
            //Set size type.
            description.sizeType = sizeType;
            //Malloc page.
            long offset = MallocPage(description);
            //Return offset.
            return offset;
        }

        protected long MallocPage(PageDescription description)
        {
            //Return result.
            return container.MallocPage(description);
        }

        protected void FreePage(long offset, int sizeType)
        {
            //Create page description.
            PageDescription description = new ();
            //Set page type.
            description.pageType = PageType.DATA_PAGE;
            //Set size type.
            description.sizeType = sizeType;
            //Free page.
            FreePage(offset, description);
        }

        protected void FreePage(long offset, PageBuffer pageBuffer)
        {
            //Free page.
            container.FreePage(offset, pageBuffer);
        }

        protected void FreePage(long offset, PageDescription description)
        {
            //Free page.
            container.FreePage(offset, description);
        }

        public long GetLength()
        {
            //Return result.
            return container.GetLength();
        }

        public long GetDataSize()
        {
            //Return result.
            return container.GetDataSize();
        }

        public void ReadFully(long position, byte[] bytes)
        {
            //Read fully.
            container.ReadFully(position, bytes);
        }

        public void WriteFully(long position, byte[] bytes)
        {
            //Write fully.
            container.WriteFully(position, bytes);
        }

        public void ReadBytes(long position, byte[] bytes, int offset, int length)
        {
            //Read bytes.
            container.ReadBytes(position, bytes, offset, length);
	    }

        public void WriteBytes(long position, byte[] bytes, int offset, int length)
        {
            //Write bytes.
            container.WriteBytes(position, bytes, offset, length);
	    }

        public void ReadFully(long position, PageDescription description)
        {
            //Read fully.
            container.ReadFully(position, description);
	    }

        public void WriteFully(long position, PageDescription description)
        {
            //Write fully.
            container.WriteFully(position, description);
	    }

        public void ReadFully(long position, PageBuffer pageBuffer)
        {
            //Read fully.
            container.ReadFully(position, pageBuffer);
	    }

        public void WriteFully(long position, PageBuffer pageBuffer)
        {
            //Write fully.
            container.WriteFully(position, pageBuffer);
	    }

        public void ReadPartially(long position, PageBuffer pageBuffer, int length)
        {
            //Read bytes.
            container.ReadPartially(position, pageBuffer, length);
	    }

        public void WritePartially(long position, PageBuffer pageBuffer, int length)
        {
            //Write bytes.
            container.WritePartially(position, pageBuffer, length);
	    }
    }
}
