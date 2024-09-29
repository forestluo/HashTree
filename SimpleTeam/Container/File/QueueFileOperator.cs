using System.ComponentModel;
using System;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace SimpleTeam.Container.File
{
    public class QueueFileOperator
		: FileOperator
    {
        //Page buffer of queue.
        private QueuePageBuffer pageBuffer;

        public QueueFileOperator(FileContainer container, int capacity)
            : base(container)
        {
            //Create queue page buffer.
            pageBuffer = new QueuePageBuffer();
            //Initialize.
            pageBuffer.Initialize();
            //Set size.
            pageBuffer.size = 1;
            //Set capacity.
            pageBuffer.capacity = capacity;
            //Malloc page.
            pageBuffer.offset = MallocPage(PageType.QUEUE_PAGE, QueuePageBuffer.DEFAULT_SIZE_TYPE);
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

            //Keep root element.
            long rootPosition = KeepElement(-1, -1);
            //Set root position.
            pageBuffer.rootPosition = rootPosition;
            //Set read position.
            pageBuffer.readPosition = rootPosition;
            //Set write position.
            pageBuffer.writePosition = rootPosition;
            //Write fully.
            WriteFully(pageBuffer.offset, pageBuffer);
        }

        public QueueFileOperator(FileContainer container, long offset)
            : base(container)
        {
            //Load page buffer.
            PageBuffer buffer = container.LoadPageBuffer(offset);
            //Check result.
            if (!(buffer.GetType() != typeof(QueuePageBuffer)))
            {
                throw new IOException("invalid queue page buffer at(" + offset + ")");
            }
            //Set page buffer.
            pageBuffer = (QueuePageBuffer)buffer;
            //Set offset.
            pageBuffer.offset = offset;
        }

        public void KeepQueuePage()
        {
            //Write fully.
            WriteFully(pageBuffer.offset, pageBuffer);
        }

        private long KeepElement(long nextElement, long dataOffset)
        {
            //Create queue element buffer.
            QueueElementBuffer buffer = new();
            //Initialize.
            buffer.Initialize();
            //Set data offset.
            buffer.dataOffset = dataOffset;
            //Set page offset.
            buffer.pageOffset = pageBuffer.offset;
            //Malloc page.
            long offset = MallocPage(PageType.QUEUE_ELEMENT, QueueElementBuffer.DEFAULT_SIZE_TYPE);
            //Check result.
            if (offset != -1)
            {
                //Set next element.
                buffer.nextElement = nextElement == -1 ? offset : nextElement;
                //Write fully.
                WriteFully(offset, buffer);
            }
            else
            {
                //Set next element.
                buffer.nextElement = nextElement == -1 ? container.GetDataSize() : nextElement;
                //Add page at the tail of file.
                offset = container.AddPage(buffer);
            }
            //Return offset.
            return offset;
        }

        public long LoadData()
        {
            //Offset.
            long dataOffset = -1;
            //Check result.
            if (pageBuffer.readPosition != pageBuffer.writePosition)
            {
                //Create queue element buffer.
                QueueElementBuffer element = new ();
                //Read fully.
                ReadFully(pageBuffer.readPosition, element);
                //Get data offset.
                dataOffset = element.dataOffset;
                //Set read position.
                pageBuffer.readPosition = element.nextElement;
                //Sub count.
                pageBuffer.count --;
            }
		    //Return result.
		    return dataOffset;
	    }

        public void KeepData(long dataOffset)
        {
            //Create queue element buffer.
            QueueElementBuffer element = new ();
            //Synchronized.
            //Read fully.
            ReadFully(pageBuffer.writePosition, element);
            //Set data offset.
            element.dataOffset = dataOffset;
            //Check result.
            if (element.nextElement == pageBuffer.readPosition)
            {
                //Add next element.
                element.nextElement = KeepElement(pageBuffer.readPosition, dataOffset);
                //Add size.
                pageBuffer.size ++;
            }
            //Write fully.
            WriteFully(pageBuffer.writePosition, element);

            //Add count.
            pageBuffer.count ++;
            //Set write position.
            pageBuffer.writePosition = element.nextElement;
        }

        public void ClearAll()
        {
            //Create queue element buffer.
            QueueElementBuffer rootElement = new ();
            //Read fully.
            ReadFully(pageBuffer.rootPosition, rootElement);

            //Create queue element buffer.
            QueueElementBuffer element = new ();
            //Get position.
            long position = rootElement.nextElement;
            //Do while.
            while (position != pageBuffer.rootPosition)
            {
                //Read fully.
                ReadFully(position, element);
                //Get next element.
                long nextElement = element.nextElement;
                //Free page.
                FreePage(position, element);
                //Sub size.
                pageBuffer.size --;
                //Set position.
                position = nextElement;
            }
            //Set next element.
            rootElement.nextElement = pageBuffer.rootPosition;
            //Write fully.
            WriteFully(pageBuffer.rootPosition, rootElement);

            //Set count.
            pageBuffer.count = 0;
            //Set page buffer.
            pageBuffer.readPosition = pageBuffer.writePosition = pageBuffer.rootPosition;
        }
    }
}
