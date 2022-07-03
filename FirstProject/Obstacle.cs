using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstProject
{
    internal class Obstacle : Cell
    {
        public Obstacle(Coordinate coordinate, Ocean ocean) : base(coordinate, ocean)
        {
            _image = '#';
        }

        public override void Process()
        {

        }

        protected override Cell Reproduce(Coordinate coordinate)
        {
            return this;
        }
    }
}
