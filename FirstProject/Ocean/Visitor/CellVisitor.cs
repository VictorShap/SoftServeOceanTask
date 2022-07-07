using System.Collections.Generic;

namespace OceanSimulationInConsole
{
    internal class CellVisitor : ICellVisitor
    {
        private HashSet<int> _iteratedCells;

        public CellVisitor()
        {
            _iteratedCells = new HashSet<int>();
        }

        public void Visit(Cell cell)
        {
            if (_iteratedCells.Contains(cell.GetHashCode()))
            {
                return;
            }

            _iteratedCells.Add(cell.GetHashCode());
            cell.Process();
        }
    }
}
