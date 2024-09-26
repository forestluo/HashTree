using System.Text;

using SimpleTeam.IO;
using SimpleTeam.Log;
using SimpleTeam.Constant;
using SimpleTeam.Function;

namespace SimpleTeam.Container.File
{
    internal class PageDescription
        : IDump
    {
        //Size Of Description
        public const int SIZE = 10 * SizeOf.BYTE;
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
        //Default Size Type
        const int DEFAULT_SIZE_TYPE = SizeType.QQKB;

        //Page Type
        public int pageType;
        //Size Type
        public int sizeType;
        //Occupied Size
        public int occupiedSize;
        //Next Page
        public long nextPage;

        public PageDescription()
        {

        }

        public void Wrap(SimpleBuffer buffer)
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
                //Set next page.
                buffer.PutInteger((int)(nextPage >> 6));
            }
        }

        public void Unwrap(SimpleBuffer buffer)
        {
            //Set position.
            buffer.SetPosition(0);
            //Get page type.
            pageType = buffer.GetByte() & 0xff;
            //Get size type.
            sizeType = buffer.GetByte() & 0xff;
            //Get occupied size.
            occupiedSize = buffer.GetInteger();
            //Get next page.
            int value = buffer.GetInteger();
            //Check result.
            if (value < 0)
            {
                //Set next page.
                nextPage = -1L;
            }
            else
            {
                //Set next page.
                nextPage = (long)value << 6;
            }
        }

        public void CheckValid(long fileSize)
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
            if ((occupiedSize + SIZE > SizeType.GetRealSize(sizeType)) ||
                (occupiedSize < 0 && occupiedSize != OccupiedSize.FULL))
            {
                throw new IOException("invalid occupied size(" + occupiedSize + ")");
            }
    	}

        public void Dump()
        {
            //Create buffer.
            StringBuilder buffer = new ();
            //Append.
            buffer.Append("PageDescription.dump : show parameters !").AppendLine().
                Append("\tpageType = ").Append(pageType).AppendLine().
                Append("\tpageSize = ").Append(sizeType).AppendLine().
                Append("\toccupiedSize = ").Append(occupiedSize).AppendLine().
                Append("\tnextPage = ").Append(HexFormat.ToString(nextPage)).AppendLine();
            //Dump.
            Console.Write(buffer.ToString());
        }
    }
}
