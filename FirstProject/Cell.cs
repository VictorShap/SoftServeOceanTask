
namespace FirstProject
{
    internal abstract class Cell
    {
        #region Fields
        protected readonly Ocean _owner;
        protected char _image;
        private Coordinate _offset;

        public bool wasIterated = false;
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
        #endregion

        #region CTORS
        public Cell(Coordinate coordinate, Ocean ocean)
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
