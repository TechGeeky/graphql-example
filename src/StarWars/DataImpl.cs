using System;

namespace StarWars
{
    public class DataImpl : IData
    {
        private string configName = "abc.json";
        private static readonly object _propertyDisplayConfigLock = new object();
        private static bool _isInitialized = false;

        public DataImpl()
        {
            Console.WriteLine("In");
            if (!_isInitialized)
            {
                lock (_propertyDisplayConfigLock)
                {
                    if (!_isInitialized)
                    {
                        Console.WriteLine("Hello World: " + configName);
                        _isInitialized = true;
                    }
                }
            }
        }

        public int TestMethod(int x)
        {
            return x + 2;
        }
    }
}
