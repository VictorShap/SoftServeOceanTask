
namespace OceanSimulationInConsole
{
    internal abstract class Cell
    {
        #region Fields
        protected readonly IOcean _owner;
        protected char _image;
        protected bool _wasIterated = false;
        private Coordinate _offset;
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

        #region Ctors
        protected Cell(Coordinate coordinate, IOcean ocean)
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
