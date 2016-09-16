using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClusterProcessorClassLibrary
{
    public class ClusterProcessorCfg
    {
        public virtual ClusterProcessorCfg(){ }
        #region ClusterProcessorCfg
        private int number_of_clusters = 100;
        public int NumberOfClusters
        {
            get { return number_of_clusters; }
            set { number_of_clusters = value; }
        }
        private string funcid = "functionname";
        public string FuncID
        {
            get { return funcid; }
            set { funcid = value; }
        }
        private double alpha = 0.5;
        public double Alpha
        {
            get { return alpha; }
            set { alpha = value; }
        }
        private double beta = 0.1;
        public double Beta
        {
            get { return beta; }
            set { beta = value; }
        }
        private int number_of_inputs = 1;
        public int NumberOfInputs
        {
            get { return number_of_inputs; }
            set { number_of_inputs = value; }
        }
        private List<int> indices_of_inputs = new List<int>();
        public List<int> IndicesOfInputs
        {
            get { return indices_of_inputs; }
            set { indices_of_inputs = value; }
        }
        private int index_of_output;
        public int IndexOfOutput
        {
            get { return index_of_output; }
            set { index_of_output = value; }
        }
        private string cluster_centers_storage_name = "ClusterCenters.xml";
        public string ClusterCentersStorageName
        {
            get { return cluster_centers_storage_name; }
            set { cluster_centers_storage_name = value; }
        }
        private int iteration_max = 100;
        public int IterationMax
        {
            get { return iteration_max; }
            set { iteration_max = value; }
        }
        private double delta_err_max = 0.05;
        public double DeltaErrMax
        {
            get { return delta_err_max; }
            set { delta_err_max = value; }
        }
        #endregion ClusterProcessorCfg
    }
    public class ClusterProcessor
    {
        ClusterProcessorCfg cfg = new ClusterProcessorCfg();
        public ClusterProcessor() { }
        public ClusterProcessor(ClusterProcessorCfg cfg)
        {
            this.cfg = cfg;
        }
        #region ClusterProcessorCfg
            string pathtomathlib = "..\\..\\..\\..\\Debug\\mathdll.dll";
        #endregion ClusterProcessorCfg
    }
}
