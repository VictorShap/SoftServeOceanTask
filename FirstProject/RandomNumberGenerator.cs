using System;

namespace FirstProject
{
    internal class RandomNumberGenerator
    {
        #region Readonly
        public static readonly Random random;
        #endregion

        #region CTORS
        static RandomNumberGenerator()
        {
            random = new Random();
        }
        #endregion
    }
}
