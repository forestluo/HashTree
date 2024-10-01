using SimpleTeam.IO;
using SimpleTeam.Constant;

namespace SimpleTeam.Container.File
{
    public class IndexElementBuffer
        : PageBuffer
    {
        //////////////////////////////////////////////////
        //
        //Offsets.
        //
        //Hash Datas    [N * IndexData]
        //
        //////////////////////////////////////////////////
        //Default Size Type
        //The sizes of element buffer are limited as HQKB, QKB, HKB, 1KB...
        internal const int DEFAULT_SIZE_TYPE = SizeType.HQKB;
        //Default Size
        static readonly int DEFAULT_SIZE = SizeType.GetRealSize(DEFAULT_SIZE_TYPE);

        //////////////////////////////////////////////////
        //
        //Temporary variables.
        //
        //Flag.
        public int flag;
        //Level.
        public int level;
        //Index.
        public int index;
        //Offset.
        public long offset;
        //
        //////////////////////////////////////////////////
        //Page offset.
        public long pageOffset;
        //Index Datas
        public IndexData[]? datas;

        public IndexElementBuffer()
            : this(DEFAULT_SIZE_TYPE)
        {
            
        }

        public IndexElementBuffer(int sizeType)
            : base(PageType.INDEX_ELEMENT, sizeType)
        {
            
        }

        internal sealed override void Initialize()
        {
            base.Initialize();

            //Set page type.
            pageType = PageType.INDEX_ELEMENT;
            //Set size type.
            sizeType = DEFAULT_SIZE_TYPE;

            //Set flag.
            flag = 0x00;
            //Set level.
            level = -1;
            //Set index.
            index = -1;
            //Set offset.
            offset = -1L;

            //Set page offset.
            pageOffset = -1L;
            //Clear datas.
            datas = null;
        }

        internal bool HasSubnode()
        {
            //Check datas.
            if (datas == null) return false;
            //Check every data.
            for (int i = 0; i < datas.Length; i++)
            {
                //Check offset.
                if (datas[i].subnodeOffset != -1L) return true;
            }
            //Return false.
            return false;
        }

        internal int GetSubnodeCount()
        {
            //Return result.
            return GetSubnodeCount(sizeType);
        }

        internal int GetIndexByKey(long key)
        {
            //Return result.
            return GetSubnodeIndex(sizeType, key);
        }

        internal long GetOffsetByIndex(int index)
        {
            //Return result.
            return offset + PageDescription.SIZE +
                SizeOf.INTEGER + index * IndexData.SIZE;
        }

        internal IndexData? GetDataByKey(long key)
        {
            //Return result.
            return datas?[GetIndexByKey(key)];
        }

        internal static int GetSubnodeCount(int sizeType)
        {
            //Check value.
            switch (sizeType)
            {
                case SizeType.QQKB:
                    break;
                case SizeType.HQKB:
                    return 7;
                case SizeType.QKB:
                    return 15;//3*5
                case SizeType.HKB:
                    return 31;
                case SizeType._1KB:
                    return 61;
                case SizeType._2KB:
                    return 127;
                case SizeType._4KB:
                    return 251;
                case SizeType._8KB:
                    return 509;
                case SizeType._16KB:
                    return 1021;
                case SizeType._32KB:
                    return 2039;
                case SizeType._64KB:
                    return 4093;
                //default:;
            }
            //Return -1.
            return -1;
        }

        internal static int GetSubnodeIndex(int sizeType, long key)
        {
            //Get index.
            return (int)(key % GetSubnodeCount(sizeType));
        }

        internal static long GetSubnodeOffset(int sizeType, long key)
        {
            //Return result.
            return PageDescription.SIZE + SizeOf.INTEGER +
                GetSubnodeIndex(sizeType, key) * IndexData.SIZE;
        }

        internal sealed override void Wrap(SimpleBuffer buffer)
        {
            base.Wrap(buffer);

            //Check page offset.
            if (pageOffset < 0)
            {
                //Put page offset.
                buffer.PutInteger(-1);
            }
            else
            {
                //Put page offset.
                buffer.PutInteger((int)(pageOffset >> 6));
            }
            //Get subnode count.
            int subnodeCount = GetSubnodeCount();
            //Check datas.
            if (datas == null || datas.Length <= 0)
            {
                //Do while.
                for (int i = 0; i < subnodeCount; i++)
                {
                    //Put key.
                    buffer.PutLong(-1L);
                    //Put data offset and subnode offset.
                    buffer.PutInteger(-1); buffer.PutInteger(-1);
                }
            }
            else
            {
                //Do while.
                for (int i = 0; i < subnodeCount; i++) datas[i].Wrap(buffer);
            }
        }

        internal sealed override void Unwrap(SimpleBuffer buffer)
        {
            base.Unwrap(buffer);

            //Get value.
            int value = buffer.GetInteger();
            //Check result.
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
            //Get subnode count.
            int subnodeCount = GetSubnodeCount();
            //Check result.
            if (subnodeCount > 0)
            {
                //Create datas.
                datas = new IndexData[subnodeCount];
                //Do while.
                for (int i = 0; i < subnodeCount; i++)
                {
                    //Create data and unwrap buffer.
                    datas[i] = new IndexData(); datas[i].Unwrap(buffer);
                }
            }
        }

        internal sealed override void Unwrap(SimpleBuffer buffer, PageDescription description)
        {
            base.Unwrap(buffer, description);

            //Get value.
            int value = buffer.GetInteger();
            //Check result.
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
            //Get subnode count.
            int subnodeCount = GetSubnodeCount();
            //Check result.
            if (subnodeCount > 0)
            {
                //Create datas.
                datas = new IndexData[subnodeCount];
                //Do while.
                for (int i = 0; i < subnodeCount; i++)
                {
                    //Create data and unwrap buffer.
                    datas[i] = new IndexData(); datas[i].Unwrap(buffer);
                }
            }
        }

        internal sealed override void CheckValid(long fileSize)
        {
            base.CheckValid(fileSize);

		    //Check occupied size.
		    if(occupiedSize == OccupiedSize.FULL || occupiedSize > 0)
		    {
			    //Check page type.
			    if(pageType != PageType.INDEX_ELEMENT)
			    {
				    throw new IOException("invalid hash key type(" + pageType + ")");
                }
			    //Check next page.
			    if(nextPage != -1)
			    {
				    //Throw exception.
				    throw new IOException("invalid next page(" + nextPage + ") of index element");
                }
		    }
            else
            {
                //Check next page.
                if ((nextPage < 0 && nextPage != -1) || nextPage > fileSize)
                {
                    //Throw exception.
                    throw new IOException("invalid next page(" + nextPage + ") of index element");
                }
            }
            //Check page offset.
            if (pageOffset > fileSize ||
                (pageOffset < 0 && pageOffset != -1) ||
                (pageOffset > 0 && (pageOffset & 0x3FL) != 0))
            {
                //Throw exception.
                throw new IOException("invalid paget offset(" + pageOffset + ")");
            }
            //Check datas.
            if (datas != null && datas.Length > 0)
            {
                //Get subnode count.
                int subnodeCount = GetSubnodeCount();
                //Do while.
                for (int i = 0; i < subnodeCount; i++) datas[i].CheckValid(fileSize);
            }
        }
    }
}
