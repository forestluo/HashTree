using SimpleTeam.Constant;

namespace SimpleTeam.Container.Properties
{
    public abstract class SimplePropertiesContainer
        : SimpleContainer, IPropertiesContainer
    {
        public SimplePropertiesContainer()
        {

        }

        public abstract int TotalCount();

        public abstract bool Exists(string name);

        public abstract void Remove(string name);

        public abstract object? Get(string name);

        public abstract void Set(string name, object? value);

        public bool GetBoolean(string name, bool defaultValue)
        {
            //Get value.
            object? value = Get(name);
            //Check result.
            if (value != null)
            {
                try
                {
                    //Get boolean.
                    return Convert.ToBoolean(value);
                }
                catch (Exception e)
                {
                    //Print.
                    Console.WriteLine("SimplePropertiesContainer.GetBoolean : " + e.Message);
                }
            }
            //Return default value.
            return defaultValue;
        }

        public byte GetByte(string name, byte defaultValue)
        {
            //Get value.
            object? value = Get(name);
            //Check result.
            if (value != null)
            {
                try
                {
                    //Get byte.
                    return Convert.ToByte(value);
                }
                catch (Exception e)
                {
                    //Print.
                    Console.WriteLine("SimplePropertiesContainer.GetByte : " + e.Message);
                }
            }
            //Return default value.
            return defaultValue;
        }

        public short GetShort(string name, short defaultValue)
        {
            //Get value.
            object? value = Get(name);
            //Check result.
            if (value != null)
            {
                try
                {
                    //Get short.
                    return Convert.ToInt16(value);
                }
                catch (Exception e)
                {
                    //Print.
                    Console.WriteLine("SimplePropertiesContainer.GetShort : " + e.Message);
                }
            }
            //Return default value.
            return defaultValue;
        }

        public int GetInteger(string name, int defaultValue)
        {
            //Get value.
            object? value = Get(name);
            //Check result.
            if (value != null)
            {
                try
                {
                    //Get integer.
                    return Convert.ToInt32(value);
                }
                catch (Exception e)
                {
                    //Print.
                    Console.WriteLine("SimplePropertiesContainer.GetInteger : " + e.Message);
                }
            }
            //Return default value.
            return defaultValue;
        }

        public long GetLong(string name, long defaultValue)
        {
            //Get value.
            object? value = Get(name);
            //Check result.
            if (value != null)
            {
                try
                {
                    //Get long.
                    return Convert.ToInt64(value);
                }
                catch (Exception e)
                {
                    //Print.
                    Console.WriteLine("SimplePropertiesContainer.GetLong : " + e.Message);
                }
            }
            //Return default value.
            return defaultValue;
        }

        public float GetFloat(string name, float defaultValue)
        {
            //Get value.
            object? value = Get(name);
            //Check result.
            if (value != null)
            {
                try
                {
                    //Get float.
                    return Convert.ToSingle(value);
                }
                catch (Exception e)
                {
                    //Print.
                    Console.WriteLine("SimplePropertiesContainer.GetFloat : " + e.Message);
                }
            }
            //Return default value.
            return defaultValue;
        }

        public double GetDouble(string name, double defaultValue)
        {
            //Get value.
            object? value = Get(name);
            //Check result.
            if (value != null)
            {
                try
                {
                    //Get double.
                    return Convert.ToDouble(value);
                }
                catch (Exception e)
                {
                    //Print.
                    Console.WriteLine("SimplePropertiesContainer.GetDouble : " + e.Message);
                }
            }
            //Return default value.
            return defaultValue;
        }

        public string? GetString(string name, string? defaultValue)
        {
            //Get value.
            object? value = Get(name);
            //Check result.
            if (value != null)
            {
                try
                {
                    //Check type.
                    if (value.GetType() == typeof(string)) return (string)value;
                }
                catch (Exception e)
                {
                    //Print.
                    Console.WriteLine("SimplePropertiesContainer.GetString : " + e.Message);
                }
            }
            //Return default value.
            return defaultValue;
        }

        public byte[]? GetBytes(string name, byte[]? defaultValue)
        {
            //Get value.
            object? value = Get(name);
            //Check result.
            if (value != null)
            {
                try
                {
                    //Check type.
                    if (value.GetType() == typeof(byte[])) return (byte[])value;
                }
                catch (Exception e)
                {
                    //Print.
                    Console.WriteLine("SimplePropertiesContainer.GetString : " + e.Message);
                }
            }
            //Return default value.
            return defaultValue;
        }

        public object? GetObject(string name, object? defaultValue)
        {
            //Get value.
            object? value = Get(name);
            //Check result.
            if (value != null)
            {
                //Return value.
                return value;
            }
            //Return default value.
            return defaultValue;
        }

        public void SetBoolean(string name, bool value)
        {
            //Set property.
            Set(name, value);
        }

        public void SetByte(string name, byte value)
        {
            //Set property.
            Set(name, value);
        }

        public void SetShort(string name, short value)
        {
            //Set property.
            Set(name, value);
        }

        public void SetInteger(string name, int value)
        {
            //Set property.
            Set(name, value);
        }

        public void SetLong(string name, long value)
        {
            //Set property.
            Set(name, value);
        }

        public void SetFloat(string name, float value)
        {
            //Set property.
            Set(name, value);
        }

        public void SetDouble(string name, double value)
        {
            //Set property.
            Set(name, value);
        }

        public void SetString(string name, string? value)
        {
            //Check value.
            if (!Empty.IsNullOrEmpty(value)) Set(name, value);
        }

        public void SetBytes(string name, byte[]? value)
        {
            //Check value.
            if (!Empty.IsNullOrEmpty(value)) Set(name, value);
        }

        public void SetObject(string name, object? value)
        {
            //Check object.
            if (value != null) Set(name, value);
        }
    }
}
