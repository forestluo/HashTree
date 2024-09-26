namespace SimpleTeam.Container
{
    public class SimpleElement : INode
	{
		//Object
		private object? value;

		public SimpleElement()
		{

		}

		public SimpleElement(object? value)
		{
            //Set value.
            this.value = value;
		}

	    public void ClearValue() { value = null; }

		public object? GetValue() { return value; }

	    public void SetValue(object? value) { this.value = value; }
	}
}
