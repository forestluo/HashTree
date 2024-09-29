using SimpleTeam.IO;

namespace SimpleTeam.Container.File
{
    public class QueueElementBuffer
        : PageBuffer
    {
        //////////////////////////////////////////////////
        //
        //Offsets.
        //
        //Data Offset     [int]
        //Next Element    [int]
        //Page Offset     [int]
        //
        //////////////////////////////////////////////////
        //Default Size Type
        internal const int DEFAULT_SIZE_TYPE = SizeType.QQKB;
        //Default Size
        static readonly int DEFAULT_SIZE = SizeType.GetRealSize(DEFAULT_SIZE_TYPE);

        //Next Element
        public long nextElement;
        //Data Offset
        public long dataOffset;
        //Page Offset
        public long pageOffset;

        public QueueElementBuffer()
            : this(DEFAULT_SIZE_TYPE)
        {
            
        }

        public QueueElementBuffer(int sizeType)
            : base(PageType.QUEUE_ELEMENT, sizeType)
        {
            
        }

        internal sealed override void Initialize()
        {
            base.Initialize();

            //Set page type.
            pageType = PageType.QUEUE_ELEMENT;
            //Set size type.
            sizeType = DEFAULT_SIZE_TYPE;

            //Set data offset.
            dataOffset = -1L;
            //Set next element.
            nextElement = -1L;
            //Set page offset.
            pageOffset = -1L;
        }

        internal sealed override void Wrap(SimpleBuffer buffer)
        {
            base.Wrap(buffer);

            //Check data offset.
            if (dataOffset < 0)
            {
                //Put value.
                buffer.PutInteger(-1);
            }
            else
            {
                //Put value.
                buffer.PutInteger((int)(dataOffset >> 6));
            }
            //Check next element.
            if (nextElement < 0)
            {
                //Put value.
                buffer.PutInteger(-1);
            }
            else
            {
                //Put value.
                buffer.PutInteger((int)(nextElement >> 6));
            }
            //Check page offset.
            if (pageOffset < 0)
            {
                //Put value.
                buffer.PutInteger(-1);
            }
            else
            {
                //Put value.
                buffer.PutInteger((int)(pageOffset >> 6));
            }
        }

        internal sealed override void Unwrap(SimpleBuffer buffer)
        {
            base.Unwrap(buffer);

            //Get value.
            int value = buffer.GetInteger();
            //Check value.
            if (value < 0)
            {
                //Set data offset.
                dataOffset = -1L;
            }
            else
            {
                //Set data offset.
                dataOffset = (long)value << 6;
            }
            //Get value.
            value = buffer.GetInteger();
            //Check value.
            if (value < 0)
            {
                //Set next element.
                nextElement = -1L;
            }
            else
            {
                //Set next element.
                nextElement = (long)value << 6;
            }
            //Get value.
            value = buffer.GetInteger();
            //Check value.
            if (value < 0)
            {
                //Set page offset.
                pageOffset = -1L;
            }
            else
            {
                //Set page offset.
                pageOffset = (long)value << 6;
            }
        }

        internal sealed override void Unwrap(SimpleBuffer buffer, PageDescription description)
        {
            base.Unwrap(buffer, description);

            //Get value.
            int value = buffer.GetInteger();
            //Check value.
            if (value < 0)
            {
                //Set data offset.
                dataOffset = -1L;
            }
            else
            {
                //Set data offset.
                dataOffset = (long)value << 6;
            }
            //Get value.
            value = buffer.GetInteger();
            //Check value.
            if (value < 0)
            {
                //Set next element.
                nextElement = -1L;
            }
            else
            {
                //Set next element.
                nextElement = (long)value << 6;
            }
            //Get value.
            value = buffer.GetInteger();
            //Check value.
            if (value < 0)
            {
                //Set page offset.
                pageOffset = -1L;
            }
            else
            {
                //Set page offset.
                pageOffset = (long)value << 6;
            }
        }

        internal sealed override void CheckValid(long fileSize)
        {
            base.CheckValid(fileSize);

            //Check occupied size.
            if (occupiedSize == OccupiedSize.FULL || occupiedSize > 0)
            {
                //Check page type.
                if (pageType != PageType.QUEUE_ELEMENT)
                {
                    throw new IOException("not a valid queue element type(" + pageType + ")");
                }
                //Check next page.
                if (nextPage != -1)
                {
                    //Throw exception.
                    throw new IOException("invalid next page(" + nextPage + ") of queue element");
                }
            }
            else
            {
                //Check next page.
                if ((nextPage < 0 && nextPage != -1) || nextPage > fileSize)
                {
                    //Throw exception.
                    throw new IOException("invalid next page(" + nextPage + ") of queue element");
                }
            }
            //Check result.
            if ((dataOffset < 0 && dataOffset != -1) ||
                (dataOffset > 0 && (dataOffset & 0x3FL) != 0))
            {
                //Throw exception.
                throw new IOException("invalid data offset(" + dataOffset + ")");
            }
            //Check result.
            if (pageOffset > fileSize ||
                (pageOffset < 0 && pageOffset != -1) ||
                (pageOffset > 0 && (pageOffset & 0x3FL) != 0))
            {
                //Throw exception.
                throw new IOException("invalid page offset(" + pageOffset + ")");
            }
            //Check result.
            if (nextElement > fileSize ||
                (nextElement < 0 && nextElement != -1) ||
                (nextElement > 0 && (nextElement & 0x3FL) != 0))
            {
                //Throw exception.
                throw new IOException("invalid next element(" + nextElement + ")");
            }
        }
    }
}
