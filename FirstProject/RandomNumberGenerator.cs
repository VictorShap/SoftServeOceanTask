using System;

namespace FirstProject
{
    internal class RandomNumberGenerator
    {
        public static readonly Random random;

        static RandomNumberGenerator()
        {
            random = new Random();
        }
    }
}
