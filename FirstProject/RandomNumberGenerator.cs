﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
