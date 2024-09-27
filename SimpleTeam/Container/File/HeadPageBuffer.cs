using SimpleTeam.IO;
using SimpleTeam.Constant;

namespace SimpleTeam.Container.File
{
    internal class HeadPageBuffer
        : PageBuffer
    {
        //////////////////////////////////////////////////
        //
        //Offsets.
        //
        //Magic Number    [int]
        //Version         [int]
        //Safely Closed   [byte]
        //Count           [int]
        //Capacity        [int]
        //Size            [int]
        //Data Size       [long]
        //File Length     [long]
        //Copyright       [string]
        //
        //////////////////////////////////////////////////
        //Default Size Type
        public const int DEFAULT_SIZE_TYPE = SizeType.HQKB;
        //Default Size
        static readonly int DEFAULT_SIZE = SizeType.GetRealSize(DEFAULT_SIZE_TYPE);

        //Magic Number
        public int magicNumber;
        //Version
        public int version;
        //Safely Closed
        public byte safelyClosed;
        //Count
        public int count;
        //Capacity
        public int capacity;
        //Size
        public int size;
        //Data Size
        public long dataSize;
        //File Length
        public long fileLength;
        //Copyright
        public string? copyright;

        public HeadPageBuffer()
            : base(PageType.HEAD_PAGE, DEFAULT_SIZE_TYPE)
        {
            
        }

        internal sealed override void Initialize()
        {
            base.Initialize();

            //Set page type.
            pageType = PageType.HEAD_PAGE;
            //Set size type.
            sizeType = DEFAULT_SIZE_TYPE;

            //Set magic number.
            magicNumber = MagicNumber.VALUE;
            //Set version.
            version = Constant.Copywrite.Version.VALUE;
            //Set safely closed.
            safelyClosed = (byte)SafelyClosed.OPENED;
            //Set data size.
            dataSize = 0;
            //Set file length.
            fileLength = 0;
            //Set capacity.
            capacity = IContainer.WITHOUT_LIMIT;
            //Set size.
            size = 0;
            //Set count.
            count = 0;
            //Set copyright.
            copyright = Constant.Copywrite.SimpleTeam.COPYWRITE;
        }

        protected sealed override void Wrap(SimpleBuffer buffer)
        {
            base.Wrap(buffer);

            //Set magic number.
            buffer.PutInteger(magicNumber);
            //Set version number.
            buffer.PutInteger(version);
            //Set safely closed.
            buffer.PutByte(safelyClosed);
            //Set file length.
            buffer.PutLong(fileLength);
            //Set data size.
            buffer.PutLong(dataSize);
            //Set capacity.
            buffer.PutInteger(capacity);
            //Set size.
            buffer.PutInteger(size);
            //Set count.
            buffer.PutInteger(count);
            //Set copyright.
            buffer.PutString(copyright);
        }

        protected sealed override void Unwrap(SimpleBuffer buffer)
        {
            base.Unwrap(buffer);

            //Get magic number.
            magicNumber = buffer.GetInteger();
            //Get version.
            version = buffer.GetInteger();
            //Get safely closed.
            safelyClosed = buffer.GetByte();
            //Get file length.
            fileLength = buffer.GetLong();
            //Get data size.
            dataSize = buffer.GetLong();
            //Get capacity.
            capacity = buffer.GetInteger();
            //Get size.
            size = buffer.GetInteger();
            //Get count.
            count = buffer.GetInteger();
            //Get copyright.
            copyright = buffer.GetString();
        }

        protected sealed override void Unwrap(SimpleBuffer buffer, PageDescription description)
        {
            base.Unwrap(buffer, description);

            //Get magic number.
            magicNumber = buffer.GetInteger();
            //Get version.
            version = buffer.GetInteger();
            //Get safely closed.
            safelyClosed = buffer.GetByte();
            //Get file length.
            fileLength = buffer.GetLong();
            //Get data size.
            dataSize = buffer.GetLong();
            //Get capacity.
            capacity = buffer.GetInteger();
            //Get size.
            size = buffer.GetInteger();
            //Get count.
            count = buffer.GetInteger();
            //Get copyright.
            copyright = buffer.GetString();
        }

        protected sealed override void CheckValid(long fileSize)
        {
            base.CheckValid(fileSize);

		    //Check page type.
		    if(pageType != PageType.HEAD_PAGE)
		    {
			    throw new IOException("invalid head page type(" + pageType + ")");
            }
		    //Check next page.
		    if(nextPage != -1)
		    {
			    //Throw exception.
			    throw new IOException("invalid next page(" + nextPage + ") of head page");
            }
            //Check magic number.
            if (magicNumber != MagicNumber.VALUE)
            {
                //Throw exception.
                throw new IOException("invalid magic number(" + magicNumber + ")");
            }
            //Check version.
            if (version != Constant.Copywrite.Version.VALUE)
            {
                //Throw exception.
                throw new IOException("invalid version(" + version + ")");
            }
            //Check safely closed.
            if (!SafelyClosed.IsValid(safelyClosed & 0xff))
            {
                //Throw exception.
                throw new IOException("invalid safely closed(" + safelyClosed + ")");
            }
            //Check data size.
            if (dataSize < 0 ||
                (dataSize & 0x3FL) != 0)
            {
                //Throw exception.
                throw new IOException("invalid data size(" + dataSize + ")");
            }
            //Check file length.
            if (fileLength < 0 ||
                (fileLength & 0x3FL) != 0)
            {
                //Throw exception.
                throw new IOException("invalid file length(" + fileLength + ")");
            }
            //Check data size and file length.
            if (dataSize > fileLength)
            {
                //Throw exception.
                throw new IOException("invalid data size(" + dataSize + ") or file length(" + fileLength + ")");
            }
            //Check capacity.
            if (capacity < 0 && capacity != IContainer.WITHOUT_LIMIT)
            {
                //Throw exception.
                throw new IOException("invalid capacity(" + capacity + ")");
            }
            //Check size.
            if (size < 0)
            {
                //Throw exception.
                throw new IOException("invalid size(" + size + ")");
            }
            //Check count.
            if (count < 0)
            {
                //Throw exception.
                throw new IOException("invalid count(" + count + ")");
            }
            //Check count and size.
            if (count > size)
            {
                //Throw exception.
                throw new IOException("invalid count(" + count + ") or size(" + size + ")");
            }
            //Check copyright.
            if (string.IsNullOrEmpty(copyright))
            {
                //Throw exception.
                throw new IOException("no copyright");
            }
            else if (!copyright.Contains(Constant.Copywrite.SimpleTeam.SITE))
            {
                //Throw exception.
                throw new IOException("invalid copyright(" + copyright + ")");
            }
	    }
    }
}
