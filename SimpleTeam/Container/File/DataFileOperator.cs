using SimpleTeam.IO;

namespace SimpleTeam.Container.File
{
    public class DataFileOperator
        : FileOperator
    {
        public DataFileOperator(FileContainer container)
            : base(container)
        {

        }

        public void FreeData(long offset)
        {
		    //Check result.
		    if(offset< 0 || (offset > 0 && offset > GetLength()))
		    {
			    //Throw exception.
			    throw new IOException("invalid offset(" + offset + ") of data page");
            }
            //Create page description.
            PageDescription description = new ();
            //Read page description.
            ReadFully(offset, description);
		    //Check result.
		    if(description.pageType != PageType.DATA_PAGE)
		    {
			    //Throw exception.
			    throw new IOException("invalid page type(" + description.pageType + ") of data page");
            }
            //Free page.
            FreePage(offset, description);
    	}

        public byte[] LoadData(long offset)
        {
            //Check result.
            if (offset < 0 || (offset > 0 && offset > GetLength()))
            {
                //Throw exception.
                throw new IOException("invalid offset(" + offset + ") of data page");
            }
            //Create page description.
            PageDescription description = new ();
            //Read page description.
            ReadFully(offset, description);
            //Check result.
            if (description.pageType != PageType.DATA_PAGE)
            {
                //Throw exception.
                throw new IOException("invalid page type(" + description.pageType + ") of data page");
            }
            //Create bytes.
            byte[] bytes = new byte[description.occupiedSize];
            ////////////////////////////////////////////////////
            //
            //Check byte buffer type.
            if (bytes.Length <= SimpleBuffer.NORMAL_SIZE)
            {
                //Read data from file.
                ReadFully(offset + PageDescription.SIZE, bytes);
            }
            else
            {
                //Create data page buffer.
                DataPageBuffer buffer = new ();
                //Set size type.
                buffer.sizeType = description.sizeType;
                //Set bytes.
                buffer.bytes = bytes;
                //Get length.
                int length = PageDescription.SIZE + bytes.Length;
                //Add padding.
                length = length + ((length & 0x03) != 0 ? 4 - (length & 0x03) : 0);
                //Read data from file.
                ReadPartially(offset, buffer, length);
            }
            //
            ////////////////////////////////////////////////////
            //Return result.
            return bytes;
        }

        public byte[] RemoveData(long offset)
        {
            //Check result.
            if (offset < 0 || (offset > 0 && offset > GetLength()))
            {
                //Throw exception.
                throw new IOException("invalid offset(" + offset + ") of data page");
            }

            //Create page description.
            PageDescription description = new ();
            //Read page description.
            ReadFully(offset, description);
            //Check result.
            if (description.pageType != PageType.DATA_PAGE)
            {
                //Throw exception.
                throw new IOException("invalid page type(" + description.pageType + ") of data page");
            }

            //Create bytes.
            byte[] bytes = new byte[description.occupiedSize];
            ////////////////////////////////////////////////////
            //
            //Check byte buffer type.
            if (bytes.Length <= SimpleBuffer.NORMAL_SIZE)
            {
                //Read data from file.
                ReadFully(offset + PageDescription.SIZE, bytes);
            }
            else
            {
                //Create data page buffer.
                DataPageBuffer buffer = new DataPageBuffer();
                //Set size type.
                buffer.sizeType = description.sizeType;
                //Set bytes.
                buffer.bytes = bytes;
                //Get length.
                int length = PageDescription.SIZE + bytes.Length;
                //Add padding.
                length = length + ((length & 0x03) != 0 ? 4 - (length & 0x03) : 0);
                //Read data from file.
                ReadPartially(offset, buffer, length);
            }
            //
            ////////////////////////////////////////////////////
            //Free page.
            FreePage(offset, description);
            //Return bytes.
            return bytes;
        }

        public long KeepData(byte[] bytes)
        {
            //Calculate page size.
            int pageSize = bytes.Length + PageDescription.SIZE;
            //Get suitable page size.
            int sizeType = SizeType.GetSizeType(pageSize);
            //Check result.
            if (sizeType == -1)
            {
                throw new IOException("unsupported size of data(" + bytes.Length + ")");
            }
            //Create page description.
            PageDescription description = new ();
            //Set size type.
            description.sizeType = sizeType;
            //Set page type.
            description.pageType = PageType.DATA_PAGE;
            //Malloc page.
            long offset = MallocPage(description);
            //Check result.
            if (offset == -1)
            {
                //Create data page buffer.
                DataPageBuffer buffer = new ();
                //Initialize.
                buffer.Initialize();
                //Set size type.
                buffer.sizeType = sizeType;
                //Set occupied size.
                buffer.occupiedSize = bytes.Length;
                //Set data reference.
                buffer.bytes = bytes;
                //Add page at tail of file.
                offset = AddPage(buffer);
            }
            else
            {
                //Set occupied size.
                description.occupiedSize = bytes.Length;
                ////////////////////////////////////////////////////
                //
                //Check byte buffer type.
                if (bytes.Length <= SimpleBuffer.NORMAL_SIZE)
                {
                    //Write description.
                    WriteFully(offset, description);
                    //Write data to file.
                    WriteFully(offset + PageDescription.SIZE, bytes);
                }
                else
                {
                    //Create data page buffer.
                    DataPageBuffer buffer = new ();
                    //Set size type.
                    buffer.sizeType = sizeType;
                    //Set occupied size.
                    buffer.occupiedSize = bytes.Length;
                    //Set bytes.
                    buffer.bytes = bytes;
                    //Set next page.
                    buffer.nextPage = -1;
                    //Get length.
                    int length = PageDescription.SIZE + bytes.Length;
                    //Add padding.
                    length = length + ((length & 0x03) != 0 ? 4 - (length & 0x03) : 0);
                    //Write bytes.
                    WritePartially(offset, buffer, length);
                }
                //
                ////////////////////////////////////////////////////
            }
            //Return result.
            return offset;
        }
    }
}
