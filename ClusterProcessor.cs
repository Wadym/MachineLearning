using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalClusterProcessorClassLibrary;

namespace ClusterProcessorClassLibrary
{
    [Serializable]
    public class CProcessorCfg
    {
        public CProcessorCfg()
        { 
        }
        private List<CProcessorNodeCfg> nodes = new List<CProcessorNodeCfg>();
        public List<CProcessorNodeCfg> Nodes
        {
            get { return nodes; }
            set { nodes = value; }
        }
        private List<List<int>> inputs_array_index=new List<List<int>>();
        public List<List<int>> InputsArrayIndex
        {
            get { return inputs_array_index; }
            set { inputs_array_index = value; }
        }
        private List<List<int>> outputs_array_index;
        public List<List<int>> OutputsArrayIndex
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
        //public ClusterCenter cluster_center = new ClusterCenter();
        //private ClusterCenter ClusterCenter
        //{
        //    get { return cluster_center; }
        //    set { cluster_center = value; }
        //}

        //private bool do_clusterization_on_start = true;
        //public bool DoClusterizationOnStart
        //{
        //    get { return do_clusterization_on_start; }
        //    set { do_clusterization_on_start = value; }
        //}
    }
    public class CProcessor
    {
        CProcessorCfg cfg = new CProcessorCfg();
        public CProcessor()
        {
        }
        public CProcessor(CProcessorCfg cfg)
        {
            this.cfg = cfg;
        }
        private CProcessor cluster_processor = new CProcessor();
        public CProcessor Cluster_Processor
        {
            get { return cluster_processor; }
            set { cluster_processor = value; }
        }
        private List<CProcessorNode<CHistoryInputNode, CRTInputNode>> mdl = new List<CProcessorNode<CHistoryInputNode, CRTInputNode>>();
        public List<CProcessorNode<CHistoryInputNode, CRTInputNode>> CProcessorNode
        {
            get { return mdl; }
            set { mdl = value; }
        }
    }
}
