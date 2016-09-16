using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClusterProcessorClassLibrary
{
    public class CPE_ClusterProcessorNodeCfg
    {
        public virtual CPE_ClusterProcessorNodeCfg()
        { 
        }
        #region CPE_ClusterProcessorNodeCfg
        private List<int> number_of_clusters = new List<int>();
        public List<int> NumberOfClusters
        {
            get { return number_of_clusters; }
            set { number_of_clusters = value; }
        }
        private List<string> funcid = new List<string>();
        public List<string> FuncID
        {
            get { return funcid; }
            set { funcid = value; }
        }
        private List<double> alpha = new List<double>();
        public List<double> Alpha
        {
            get { return alpha; }
            set { alpha = value; }
        }
        private List<double> beta = new List<double>();
        public List<double> Beta
        {
            get { return beta; }
            set { beta = value; }
        }
        private List<int> number_of_inputs = new List<int>();
        public List<int> NumberOfInputs
        {
            get { return number_of_inputs; }
            set { number_of_inputs = value; }
        }
        private List<List<int>> indices_of_inputs = new List<List<int>>();
        public List<List<int>> IndicesOfInputs
        {
            get { return indices_of_inputs; }
            set { indices_of_inputs = value; }
        }
        private List<int> index_of_output = new List<int>();
        public List<int> IndexOfOutput
        {
            get { return index_of_output; }
            set { index_of_output = value; }
        }
        private List<string> cluster_centers_storage_name = new List<string>();
        public List<string> ClusterCentersStorageName
        {
            get { return cluster_centers_storage_name; }
            set { cluster_centers_storage_name = value; }
        }
        private List<int> iteration_max = new List<int>();
        public List<int> IterationMax
        {
            get { return iteration_max; }
            set { iteration_max = value; }
        }
        private List<double> delta_err_max = new List<double>();
        public List<double> DeltaErrMax
        {
            get { return delta_err_max; }
            set { delta_err_max = value; }
        }
        #endregion CPE_ClusterProcessorNodeCfg
    }
    public class CPE_ClusterProcessorNode
    {
        CPE_ClusterProcessorNodeCfg cfg = new CPE_ClusterProcessorNodeCfg();
        public virtual CPE_ClusterProcessorNode()
        {
        }
        public virtual CPE_ClusterProcessorNode(CPE_ClusterProcessorNodeCfg cfg)
        {
            this.cfg = cfg;
        }
    }
}
