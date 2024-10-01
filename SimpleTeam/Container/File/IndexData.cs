using SimpleTeam.IO;
using SimpleTeam.Constant;

namespace SimpleTeam.Container.File
{
    public class IndexData
    {
        //////////////////////////////////////////////////
        //
        //Offsets.
        //
        //Key             [long]
        //Data Offset     [int]
        //Subnode Offset  [int]
        //
        //////////////////////////////////////////////////
        //Size.
        public const int SIZE = SizeOf.LONG + 2 * SizeOf.INTEGER;

        //////////////////////////////////////////////////
        //
        //Temporary variables.
        //
        //Offset.
        public long offset;
        //
        //////////////////////////////////////////////////
        //Key of data.
        public long key;
        //Offset of data.
        public long dataOffset;
        //Offset of subnode.
        public long subnodeOffset;

        public IndexData()
        {
            //Set offset.
            offset = -1L;

            //Set key.
            key = -1L;
            //Set data offset.
            dataOffset = -1L;
            //Set subnode offset.
            subnodeOffset = -1L;
        }

        public void ReadFully(FileContainer container, long offset)
        {
            //Create buffer.
            SimpleBuffer buffer = SimpleBuffer.CreateBuffer(SizeType.QQKB);
            //Read partially.
            container.ReadPartially(offset, buffer, SIZE);
		    //Set position.
		    buffer.SetPosition(0);
            //Unwrap data.
            Unwrap(buffer);
        }

        public void WriteFully(FileContainer container, long offset)
        {
            //Create buffer.
            SimpleBuffer buffer = SimpleBuffer.CreateBuffer(SizeType.QQKB);
            //Wrap data.
            Wrap(buffer);
            //Set position.
            buffer.SetPosition(0);
            //Write partially.
            container.WritePartially(offset, buffer, SIZE);
        }

        internal void Wrap(SimpleBuffer buffer)
        {
            //Set key.
            buffer.PutLong(key);
            //Check data offset.
            if (dataOffset < 0)
            {
                //Put value.
                buffer.PutInteger(-1);
            }
            else
            {
                //Set data offset.
                buffer.PutInteger((int)(dataOffset >> 6));
            }
            //Check subnode offset.
            if (subnodeOffset < 0)
            {
                //Put page offset.
                buffer.PutInteger(-1);
            }
            else
            {
                //Put subnode offset.
                buffer.PutInteger((int)(subnodeOffset >> 6));
            }
        }

        internal void Unwrap(SimpleBuffer buffer)
        {
            //Get key.
            key = buffer.GetLong();
            //Get value.
            int value = buffer.GetInteger();
            //Check result.
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
            //Check result.
            if (value < 0)
            {
                //Set subnode offset.
                subnodeOffset = -1L;
            }
            else
            {
                //Set subnode offset.
                subnodeOffset = (long)value << 6;
            }
        }

        internal void CheckValid(long fileSize)
        {
            //Check key.
            if(key< 0 && key != -1)
            {
			    //Throw exception.
			    throw new IOException("invalid key(" + key + ")");
            }
            //Check data offset.
		    if((dataOffset< 0 && dataOffset != -1) ||
                (dataOffset > 0 && (dataOffset & 0x3FL) != 0))
		    {
			    //Throw exception.
			    throw new IOException("invalid data offset(" + dataOffset + ")");
            }
            //Check result.
            if (subnodeOffset > fileSize ||
                (subnodeOffset < 0 && subnodeOffset != -1) ||
                (subnodeOffset > 0 && (subnodeOffset & 0x3FL) != 0))
            {
                //Throw exception.
                throw new IOException("invalid subnode offset(" + subnodeOffset + ")");
            }
        }
    }
}
