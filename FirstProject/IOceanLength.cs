using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OceanSimulationInConsole
{
    internal interface IOceanLength : IOceanIndexer
    {
        int NumColumns { get; }
        int NumRows { get; }
    }
}
