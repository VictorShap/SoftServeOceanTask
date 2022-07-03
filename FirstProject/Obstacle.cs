using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstProject
{
    internal class Obstacle : Cell
    {
        public Obstacle(Coordinate coordinate) : base(coordinate)
        {
            image = '#';
        }
    }
}
