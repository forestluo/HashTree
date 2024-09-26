using SimpleTeam.Container.List;

namespace SimpleTeam.Container.Queue
{
    public class ListQueueContainer :
        SimpleContainer, IQueueContainer
    {
        //Root of list.
        private IListNode root;
        //Read position.
        private IListNode readPosition;
        //Write position.
        private IListNode writePosition;

        public ListQueueContainer()
        {
            //Set size.
            SetSize(1);
            //Create list.
            root = new SimpleListElement();

            //Set read position.
            readPosition = root;
            //Set write position.
            writePosition = root;
        }

        public ListQueueContainer(int capacity)
            : base(capacity)
        {
            //Set size.
            SetSize(1);
            //Create list.
            root = new SimpleListElement();

            //Set read position.
            readPosition = root;
            //Set write position.
            writePosition = root;
        }

        private void DeleteNode(IListNode node)
        {
            //Remove node.
            RemoveNode(node);
            //Clear value.
            node.ClearValue();
            //Decrease count.
            DecreaseCount();
        }

        private void RemoveNode(IListNode node)
        {
            //Decrease size.
            DecreaseSize();

            //Set next previous.
            node.SetNextPrevious(node.GetPrevious());
            //Set previous next.
            node.SetPreviousNext(node.GetNext());

            //Set next.
            //node.SetNext(null);
            //Set previous.
            //node.SetPrevious(null);
        }

        private void AddHeadNode(IListNode node)
        {
            //Increase size.
            IncreaseSize();

            //Create list element.
            SimpleListElement element = new ();

            //Set next.
            element.SetNext(node.GetNext());
            //Set previous.
            element.SetPrevious(node);

            //Set next previous.
            element.SetNextPrevious(element);
            //Set previous next.
            element.SetPreviousNext(element);
        }

        public object? GetHead()
        {
            //Check position.
            return readPosition != writePosition ? readPosition.GetValue() : null;
        }

        public object? RemoveHead()
        {
            //Check position.
            if (readPosition != writePosition)
            {
                //Decrease count.
                DecreaseCount();
                //Get value.
                object? value = readPosition.GetValue();
                //Clear value.
                readPosition.ClearValue();
                //Move next.
                readPosition = readPosition.GetNext();
                //Return result.
                return value;
            }
            //Return null.
            return null;
        }

        public void AddTail(object value)
        {
            //Increase count.
            IncreaseCount();
            //Set object.
            writePosition.SetValue(value);
            //Check result.
            if (writePosition.GetNext() == readPosition)
            {
                //Add head node.
                AddHeadNode(writePosition);
            }
            //Get next.
            writePosition = writePosition.GetNext();
        }

        public new void ClearAll()
        {
            base.ClearAll();

            //Remove all elements.
            while ((root.GetNext()) != root)
            {
                //Delete node.
                DeleteNode(root.GetNext());
            }

            //Set size.
            SetSize(1);
            //Set read position.
            readPosition = root;
            //Set write position.
            writePosition = root;
        }
    }
}
