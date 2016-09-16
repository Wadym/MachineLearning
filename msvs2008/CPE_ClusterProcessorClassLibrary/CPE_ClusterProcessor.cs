using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClusterProcessorClassLibrary;

namespace CPE_ClusterProcessorClassLibrary
{
    public class CPE_ClusterProcessorCfg
    {
        #region CPE_ClusterProcessorCfg
        private List<CPE_ClusterProcessorNode> mdl = new List<CPE_ClusterProcessorNode>();
        public List<CPE_ClusterProcessorNode> ClusterProcessorNode
        {
            get { return mdl; }
            set { mdl = value; }
        }
        private ClusterProcessor cluster_processor = new ClusterProcessor();
        public ClusterProcessor Cluster_Processor
        {
            get { return cluster_processor; }
            set { cluster_processor = value; }
        }
        private HistoryProcessor history_processor = new HistoryProcessor();
        public HistoryProcessor Hystory_Processor
        {
            get { return history_processor; }
            set { history_processor = value; }
        }
        #endregion CPE_ClusterProcessorCfg
        #region External_CPE_ClusterProcessorCfg
        private double updaterate = 10000;
        public double UpdateRate
        {
            get { return updaterate; }
            set { updaterate = value; }
        }
        private bool do_clusterization_on_start = true;
        public bool DoClusterizationOnStart
        {
            get { return do_clusterization_on_start; }
            set { do_clusterization_on_start = value; }
        }
        private long requireed_records_number_for_clusterization = 40000;
        public long RequireedRecordsNumberForClusterization
        {
            get { return requireed_records_number_for_clusterization; }
            set { requireed_records_number_for_clusterization = value; }
        }
        #endregion External_CPE_ClusterProcessorCfg
    }
    public class CPE_ClusterProcessor
    {
        CPE_ClusterProcessorCfg cfg = new CPE_ClusterProcessorCfg();
        public virtual CPE_ClusterProcessor()
        { 
        }
        public virtual CPE_ClusterProcessor(CPE_ClusterProcessorCfg cfg)
        {
            this.cfg = cfg;
        }
    }
}
