using System.Collections;

namespace SimpleTeam.Container.List
{
    public class SimpleListContainer :
        SimpleContainer, IEnumerable 
    {
        private IListNode root;

        public SimpleListContainer()
        {
            //Set size.
            SetSize(1);
            //Create root element.
            root = new SimpleListElement();
        }

        public SimpleListContainer(int capacity) :
            base(capacity)
        {
            //Set size.
            SetSize(1);
            //Create root element.
            root = new SimpleListElement();
        }

        public void DeleteNode(IListNode node)
        {
            //Remove node.
            RemoveNode(node);
            //Clear value.
            node.ClearValue();
            //Decrease count.
            DecreaseCount();
        }

        public void AddTailNode(IListNode node)
        {
            //Increase size.
            IncreaseSize();

            //Set next.
            node.SetNext(root);
            //Set previous.
            node.SetPrevious(root.GetPrevious());

            //Set next previous.
            node.SetNextPrevious(node);
            //Set previous next.
            node.SetPreviousNext(node);
        }

        public void AddHeadNode(IListNode node)
        {
            //Increase size.
            IncreaseSize();

            //Set next.
            node.SetNext(root.GetNext());
            //Set previous.
            node.SetPrevious(root);

            //Set next previous.
            node.SetNextPrevious(node);
            //Set previous next.
            node.SetPreviousNext(node);
        }

        public void RemoveNode(IListNode node)
        {
            //Decrease size.
            DecreaseSize();

            //Set next previous.
            node.SetNextPrevious(node.GetPrevious());
            //Set previous next.
            node.SetPreviousNext(node.GetNext());
        }

        public object? GetHead()
        {
            //Get head of list.
            return root.GetNext().GetValue();
        }

        public object? GetTail()
        {
            //Get tail of list.
            return root.GetPrevious().GetValue();
        }

        public object? RemoveHead()
        {
            //Get head.
            IListNode head = root.GetNext();
            //Check result.
            if (head == root) return null;
            //Get object.
            object? value = head.GetValue();
            //Delete node.
            DeleteNode(head);
            //Decrease count.
            DecreaseCount();
            //Return value.
            return value;
        }

        public object? RemoveTail()
        {
            //Get tail.
            IListNode tail = root.GetPrevious();
            //Check result.
            if (tail == root) return null;
            //Get object.
            object? value = tail.GetValue();
            //Delete node.
            DeleteNode(tail);
            //Decrease count.
            DecreaseCount();
            //Return value.
            return value;
        }

        public void AddHead(object? value)
        {
            //Increase count.
            IncreaseCount();
            //Add head.
            AddHeadNode(new SimpleListElement(value));
        }

        public void AddTail(object? value)
        {
            //Increase count.
            IncreaseCount();
            //Add tail.
            AddTailNode(new SimpleListElement(value));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public SimpleListEnumerator GetEnumerator()
        {
            return new SimpleListEnumerator(root);
        }

        public new void ClearAll()
        {
            base.ClearAll();

            //Remove all elements.
            while ((root.GetNext()) != root) DeleteNode(root.GetNext());
        }
    }
}
