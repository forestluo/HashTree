namespace SimpleTeam.Container.Properties
{
    internal interface IPropertiesContainer
        : IContainer
    {
        public int TotalCount();

        public bool Exists(string name);

        public object? Get(string name);

        public void Set(string name, object? value);

        public void Remove(string name);

        public bool GetBoolean(string name, bool defaultValue);

        public byte GetByte(string name, byte defaultValue);

        public short GetShort(string name, short defaultValue);

        public int GetInteger(string name, int defaultValue);

        public long GetLong(string name, long defaultValue);

        public float GetFloat(string name, float defaultValue);

        public double GetDouble(string name, double defaultValue);

        public string? GetString(string name, string defaultValue);

        public byte[]? GetBytes(string name, byte[]? defaultValue);

        public object? GetObject(string name, object defaultValue);

        public void SetBoolean(string name, bool value);

        public void SetByte(string name, byte value);

        public void SetShort(string name, short value);

        public void SetInteger(string name, int value);

        public void SetLong(string name, long value);

        public void SetFloat(string name, float value);

        public void SetDouble(string name, double value);

        public void SetString(string name, string? value);

        public void SetBytes(string name, byte[]? value);

        public void SetObject(string name, object? value);
    }
}
