namespace SimpleTeam.Container.List
{
    internal class SimpleListElement :
        SimpleElement, IListNode
    {
        //Next
        private IListNode next;
        //Previous
        private IListNode previous;

        public SimpleListElement()
        {
            //Set next.
            next = this;
            //Set previous.
            previous = this;
        }

        public SimpleListElement(object? value) :
            base(value)
        {
            //Set next.
            next = this;
            //Set previous.
            previous = this;
        }

        public IListNode GetNext()
        {
            //Return next node.
            return next;
        }

        public IListNode GetPrevious()
        {
            //Return previous node.
            return previous;
        }

        public void SetNext(IListNode node)
        {
            //Set next.
            next = node;
        }

        public void SetPrevious(IListNode node)
        {
            //Set previous.
            previous = node;
        }

        public void SetNextPrevious(IListNode node)
        {
            //Set next previous.
            next.SetPrevious(node);
        }

        public void SetPreviousNext(IListNode node)
        {
            //Set previous next.
            previous.SetNext(node);
        }
    }
}
