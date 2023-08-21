using System.ComponentModel;

namespace VirtualMind.Test.Library.Extensions
{
    public static class DictionaryExtensions
    {
        public static IDictionary<string, string> ToDictionary(this object source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var dictionary = new Dictionary<string, string>();
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(source))
            {
                var value = property.GetValue(source);

                if (value != null)
                {
                    dictionary.Add(property.Name, value.ToString());
                }
            }

            return dictionary;
        }
    }
}
