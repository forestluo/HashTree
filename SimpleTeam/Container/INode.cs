namespace SimpleTeam.Container
{
    public interface INode
    {
        public void ClearValue();

        public object? GetValue();

        public void SetValue(object? value);
    }
}
