using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstProject
{
    internal abstract class Cell
    {
        protected readonly Ocean _owner;
        protected char _image;
        private Coordinate _offset;

        public bool isBeenIterated = false;

        public Coordinate Offset
        {
            get
            {
                return _offset;
            }
            protected set
            {
                _offset = value;
            }
        }
        public char Image
        {
            get => _image;
        }

        public Cell(Coordinate coordinate, Ocean ocean)
        {
            _owner = ocean;
            _offset = coordinate;
        }

        protected abstract Cell Reproduce(Coordinate coordinate);

        public abstract void Process();

        public void Display()
        {
            Console.Write(Image);
        }
    }
}
