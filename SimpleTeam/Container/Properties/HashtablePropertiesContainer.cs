using System.Text;
using System.Collections;
using SimpleTeam.Function;

namespace SimpleTeam.Container.Properties
{
    public class HashtablePropertiesContainer
        : SimplePropertiesContainer
    {
        private Hashtable properties;

        public HashtablePropertiesContainer()
            : this([])
        {

        }

        private HashtablePropertiesContainer(Hashtable properties)
        {
            //Set properties.
            this.properties = properties;
        }

        public int Size()
        {
            //Set size.
            SetSize(properties.Count);
            //Return size.
            return base.GetSize();
        }

        public void Clear()
        {
            //Clear properties.
            properties.Clear();
        }

        public override int TotalCount()
        {
            //Return size.
            return properties.Count;
        }

        public override bool Exists(string name)
        {
            //Check key.
            return properties.ContainsKey(name);
        }

        public override void Remove(string name)
        {
            //Remove a property.
            properties.Remove(name);
        }

        public override object? Get(string name)
        {
            //Return property.
            return properties[name];
        }

        public override void Set(string name, object? value)
        {
            //Check object.
            if (value == null)
            {
                //Remove object.
                properties.Remove(name);
            }
            else
            {
                //Set property.
                properties[name] = value;
            }
        }

        public void Dump()
        {
            //Create buffer.
            StringBuilder buffer = new ();
            //Get keys.
            foreach (string key in properties.Keys)
            {
                //Get element.
                object? value = properties[key];
                //Check result.
                if (value == null)
                {
                    //Print.
                    buffer.Append('\t').Append(key).Append(" = null").AppendLine();
                }
                else if (value.GetType() == typeof(bool))
			    {
                    //Print.
                    buffer.Append('\t').Append(key).Append(" = ").Append((bool)value).AppendLine();
                }
                else if (value.GetType() == typeof(byte))
                {
                    //Print.
                    buffer.Append('\t').Append(key).Append("(byte) = ").Append((byte)value).AppendLine();
                }
                else if (value.GetType() == typeof(short))
                {
                    //Print.
                    buffer.Append('\t').Append(key).Append("(short) = ").Append((short)value).AppendLine();
                }
                else if (value.GetType() == typeof(int))
                {
                    //Print.
                    buffer.Append('\t').Append(key).Append("(int) = ").Append((int)value).AppendLine();
                }
                else if (value.GetType() == typeof(long))
                {
                    //Print.
                    buffer.Append('\t').Append(key).Append("(long) = ").Append((long)value).AppendLine();
                }
                else if (value.GetType() == typeof(float))
                {
                    //Print.
                    buffer.Append('\t').Append(key).Append("(float) = ").Append((float)value).AppendLine();
                }
                else if (value.GetType() == typeof(double))
                {
                    //Print.
                    buffer.Append('\t').Append(key).Append("(double) = ").Append((double)value).AppendLine();
                }
                else if (value.GetType() == typeof(char))
			    {
                    //Print.
                    buffer.Append('\t').Append(key).Append("(char) = ").Append((char)value).AppendLine();
                }
                else if (value.GetType() == typeof(string))
                {
                    //Print.
                    buffer.Append('\t').Append(key).Append(" = \"").Append((string)value).Append('\"').AppendLine();
                }
                else if (value.GetType() == typeof(byte[]))
                {
                    //Print.
                    buffer.Append('\t').Append(key).Append(" = 0x").Append(HexFormat.ToString((byte[])value)).Append("\"").AppendLine();
                }
                else
                {
                    //Print.
                    buffer.Append('\t').Append(key).Append("(object) = ").Append(value.GetType().FullName).AppendLine();
                }
            }
            //Print.
            Console.Write(buffer.ToString());
    	}
    }
}
