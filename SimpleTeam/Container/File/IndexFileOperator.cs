using SimpleTeam.IO;

namespace SimpleTeam.Container.File
{
    public class IndexFileOperator
        : FileOperator
    {
        //Max Level
        public const int MAX_LEVEL = 5;

        //Page buffer of hash.
        private IndexPageBuffer pageBuffer;

        public IndexFileOperator(FileContainer container)
            : this(container,
                  IContainer.WITHOUT_LIMIT,
                  IndexPageBuffer.DEFAULT_SIZE_TYPE)
        {

        }

        public IndexFileOperator(FileContainer container, int capacity)
            : this(container, capacity,
                  IndexPageBuffer.DEFAULT_SIZE_TYPE)
        {
        
        }

        public IndexFileOperator(FileContainer container, int capacity, int sizeType)
            : base(container)
        { 
            //Check size type.
            if(sizeType < IndexPageBuffer.DEFAULT_SIZE_TYPE)
            {
                throw new IOException("invalid size type(" + sizeType + ")");
            }

            //Create hash page buffer.
            pageBuffer = new IndexPageBuffer();
            //Initialize.
            pageBuffer.Initialize();
            //Set size type.
            pageBuffer.sizeType = sizeType;
		    //Set capacity.
		    pageBuffer.capacity = capacity;
		    //Set size.
		    pageBuffer.size = 0;
            //Set count.
            pageBuffer.count = 0;
            //Get subnode count.
            int subnodeCount = pageBuffer.GetSubnodeCount();
            //Check count.
            if(subnodeCount <= 0)
            {
                throw new IOException("invalid subnode count(" + subnodeCount + ")");
            }
            //Get size.
            pageBuffer.size = subnodeCount;
            //Create datas.
            pageBuffer.datas = new IndexData[subnodeCount];
            //Do while and initialize subnode offsets.
            for (int i = 0; i < subnodeCount; i++)
            {
                //Create data.
                pageBuffer.datas[i] = new IndexData();
            }

            //Malloc page.
            pageBuffer.offset = MallocPage(PageType.INDEX_PAGE, pageBuffer.sizeType);
            //Check result.
            if (pageBuffer.offset == -1)
            {
                //Add page at tail of file.
                pageBuffer.offset = AddPage(pageBuffer);
            }
            else
            {
                //Write fully.
                WriteFully(pageBuffer.offset, pageBuffer);
            }

            //Do while and initialize subnode offsets.
            for (int i = 0; i < subnodeCount; i ++)
            {
                //Set offset of data.
                pageBuffer.datas[i].offset = pageBuffer.GetOffsetByIndex(i);
            }
    	}

        public IndexFileOperator(FileContainer container, long pageOffset)
            : base(container)
        {
    		//Load page buffer.
    		PageBuffer buffer = container.LoadPageBuffer(pageOffset);
            //Check size type.
            if(buffer.sizeType<IndexPageBuffer.DEFAULT_SIZE_TYPE)
            {
                throw new IOException("invalid size type(" + buffer.sizeType + ")");
            }
		    //Check result.
		    if(buffer.GetType() != typeof(IndexPageBuffer))
		    {
			    throw new IOException("invalid hash page buffer at(" + pageOffset + ")");
            }
            //Set page buffer.
            pageBuffer = (IndexPageBuffer)buffer;
            //Set page buffer offset.
            pageBuffer.offset = pageOffset;
            //Check datas.
            if (pageBuffer.datas == null)
            {
                throw new IOException("null datas of index page buffer");
            }
            //Get subnode count.
            int subnodeCount = pageBuffer.GetSubnodeCount();
            //Check count.
            if (subnodeCount <= 0)
            {
                throw new IOException("invalid subnode count(" + subnodeCount + ")");
            }
            //Do while and initialize subnode offsets.
            for (int i = 0; i < subnodeCount; i++)
            {
                //Set offset of data.
                pageBuffer.datas[i].offset = pageBuffer.GetOffsetByIndex(i);
            }
	    }

        public int GetSize()
        {
            //Return size.
            return pageBuffer.size;
        }

        public int GetCount()
        {
            //Return count.
            return pageBuffer.count;
        }

        public int GetCapacity()
        {
            //Return capacity.
            return pageBuffer.capacity;
        }

        public long GetEntrance()
        {
            //Return page offset.
            return pageBuffer.offset;
        }

        public sealed override void Flush()
        {
            //Check page buffer.
            if (pageBuffer == null)
            {
                throw new IOException("operator is closed");
            }
            //Write fully.
            WriteFully(pageBuffer.offset, pageBuffer);
        }

        private IndexData? GetSubnode(long key)
        {
            //Return result.
            return pageBuffer.GetDataByKey(key);
        }

        private IndexElementBuffer LoadElement(int level, long offset)
        {
            //Get size type.
            int sizeType =
                IndexElementBuffer.DEFAULT_SIZE_TYPE + level;
            //Create buffer.
            SimpleBuffer buffer = SimpleBuffer.CreateBuffer(sizeType);
            //Read fully.
            container.ReadFully(offset, buffer);
            //Create index element buffer.
            IndexElementBuffer elementBuffer = new ();
            //Unwrap.
            elementBuffer.Unwrap(buffer);
            //Check valid.
            elementBuffer.CheckValid(GetDataSize());
            //Check size type.
            if (elementBuffer.sizeType != sizeType)
            {
                throw new IOException("invalid size type(" + elementBuffer.sizeType + ") of element");
            }
            //Check page offset.
            if (elementBuffer.pageOffset != pageBuffer.offset)
            {
                throw new IOException("invalid page offset(" + elementBuffer.pageOffset + ") of element");
            }
            //Set level.
            elementBuffer.level = level;
            //Set offset.
            elementBuffer.offset = offset;
            //Check datas.
            if(elementBuffer.datas == null)
            {
                throw new IOException("null datas of element buffer");
            }
            //Set element.
            for (int i = 0; i < elementBuffer.datas.Length; i++)
            {
                //Set offset of data.
                elementBuffer.datas[i].offset = elementBuffer.GetOffsetByIndex(i);
            }
            //Return result.
            return elementBuffer;
        }

	    private long KeepElement(int level,long key,long dataOffset)
	    {
		    //Create hash element buffer.
		    IndexElementBuffer buffer = new ();
		    //Initialize.
		    buffer.Initialize();
            //Set size type.
            buffer.sizeType =
                IndexElementBuffer.DEFAULT_SIZE_TYPE + level;
            //Set page offset.
            buffer.pageOffset = pageBuffer.offset;
            //Get subnode count.
            int subnodeCount = buffer.GetSubnodeCount();
            //Check count.
            if(subnodeCount <= 0)
            {
                throw new IOException("invalid subnode count(" + subnodeCount + ")");
            }
            //Create datas.
            buffer.datas = new IndexData[subnodeCount];
            //Do while.
            for(int i = 0;i < subnodeCount;i ++)
            {
                //Create data.
                buffer.datas[i] = new IndexData();
            }
            //Get hash data.
            IndexData? data = buffer.GetDataByKey(key);
            //Check result.
            if(data == null)
            {
                throw new IOException("null index data of key(" + key + ")");
            }
            //Set key.
            data.key = key;
            //Set data offset.
            data.dataOffset = dataOffset;
        
		    //Malloc page.
		    long offset = MallocPage(PageType.INDEX_ELEMENT,buffer.sizeType);
		    //Check result.
		    if(offset != -1)
		    {
			    //Write fully.
			    WriteFully(offset, buffer);
		    }
		    else
		    {
                //Add page at the tail of file.
                offset = container.AddPage(buffer);
		    }
            //Add size and count.
            pageBuffer.size += subnodeCount; pageBuffer.count ++;
		    //Return subnode offset.
		    return offset;
	    }

        private IndexData LoadIndexData(int level, long offset, long key)
        {
            //Get size type.
            int sizeType =
                IndexElementBuffer.DEFAULT_SIZE_TYPE + level;
            //Get offset of data.
            offset += IndexElementBuffer.GetSubnodeOffset(sizeType, key);
            //Create hash data.
            IndexData data = new ();
            //Read fully.
            data.ReadFully(container, offset);
            //Set offset.
            data.offset = offset;
            //Return data.
            return data;
        }

        public long LoadData(long key)
        {
            //Check result.
            if (key < 0)
            {
                throw new IOException("key is negative");
            }
            //Check page buffer.
            if (pageBuffer == null)
            {
                throw new IOException("operator is closed");
            }

            //Get subnode.
            IndexData? data = GetSubnode(key);
            //Check result.
            if(data == null)
            {
                throw new IOException("null index data of key(" + key + ")");
            }
            //Check key.
            if (data.key == key) return data.dataOffset;
            //Check subnode.
            else if (data.subnodeOffset == -1L) return -1L;

            //Level.
            int level = 0;
            //Offset value.
            long offsetValue = -1L;
            //Subnode offset.
            long subnodeOffset = data.subnodeOffset;
            //Do while.
            while (subnodeOffset != -1 && level < MAX_LEVEL)
            {
                //Load index data.
                data = LoadIndexData(level, subnodeOffset, key);
                //Check key.
                if (data.key == key)
                {
                    //Set offset value.
                    offsetValue = data.dataOffset; break;
                }
                //Get subnode offfset and add level.
                subnodeOffset = data.subnodeOffset; level ++;
            }
            //Return result.
            return offsetValue;
        }

        public long KeepData(long key, long dataOffset)
        {
            //Check result.
            if (key < 0)
            {
                throw new IOException("key is negative");
            }
            //Check page buffer.
            if (pageBuffer == null)
            {
                throw new IOException("operator is closed");
            }

            //Offset value.
            long offsetValue = -1L;
            //Saved flag.
            bool savedFlag = false;
            //Subnode offset.
            IndexData? data = GetSubnode(key);
            //Check result.
            if (data == null)
            {
                throw new IOException("null index data of key(" + key + ")");
            }
            //Check result.
            if (data.key == key)
            {
                //Get offset.
                offsetValue =
                    data.dataOffset;
                //Set data offset.
                data.dataOffset = dataOffset;
                //Synchronized it to disk file.
                data.WriteFully(container, data.offset);
                //Return offset value.
                return offsetValue;
            }
            else if (data.key == -1L)
            {
                //Set saved flag.
                savedFlag = true;
                //Add count.
                pageBuffer.count ++;

                //Set key.
                data.key = key;
                //Set data offset.
                data.dataOffset = dataOffset;
                //Synchronized it to disk file.
                data.WriteFully(container, data.offset);
            }
            else if (data.subnodeOffset == -1L)
            {
                //Set subnode offset.
                data.subnodeOffset =
                    KeepElement(0, key, dataOffset);
                //Synchronized it to disk file.
                data.WriteFully(container, data.offset); return -1L;
            }

            //Levels.
            int level = 0;
            //Subnode offset.
            long subnodeOffset = data.subnodeOffset;
            //Do while.
            while (subnodeOffset != -1 && level < MAX_LEVEL)
            {
                //Load index data.
                data = LoadIndexData(level, subnodeOffset, key);
                //Check key.
                if (data.key == key)
                {
                    //Offset value.
                    offsetValue =
                        data.dataOffset;
                    //Check saved flag.
                    if (!savedFlag)
                    {
                        //Set key.
                        data.key = key;
                        //Set data offset.
                        data.dataOffset = dataOffset;
                    }
                    else
                    {
                        //Add count.
                        pageBuffer.count --;

                        //Set key.
                        data.key = -1L;
                        //Set data offset.
                        data.dataOffset = -1L;
                    }
                    //Synchronized it to disk file.
                    data.WriteFully(container, data.offset); break;
                }
                //Check key.
                else if (data.key == -1L)
                {
                    //Check saved flag.
                    if (!savedFlag)
                    {
                        //Set saved flag.
                        savedFlag = true;
                        //Add count.
                        pageBuffer.count ++;

                        //Set key.
                        data.key = key;
                        //Set data offset.
                        data.dataOffset = dataOffset;
                        //Synchronized it to disk file.
                        data.WriteFully(container, data.offset);
                    }
                }
                else if (data.subnodeOffset == -1L)
                {
                    //Check saved.
                    if (!savedFlag)
                    {
                        //Set subnode offset.
                        data.subnodeOffset =
                            KeepElement(level + 1, key, dataOffset);
                        //Synchronized it to disk file.
                        data.WriteFully(container, data.offset); break;
                    }
                }
                //Get subnode offset and add level.
                subnodeOffset = data.subnodeOffset; level ++;
            }
            //Return result.
            return offsetValue;
        }

        public long RemoveData(long key)
        {
            //Check result.
            if (key < 0)
            {
                throw new IOException("key is negative");
            }
            //Check page buffer.
            if (pageBuffer == null)
            {
                throw new IOException("operator is closed");
            }

            //Offset value.
            long offsetValue = -1L;
            //Subnode offset.
            IndexData? data = GetSubnode(key);
            //Check result.
            if (data == null)
            {
                throw new IOException("null index data of key(" + key + ")");
            }
            //Check result.
            if (data.key == key)
            {
                //Get offset.
                offsetValue =
                    data.dataOffset;
                //Set key offset.
                data.key = -1L;
                //Set data offset.
                data.dataOffset = -1L;
                //Synchronized it to disk file.
                data.WriteFully(container, data.offset);

                //Sub count.
                pageBuffer.count --;
                //Return offset value.
                return offsetValue;
            }
            else if (data.subnodeOffset == -1L) return -1L;

            //Levels.
            int level = 0;
            //Subnode offset.
            long subnodeOffset = data.subnodeOffset;
            //Do while.
            while (subnodeOffset != -1L && level < MAX_LEVEL)
            {
                //Load index data.
                data = LoadIndexData(level, subnodeOffset, key);
                //Check key.
                if (data.key == key)
                {
                    //Get offset value.
                    offsetValue =
                        data.dataOffset;

                    //Clear key.
                    data.key = -1L;
                    //Clear data offset.
                    data.dataOffset = -1L;
                    //Synchronized it to disk.
                    data.WriteFully(container, data.offset);

                    //Sub count.
                    pageBuffer.count --; break;
                }
                //Get subnode offfset and add level.
                subnodeOffset = data.subnodeOffset; level ++;
            }
            //Return offset value.
            return offsetValue;
        }

        private void ClearSubnode(IndexElementBuffer buffer)
        {
            //Check value.
            if (buffer.datas == null)
            {
                throw new IOException("null index data of node");
            }
            //Get subnode count.
            int subnodeCount = buffer.GetSubnodeCount();
            //Do while.
            for (int i = 0; i < subnodeCount; i++)
            {
                //Get subnode offset.
                long subnodeOffset = buffer.datas[i].subnodeOffset;
                //Check result.
                if (subnodeOffset != -1L)
                {
                    //Clear subnode.
                    ClearSubnode(LoadElement(buffer.level + 1, subnodeOffset));
                }
                //Check key and sub count.
                if (buffer.datas[i].key != -1L)
                {
                    //Sub count.
                    pageBuffer.count --;
                    //Clear key.
                    buffer.datas[i].key = -1L;
                }
            }
            //Sub size.
            pageBuffer.size -= subnodeCount;
            //Free page.
            container.FreePage(buffer.offset, buffer);
        }

        public void ClearAll()
        {
            //Get subnode count.
            int subnodeCount =
                pageBuffer.GetSubnodeCount();
            //Check value.
            if (pageBuffer.datas == null)
            {
                throw new IOException("null index data of node");
            }
            //Do while.
            for (int i = 0; i < subnodeCount; i++)
            {
                //Get subnode offset.
                long subnodeOffset =
                    pageBuffer.datas[i].subnodeOffset;
                //Check result.
                if (subnodeOffset != -1L)
                {
                    //Set subnode offset.
                    pageBuffer.datas[i].subnodeOffset = -1L;
                    //Clear subnode.
                    ClearSubnode(LoadElement(0, subnodeOffset));
                }
                //Check key and sub count.
                if (pageBuffer.datas[i].key != -1L)
                {
                    //Sub count.
                    pageBuffer.count --;
                    //Clear key.
                    pageBuffer.datas[i].key = -1L;
                }
            }
        }
    }
}
