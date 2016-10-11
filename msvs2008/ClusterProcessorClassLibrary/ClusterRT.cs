using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClusterProcessorClassLibrary
{
    class ClusterRT<T> : Cluster<T> where T : CHistoryInput, new()
    {
    }
}
