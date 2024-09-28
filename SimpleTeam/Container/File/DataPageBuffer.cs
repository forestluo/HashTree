using SimpleTeam.IO;

namespace SimpleTeam.Container.File
{
    public class DataPageBuffer
        : PageBuffer
    {
        //////////////////////////////////////////////////
        //
        //Offsets.
        //
        //Data Bytes      [bytes]
        //
        //////////////////////////////////////////////////
        //Default Size Type
        const int DEFAULT_SIZE_TYPE = SizeType.QQKB;
        //Minimum Data Page Size
        //static readonly int MINIMUM_DATA_PAGE_SIZE = MINIMUM_PAGE_SIZE + SizeOf.BYTE + SizeOf.INTEGER;

        //Reference of bytes.
        public byte[]? bytes;

        public DataPageBuffer()
            : this(DEFAULT_SIZE_TYPE)
        {
            
        }

        public DataPageBuffer(int sizeType)
            : base(PageType.DATA_PAGE, sizeType)
        {

        }

        internal sealed override void Initialize()
        {
            base.Initialize();

            //Set page type.
            pageType = PageType.DATA_PAGE;
            //Set size type.
            sizeType = DEFAULT_SIZE_TYPE;
        }

        internal sealed override void Wrap(SimpleBuffer buffer)
        {
            base.Wrap(buffer);

            //Check bytes.
            if (bytes != null && bytes.Length > 0) buffer.PutBytes(bytes);
        }

        internal sealed override void Unwrap(SimpleBuffer buffer)
        {
            base.Unwrap(buffer);

            //Check bytes.
            if (bytes != null && bytes.Length > 0) buffer.GetBytes(bytes);
        }

        internal sealed override void Unwrap(SimpleBuffer buffer, PageDescription description)
        {
            base.Unwrap(buffer, description);

            //Check byts.
            if (bytes != null && bytes.Length > 0) buffer.GetBytes(bytes);
        }

        internal sealed override void CheckValid(long fileSize)
        {
            base.CheckValid(fileSize);

		    //Check occupied size.
		    if(occupiedSize == OccupiedSize.FULL || occupiedSize > 0)
		    {
			    //Check page type.
			    if(pageType != PageType.DATA_PAGE)
			    {
				    throw new IOException("invalid data page type(" + pageType + ")");
                }
			    //Check next page.
			    if(nextPage != -1)
			    {
				    //Throw exception.
				    throw new IOException("invalid next page(" + nextPage + ") of data page");
                }
		    }
            else
            {
                //Check next page.
                if ((nextPage < 0 && nextPage != -1) || nextPage > fileSize)
                {
                    //Throw exception.
                    throw new IOException("invalid next page(" + nextPage + ") of data page");
                }
            }
	    }
    }
}
