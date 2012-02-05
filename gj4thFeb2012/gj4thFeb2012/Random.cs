using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gj4thFeb2012
{
    public static class Rng
    {
        private static Random _rng = new Random();

        public static int Next()
        {
            return _rng.Next();
        }

        public static int Next(int max)
        {
            return _rng.Next(max);
        }

        public static int Next(int min, int max)
        {
            return _rng.Next(min, max);
        }
    }
}
