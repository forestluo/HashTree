using SimpleTeam.IO;
using SimpleTeam.Constant;

namespace SimpleTeam.Container.File
{
    public class IndexPageBuffer
        : PageBuffer
    {
        //////////////////////////////////////////////////
        //
        //Offsets.
        //
        //Capacity        [int]
        //Size            [int]
        //Count           [int]
        //Hash Datas      [N * IndexData]
        //
        //////////////////////////////////////////////////
        //Default Size Type
        //The sizes of page are limited as 2KB, 4KB, 8KB, ..., 1024KB.
        internal const int DEFAULT_SIZE_TYPE = SizeType._64MB;
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
        //Index Datas
        public IndexData[]? datas;

        public IndexPageBuffer()
            : this(DEFAULT_SIZE_TYPE)
        {

        }

        public IndexPageBuffer(int sizeType)
            : base(PageType.INDEX_PAGE, sizeType)
        {
            
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
                3 * SizeOf.INTEGER + index * IndexData.SIZE;
        }

        internal long GetOffsetByKey(long key)
        {
            //Return result.
            return offset + PageDescription.SIZE +
                3 * SizeOf.INTEGER + GetIndexByKey(key) * IndexData.SIZE;
        }

        internal IndexData? GetDataByKey(long key)
        {
            //Return result.
            return datas?[GetSubnodeIndex(sizeType, key)];
        }

        static int GetSubnodeCount(int sizeType)
        {
            //Check value.
            switch (sizeType)
            {
                case SizeType._1MB:
                    return 65521;
                case SizeType._2MB:
                    return 131063;
                case SizeType._4MB:
                    return 262139;
                case SizeType._8MB:
                    return 524269;
                case SizeType._16MB:
                    return 1048573;
                case SizeType._32MB:
                    return 2097143;
                case SizeType._64MB:
                    return 4194301;
                //default:;
            }
            //Return -1.
            return -1;
        }

        static int GetSubnodeIndex(int sizeType, long key)
        {
            //Get index.
            return (int)(key % GetSubnodeCount(sizeType));
        }

        internal sealed override void Initialize()
        {
            base.Initialize();

            //Set page type.
            pageType = PageType.INDEX_PAGE;
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

            //Clear datas.
            datas = null;
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

            //Get capacity.
            capacity = buffer.GetInteger();
            //Get size.
            size = buffer.GetInteger();
            //Get count.
            count = buffer.GetInteger();

            //Get subnode count.
            int subnodeCount = GetSubnodeCount();
            //Check count.
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

            //Get capacity.
            capacity = buffer.GetInteger();
            //Get size.
            size = buffer.GetInteger();
            //Get count.
            count = buffer.GetInteger();

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
            if (occupiedSize == OccupiedSize.FULL || occupiedSize > 0)
            {
                //Check page type.
                if (pageType != PageType.INDEX_PAGE)
                {
                    throw new IOException("invalid index page type(" + pageType + ")");
                }
                //Check next page.
                if (nextPage != -1)
                {
                    //Throw exception.
                    throw new IOException("invalid next page(" + nextPage + ") of index page");
                }
            }
            else
            {
                //Check next page.
                if ((nextPage < 0 && nextPage != -1) || nextPage > fileSize)
                {
                    //Throw exception.
                    throw new IOException("invalid next page(" + nextPage + ") of index page");
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
