using SimpleTeam.IO;

namespace SimpleTeam.Container.File
{
    public abstract class PageBuffer
    {
        //////////////////////////////////////////////////
        //
        //Offsets.
        //
        //Page Type       [byte]
        //Size Type       [byte]
        //Occupied Size   [int]
        //Next Page       [int]
        //
        //////////////////////////////////////////////////
        //Minimum Page Size
        internal const int MINIMUM_PAGE_SIZE = PageDescription.SIZE;

        //Page Type
        public int pageType;
        //Size Type
        public int sizeType;
        //Occupied Size
        public int occupiedSize;
        //Next Page
        public long nextPage;

        public PageBuffer(int sizeType)
        {
            //Set size type.
            this.sizeType = sizeType;
        }

        protected PageBuffer(int pageType, int sizeType)
        {
            //Set page type.
            this.pageType = pageType;
            //Set size type.
            this.sizeType = sizeType;
        }

        internal virtual void Initialize()
        {
            //Set next page.
            nextPage = -1L;
            //Set occupied size.
            occupiedSize = OccupiedSize.FULL;
        }

        internal virtual void Wrap(SimpleBuffer buffer)
        {
            //Set position.
            buffer.SetPosition(0);

            //Set page type.
            buffer.PutByte((byte)pageType);
            //Set size type.
            buffer.PutByte((byte)sizeType);
            //Set occupied size.
            buffer.PutInteger(occupiedSize);
            //Check next page.
            if (nextPage < 0)
            {
                //Put value.
                buffer.PutInteger(-1);
            }
            else
            {
                //Put value.
                buffer.PutInteger((int)(nextPage >> 6));
            }
        }

        internal virtual void Unwrap(SimpleBuffer buffer)
        {
            //Set position.
            buffer.SetPosition(0);

            //Get page type.
            pageType = buffer.GetByte() & 0xff;
            //Get size type.
            sizeType = buffer.GetByte() & 0xff;
            //Get occupied size.
            occupiedSize = buffer.GetInteger();
            //Get value.
            int value = buffer.GetInteger();
            //Check result.
            if (value < 0)
            {
                //Set next page.
                nextPage = -1L;
            }
            else
            {
                //Get next page.
                nextPage = (long)value << 6;
            }
        }

        internal virtual void Unwrap(SimpleBuffer buffer, PageDescription description)
        {
            //Set position.
            buffer.SetPosition(0);

            //Get page type.
            pageType = description.pageType;
            //Get size type.
            sizeType = description.sizeType;
            //Get occupied size.
            occupiedSize = description.occupiedSize;
            //Get next page.
            nextPage = description.nextPage;
        }

        internal virtual void CheckValid(long fileSize)
        {
    		//Check page type.
	    	if(!PageType.IsValid(pageType))
		    {
			    throw new IOException("invalid page type(" + pageType + ")");
            }
		    //Check size type.
		    if(!SizeType.IsValid(sizeType))
		    {
			    throw new IOException("invalid size type(" + sizeType + ")");
            }
            //Check size type.
            if (pageType == PageType.HEAD_PAGE)
            {
                //Check size type.
                if (sizeType != HeadPageBuffer.DEFAULT_SIZE_TYPE)
                {
                    throw new IOException("invalid size type(" + sizeType + ")");
                }
            }
            else if (pageType == PageType.FREE_PAGE)
            {
                //Check size type.
                if (sizeType != FreePageBuffer.DEFAULT_SIZE_TYPE)
                {
                    throw new IOException("invalid size type(" + sizeType + ")");
                }
            }
            //Check next page.
            if (nextPage > fileSize ||
                (nextPage < 0 && nextPage != -1) ||
                (nextPage > 0 && (nextPage & 0x3FL) != 0))
            {
                throw new IOException("invalid next page(" + nextPage + ")");
            }
            //Check occupied size.
            if ((occupiedSize < 0 && occupiedSize != OccupiedSize.FULL) ||
                (occupiedSize + PageDescription.SIZE > SizeType.GetRealSize(sizeType)))
            {
                throw new IOException("invalid occupied size(" + occupiedSize + ")");
            }
    	}
    }
}
