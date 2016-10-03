using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClusterProcessorClassLibrary;

namespace CPE_ClusterProcessorClassLibrary
{
    [Serializable]
    public class CPE_ClusterProcessorNodeCfg
    {
        public CPE_ClusterProcessorNodeCfg()
        { 
        }
        private List<ClusterProcessorCfg> cp_cfg=new List<ClusterProcessorCfg>();
        public List<ClusterProcessorCfg> CPCfg
        {
            get { return cp_cfg; }
            set { cp_cfg = value; }
        }
        private List<int> inputs_array_index=new List<int>();
        public List<int> InputsArrayIndex
        {
            get { return inputs_array_index; }
            set { inputs_array_index = value; }
        }
        private List<int> outputs_array_index;
        public List<int> OutputsArrayIndex
        {
            get { return outputs_array_index; }
            set { outputs_array_index = value; }
        }
        private int node_decimation = 1;
        public int NodeDecimation
        {
            get { return node_decimation; }
            set { node_decimation = value; }
        }
        private double clusterization_update_interval = 1 * 60 * 60 * 1000;
        public double ClusterizationUpdateInterval
        {
            get { return clusterization_update_interval; }
            set { clusterization_update_interval = value; }
        }
        public ClusterCenter cluster_center = new ClusterCenter();
        private ClusterCenter ClusterCenter
        {
            get { return cluster_center; }
            set { cluster_center = value; }
        }
        //private bool do_clusterization_on_start = true;
        //public bool DoClusterizationOnStart
        //{
        //    get { return do_clusterization_on_start; }
        //    set { do_clusterization_on_start = value; }
        //}
    }
    public class CPE_ClusterProcessorNode
    {
        CPE_ClusterProcessorNodeCfg cfg = new CPE_ClusterProcessorNodeCfg();
        public CPE_ClusterProcessorNode()
        {
        }
        public CPE_ClusterProcessorNode(CPE_ClusterProcessorNodeCfg cfg)
        {
            this.cfg = cfg;
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

    }
}
