﻿
namespace OceanSimulationInConsole
{
    internal abstract class Cell
    {
        #region Fields
        protected readonly IOcean _owner;
        protected char _image;
        private Coordinate _offset;

        protected bool _wasIterated = false;
        #endregion

        #region Properties
        public Coordinate Offset
        {
            get
            {
                return _offset;
            }
            set
            {
                _offset = value;
            }
        }
        public char Image
        {
            get => _image;
        }
        public bool WasIterated { get => _wasIterated; set => _wasIterated = value; }
        #endregion

        #region CTORS
        public Cell(Coordinate coordinate, IOcean ocean)
        {
            _owner = ocean;
            _offset = coordinate;
        }
        #endregion

        #region Methods
        protected abstract Cell Reproduce(Coordinate coordinate);

        public abstract void Process();
        #endregion
    }
}
