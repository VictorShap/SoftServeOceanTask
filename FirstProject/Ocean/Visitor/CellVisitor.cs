using System.Collections.Generic;

namespace OceanSimulationInConsole
{
    internal class CellVisitor : ICellVisitor
    {
        #region Fields
        private HashSet<int> _iteratedCells; // set of hash codes of cells that have been iterated
        #endregion

        #region Ctors
        public CellVisitor()
        {
            _iteratedCells = new HashSet<int>();
        }
        #endregion

        #region Methods
        public void Visit(Cell cell)
        {
            if (_iteratedCells.Contains(cell.GetHashCode()))
            {
                return;
            }

            _iteratedCells.Add(cell.GetHashCode());
            cell.Process();
        }
        #endregion
    }
}
