using SimpleTeam.IO;

namespace SimpleTeam.Container.File
{
    public class QueuePageBuffer
        : PageBuffer
    {
        //////////////////////////////////////////////////
        //
        //Offsets.
        //
        //Capacity        [int]
        //Size            [int]
        //Count           [int]
        //Root Position   [int]
        //Read Position   [int]
        //Write Position  [int]
        //
        //////////////////////////////////////////////////
        //Default Size Type
        internal const int DEFAULT_SIZE_TYPE = SizeType.QQKB;
        //Default Size
        static readonly int DEFAULT_SIZE = SizeType.GetRealSize(DEFAULT_SIZE_TYPE);

        //////////////////////////////////////////////////
        //
        //Temporary variables.
        //
        //Offset.
        public long offset;
        //
        //////////////////////////////////////////////////
        //Capacity.
        public int capacity;
        //Size.
        public int size;
        //Count.
        public int count;
        //Root Position
        public long rootPosition;
        //Read Position
        public long readPosition;
        //Write Position
        public long writePosition;

        public QueuePageBuffer()
            : this(DEFAULT_SIZE_TYPE)
        {

        }

        public QueuePageBuffer(int sizeType)
            : base(PageType.QUEUE_PAGE, sizeType)
        {
            
        }

        internal sealed override void Initialize()
        {
            base.Initialize();

            //Set page type.
            pageType = PageType.QUEUE_PAGE;
            //Set size type.
            sizeType = DEFAULT_SIZE_TYPE;

            //Clear offset.
            offset = -1L;

            //Set capacity.
            capacity = IContainer.WITHOUT_LIMIT;
            //Set size.
            size = 0;
            //Set count.
            count = 0;

            //Set root position.
            rootPosition = -1L;
            //Set read position.
            readPosition = -1L;
            //Set write position.
            writePosition = -1L;
        }

        internal sealed override void Wrap(SimpleBuffer buffer)
        {
            base.Wrap(buffer);

            //Set capacity.
            buffer.PutInteger(capacity);
            //Set size.
            buffer.PutInteger(size);
            //Set count.
            buffer.PutInteger(count);
            //Check root position.
            if (rootPosition < 0)
            {
                //Put value.
                buffer.PutInteger(-1);
            }
            else
            {
                //Put value.
                buffer.PutInteger((int)(rootPosition >> 6));
            }
            //Check read position.
            if (readPosition < 0)
            {
                //Put value.
                buffer.PutInteger(-1);
            }
            else
            {
                //Put value.
                buffer.PutInteger((int)(readPosition >> 6));
            }
            //Check write position.
            if (writePosition < 0)
            {
                //Put value.
                buffer.PutInteger(-1);
            }
            else
            {
                //Put value.
                buffer.PutInteger((int)(writePosition >> 6));
            }
        }

        internal sealed override void Unwrap(SimpleBuffer buffer)
        {
            base.Unwrap(buffer);

            //Get capacity.
            capacity = buffer.GetInteger();
            //Get size.
            size = buffer.GetInteger();
            //Get count.
            count = buffer.GetInteger();
            //Get value.
            int value = buffer.GetInteger();
            //Check result.
            if (value < 0)
            {
                //Set root position.
                rootPosition = -1L;
            }
            else
            {
                //Set root position.
                rootPosition = (long)value << 6;
            }
            //Get value.
            value = buffer.GetInteger();
            //Check result.
            if (value < 0)
            {
                //Set read position.
                readPosition = -1L;
            }
            else
            {
                //Set read position.
                readPosition = (long)value << 6;
            }
            //Get value.
            value = buffer.GetInteger();
            //Check result.
            if (value < 0)
            {
                //Set write position.
                writePosition = -1L;
            }
            else
            {
                //Set write position.
                writePosition = (long)value << 6;
            }
        }

        internal sealed override void Unwrap(SimpleBuffer buffer, PageDescription description)
        {
            base.Unwrap(buffer, description);

            //Get capacity.
            capacity = buffer.GetInteger();
            //Get size.
            size = buffer.GetInteger();
            //Get count.
            count = buffer.GetInteger();
            //Get value.
            int value = buffer.GetInteger();
            //Check result.
            if (value < 0)
            {
                //Set root position.
                rootPosition = -1L;
            }
            else
            {
                //Set root position.
                rootPosition = (long)value << 6;
            }
            //Get value.
            value = buffer.GetInteger();
            //Check result.
            if (value < 0)
            {
                //Set read position.
                readPosition = -1L;
            }
            else
            {
                //Set read position.
                readPosition = (long)value << 6;
            }
            //Get value.
            value = buffer.GetInteger();
            //Check result.
            if (value < 0)
            {
                //Set write position.
                writePosition = -1L;
            }
            else
            {
                //Set write position.
                writePosition = (long)value << 6;
            }
        }

        internal sealed override void CheckValid(long fileSize)
        {
            base.CheckValid(fileSize);

            //Check occupied size.
            if (occupiedSize == OccupiedSize.FULL || occupiedSize > 0)
            {
                //Check page type.
                if (pageType != PageType.QUEUE_PAGE)
                {
                    throw new IOException("invalid queue page type(" + pageType + ")");
                }
                //Check next page.
                if (nextPage != -1)
                {
                    //Throw exception.
                    throw new IOException("invalid next page(" + nextPage + ") of queue page");
                }
            }
            else
            {
                //Check next page.
                if ((nextPage < 0 && nextPage != -1) || nextPage > fileSize)
                {
                    //Throw exception.
                    throw new IOException("invalid next page(" + nextPage + ") of queue page");
                }
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
            //Check result.
            if (rootPosition > fileSize ||
                (rootPosition < 0 && rootPosition != -1) ||
                (rootPosition > 0 && (rootPosition & 0x3FL) != 0))
            {
                //Throw exception.
                throw new IOException("invalid root position(" + rootPosition + ")");
            }
            //Check result.
            if (readPosition > fileSize ||
                (readPosition < 0 && readPosition != -1) ||
                (readPosition > 0 && (readPosition & 0x3FL) != 0))
            {
                //Throw exception.
                throw new IOException("invalid read position(" + readPosition + ")");
            }
            //Check result.
            if (writePosition > fileSize ||
                (writePosition < 0 && writePosition != -1) ||
                (writePosition > 0 && (writePosition & 0x3FL) != 0))
            {
                //Throw exception.
                throw new IOException("invalid write position(" + writePosition + ")");
            }
        }
    }
}
