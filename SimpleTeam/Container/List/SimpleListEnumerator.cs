using System.Collections;

namespace SimpleTeam.Container.List
{
    public class SimpleListEnumerator
        : IEnumerator
    {
        //Root
        private IListNode root;
        //Next
        private IListNode next;

        public SimpleListEnumerator(IListNode root)
        {
            //Set root.
            this.root = root;
            //Set next.
            this.next = root.GetNext();
        }

        public IListNode Current
        {
            //Return value.
            get { return next; }
        }

        object IEnumerator.Current
        {
            //Return value.
            get { return Current; }
        }

        public void Reset()
        {
            //Set next.
            next = root.GetNext();
        }

        public bool MoveNext()
        {
            //The end.
            if (next == root) return false;
            //Move to next.
            next = next.GetNext(); return true;
        }
    }
}
