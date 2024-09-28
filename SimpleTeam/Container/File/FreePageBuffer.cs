using SimpleTeam.IO;
using System.Security.AccessControl;

namespace SimpleTeam.Container.File
{
    public class FreePageBuffer
        : PageBuffer
    {
        //Default Data Page Types
        internal const int DEFAULT_DATA_PAGE_TYPES = SizeType.TOTAL_TYPES;
        //////////////////////////////////////////////////
        //
        //Offsets.
        //
        //Page(s) Offset  [N * long] (N = 18)
        //
        //////////////////////////////////////////////////
        //Default Size Type
        internal const int DEFAULT_SIZE_TYPE = SizeType.HQKB;
        //Default Size
        internal static readonly int DEFAULT_SIZE = SizeType.GetRealSize(DEFAULT_SIZE_TYPE);

        //Next Data Pages
        public long[] nextDataPages;

        public FreePageBuffer()
            : base(PageType.FREE_PAGE, DEFAULT_SIZE_TYPE)
        {
            //Create next data pages.
            nextDataPages = new long[DEFAULT_DATA_PAGE_TYPES];
        }

        internal sealed override void Initialize()
        {
            base.Initialize();

            //Set page type.
            pageType = PageType.FREE_PAGE;
            //Set size type.
            sizeType = DEFAULT_SIZE_TYPE;

            //Set next data pages.
            for (int i = 0; i < DEFAULT_DATA_PAGE_TYPES; i++) nextDataPages[i] = -1L;
        }

        internal sealed override void Wrap(SimpleBuffer buffer)
        {
            base.Wrap(buffer);

            //Set next data pages.
            for (int i = 0; i < DEFAULT_DATA_PAGE_TYPES; i++)
            {
                //Put value.
                buffer.PutInteger((int)(nextDataPages[i] >> 6));
            }
        }

        internal sealed override void Unwrap(SimpleBuffer buffer)
        {
            base.Unwrap(buffer);

            //Get next data pages.
            for (int i = 0;
                i < DEFAULT_DATA_PAGE_TYPES; i++)
            {
                //Get value.
                int value = buffer.GetInteger();
                //Check result.
                if (value < 0)
                {
                    //Set next data page.
                    nextDataPages[i] = -1L;
                }
                else
                {
                    //Set next data page.
                    nextDataPages[i] = (long)value << 6;
                }
            }
        }

        internal sealed override void Unwrap(SimpleBuffer buffer, PageDescription description)
        {
            base.Unwrap(buffer, description);

            //Get next data pages.
            for (int i = 0;
                i < DEFAULT_DATA_PAGE_TYPES; i++)
            {
                //Get value.
                int value = buffer.GetInteger();
                //Check result.
                if (value < 0)
                {
                    //Set next data page.
                    nextDataPages[i] = -1L;
                }
                else
                {
                    //Set next data page.
                    nextDataPages[i] = (long)value << 6;
                }
            }
        }

        internal sealed override void CheckValid(long fileSize)
        {
            base.CheckValid(fileSize);

            //Check page type.
            if (pageType != PageType.FREE_PAGE)
            {
                throw new IOException("invalid free page type(" + pageType + ")");
            }
            //Check next page.
            if (nextPage != -1)
            {
                //Throw exception.
                throw new IOException("no next page(" + nextPage + ") was allowed for free page");
            }
            //Do while.
            for (int i = 0; i < nextDataPages.Length; i++)
            {
                //Get next page.
                long value = nextDataPages[i];
                //Check result.
                if (value > fileSize ||
                   (value < 0 && value != -1) ||
                   (value > 0 && (value & 0x3FL) != 0))
                {
                    //Throw exception.
                    throw new IOException("invalid next free data page offset(" + value + ")");
                }
            }
        }
    }
}
