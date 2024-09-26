namespace SimpleTeam.Container
{
    public class KeyElement
        : SimpleElement
    {
        /**
	     * Key of element.
	     */
        private string key;

        public KeyElement(string key, object? value) :
            base(value)
        {
            //Set key.
            this.key = key;
        }

        public string GetKey()
        {
            //Return key.
            return key;
        }

        public void SetKey(string key)
        {
            //Set key.
            this.key = key;
        }
    }
}
