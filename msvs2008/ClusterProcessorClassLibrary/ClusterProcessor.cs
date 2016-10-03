using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClusterProcessorClassLibrary
{
    [Serializable]
    public class ClusterCenter
    {
        private List<List<double>> XC = new List<List<double>>();
        public List<List<double>> xC
        {
            get { return XC; }
            set { XC = value; }
        }
        private List<List<double>> YC = new List<List<double>>();
        public List<List<double>> yC
        {
            get { return YC; }
            set { YC = value; }
        }
        private List<double> TC = new List<double>();
        public List<double> tC
        {
            get { return TC; }
            set { TC = value; }
        }
    }
    [Serializable]
    public class ClusterProcessorCfg : CConfig
    {
        public ClusterProcessorCfg(){ }
    }
    public class ClusterProcessor
    {
        ClusterProcessorCfg cfg = new ClusterProcessorCfg();
        public ClusterProcessor() { }
        public ClusterProcessor(ClusterProcessorCfg cfg)
        {
            this.cfg = cfg;
        }
    }
    [Serializable]
    public class CConfig
    {
        public CConfig() { }
        public virtual void Config() { }
        #region ClusterProcessorCfg
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
        private string cluster_centers_storage_name = "ClusterCenters_FuncID.xml";
        public string ClusterCentersStorageName
        {
            get { return cluster_centers_storage_name; }
            set { cluster_centers_storage_name = value; }
        }
        private int number_of_clusters = 100;
        public int NumberOfClusters
        {
            get { return number_of_clusters; }
            set { number_of_clusters = value; }
        }
        private double beta = 0.1;
        public double Beta
        {
            get { return beta; }
            set { beta = value; }
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
        private string storage_name;
        public string StorageName
        {
            get { return storage_name; }
            set { storage_name = value; }
        }
        private long requireed_records_number_for_clusterization;
        public long RequireedRecordsNumberForClusterization
        {
            get { return requireed_records_number_for_clusterization; }
            set { requireed_records_number_for_clusterization = value; }
        }
        private long requireed_records_number_for_initial_clusterization = 7 * 24 * 60 * 60 / 2;
        public long RequireedRecordsNumberForInitialClusterization
        {
            get { return requireed_records_number_for_initial_clusterization; }
            set { requireed_records_number_for_initial_clusterization = value; }
        }
        private long max_records_number_for_clusterizationmyVar;
        public long MaxRecordsNumberForClusterization
        {
            get { return max_records_number_for_clusterizationmyVar; }
            set { max_records_number_for_clusterizationmyVar = value; }
        }

        private List<ClusterCenter> cluster_centers = new List<ClusterCenter>();
        public List<ClusterCenter> СlusterСenters
        {
            get { return cluster_centers; }
            set { cluster_centers = value; }
        }
 
        
        #endregion ClusterProcessorCfg
    }
    public class InputMapper : BaseMath
    {
        public InputMapper() { }
        public virtual bool Init() { return true; }
        public virtual bool Config() { return true; }
        public virtual bool Process()
        {
            return true;
        }
        private List<List<double>> x = new List<List<double>>();
        public List<List<double>> X
        {
            get { return x; }
            set { x = value; }
        }
        private List<List<double>> y = new List<List<double>>();
        public List<List<double>> Y
        {
            get { return y; }
            set { y = value; }
        }
        private List<double> t = new List<double>();
        public List<double> T
        {
            get { return t; }
            set { t = value; }
        }
        private ClusterCenter cc = new ClusterCenter();
        public ClusterCenter CC
        {
            get { return cc; }
            set { cc = value; }
        }
    }
}
