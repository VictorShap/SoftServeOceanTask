
namespace OceanSimulationInConsole
{
    internal abstract class Cell
    {
        #region Readonly
        protected readonly IOcean _owner; // the ocean to which the cell belongs
        #endregion

        #region Fields
        private Coordinate _offset; // the position of the cell in the ocean 
        protected char _image; // the image of the cell that is displayed in the console
        #endregion

        #region Properties
        public Coordinate Offset
        {
            protected get
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

        public void Accept(ICellVisitor cellVisitor)
        {
            cellVisitor.Visit(this);
        }
        #endregion
    }
}
