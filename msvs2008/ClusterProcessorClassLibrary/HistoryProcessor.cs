using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClusterProcessorClassLibrary
{
    public class HistoryProcessor
    {
        #region HistoryProcessorCfg
            private List<string> storage_name = new List<string>();
            public List<string> StorageName
            {
                get { return storage_name; }
                set { storage_name = value; }
            }
            private List<long> requireed_records_number_for_clusterization = new List<long>();
            public List<long> RequireedRecordsNumberForClusterization
            {
                get { return requireed_records_number_for_clusterization; }
                set { requireed_records_number_for_clusterization = value; }
            }
        #endregion HistoryProcessorCfg
    }
}
