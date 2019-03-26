using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntLearnShared.Utils
{
    public class OwcRandom
    {
        private static Random _rnd = new Random();

        private static List<int> _uniqueValues = new List<int>();

        public static int GetRandom(int min, int max)
        {
            return _rnd.Next(min, max);
        }

        public static int GetRandom()
        {
            return _rnd.Next();
        }

        public static double GetRandom01()
        {
            return _rnd.NextDouble();
        }

        public static int GetUniqueValue()
        {
            int value;
            do
            {
                value = _rnd.Next();
            } while (_uniqueValues.Contains(value));

            _uniqueValues.Add(value);

            return value;
        }
    }
}
