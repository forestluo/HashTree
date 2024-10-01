using SimpleTeam.Constant;
using SimpleTeam.IO;

namespace SimpleTeam.Container.File
{
    internal class MappedFileContainer
        : FileContainer
    {
        public MappedFileContainer(FileInfo file)
            : base(file)
        {

        }

        protected sealed override void Preprocess()
        {
            base.Preprocess();

            //Check file length.
            if (fileLength > MAX_SIZE)
            {
                throw new IOException("too large(" + fileLength + ") for mapping");
            }
        }

        public sealed override void SetPosition(long position)
        {
            //Check position.
            if (position < 0 || position > MAX_SIZE)
            {
                throw new IOException("invalid position(" + position + ")");
            }
            //Check position.
            if (position != lastPosition)
            {
                //Set last position.
                lastPosition = position;
                //Check position.
                if (lastPosition > dataSize)
                {
                    throw new IOException("invalid last position(" + lastPosition + ")");
                }
            }
        }

        public sealed override int ReadInteger(long position)
        {
            //Check read action.
            CheckReadAction(position, SizeOf.INTEGER);
            //Set position.
            SetPosition(position);
            //Read value.
            int value = accessor.ReadInt32(position);
            //Add read count.
            readCount += SizeOf.INTEGER;
            //Add position.
            lastPosition += SizeOf.INTEGER;
            //Return result.
            return value;
        }

        public sealed override void WriteInteger(long position, int value)
        {
            //Check write action.
            CheckWriteAction(position, SizeOf.INTEGER);
            //Set position.
            SetPosition(position);
            //Write value.
            accessor.Write(position, value);
            //Add write count.
            writeCount += SizeOf.INTEGER;
            //Add position.
            lastPosition += SizeOf.INTEGER;
        }

        public sealed override void ReadFully(long position, byte[] bytes)
        {
            //Check read action.
            CheckReadAction(position, bytes.Length);
            //Set position.
            SetPosition(position);
            //Read fully.
            accessor.ReadArray(position, bytes, 0, bytes.Length);
            //Add read count.
            readCount += bytes.Length;
            //Add position.
            lastPosition += bytes.Length;
        }

        public sealed override void WriteFully(long position, byte[] bytes)
        {
            //Check write action.
            CheckWriteAction(position, bytes.Length);
            //Set position.
            SetPosition(position);
            //Put bytes.
            accessor.WriteArray(position, bytes, 0, bytes.Length);
            //Add written count.
            writeCount += bytes.Length;
            //Add position.
            lastPosition += bytes.Length;
        }

        public sealed override void ReadBytes(long position, byte[] bytes, int offset, int length)
        {
            //Check read action.
            CheckReadAction(position, length);
    		//Set position.
	    	SetPosition(position);
            //Read bytes.
            accessor.ReadArray(position, bytes, offset, length);
	    	//Add read count.
		    readCount += length;
    		//Add position.
	    	lastPosition += length;
	    }

        public sealed override void WriteBytes(long position, byte[] bytes, int offset, int length)
        {
            //Check write actioin.
            CheckWriteAction(position, length);
            //Set position.
            SetPosition(position);
            //Write bytes.
            accessor.WriteArray(position, bytes, offset, length);
            //Add written count.
            writeCount += length;
            //Add position.
            lastPosition += length;
        }

        public sealed override void ReadFully(long position, SimpleBuffer buffer)
        {
            //Check read action.
            CheckReadAction(position, buffer.GetSize());
            //Set position.
            SetPosition(position);
            //Clear.
            buffer.Clear();
            //Read buffer.
            int size = accessor.ReadArray(position, buffer.GetBytes(), 0, buffer.GetSize());       
            //Add read count.
            readCount += size;
            //Add position.
            lastPosition += size;
        }

        public sealed override void WriteFully(long position, SimpleBuffer buffer)
        {
            //Check write action.
            CheckWriteAction(position, buffer.GetSize());
            //Set position.
            SetPosition(position);
            //Set position.
            buffer.SetPosition(0);
            //Write buffer.
            accessor.WriteArray(position, buffer.GetBytes(), 0, buffer.GetSize());
            //Get size.
            int size = buffer.GetSize();
            //Add write count.
            writeCount += size;
            //Add position.
            lastPosition += size;
        }

        public sealed override void ReadPartially(long position, SimpleBuffer buffer, int length)
        {
            //Check read action.
            CheckReadAction(position, length);
            //Set position.
            SetPosition(position);
            //Clear.
            buffer.Clear();
            //Read buffer.
            int size = accessor.ReadArray(position, buffer.GetBytes(), 0, length);
            //Add read count.
            readCount += size;
            //Add position.
            lastPosition += size;
        }

        public sealed override void WritePartially(long position, SimpleBuffer buffer, int length)
        {
            //Check write action.
            CheckWriteAction(position, length);
            //Set position.
            SetPosition(position);
            //Set position.
            buffer.SetPosition(0);
            //Write buffer.
            accessor.WriteArray(position, buffer.GetBytes(), 0, length);
            //Get size.
            int size = buffer.GetSize();
            //Add write count.
            writeCount += size;
            //Add position.
            lastPosition += size;
        }
    }
}