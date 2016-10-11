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
    public class CClusterRT<TRT> where TRT : CRTInput, new()
    {
        public CClusterRT() { }
        #region rtdata
        private List<double> input;
        public List<double> Input
        {
            get { return input; }
            set { input = value; }
        }
        private List<double> output;
        public List<double> Output
        {
            get { return output; }
            set { output = value; }
        }
        private List<double> tm;
        public List<double> Tm
        {
            get { return tm; }
            set { tm = value; }
        }
        #endregion rtdata
        #region interface
        public virtual void Config(ClusterProcessorCfg cfg)
        {
            //input = new List<double>();
            //output = new List<double>();
            //tm = new List<double>();
        }
        public virtual void Init()
        {
        }
        public virtual void Activate()
        {
        }
        public virtual void GetRTData(ref TRT procData)
        {
            procData.Input = this.Input;
            procData.Output = this.Output;
            procData.Tm = this.Tm;
            return;
        }
        public virtual void Exe()
        {
            return;
        }
        #endregion interface
    }
    public class CRTInput
    {
        public CRTInput() { }
        public virtual void Config() { return; }
        #region rtdata
        private List<double> input;
        public List<double> Input
        {
            get { return input; }
            set { input = value; }
        }
        private List<double> output;
        public List<double> Output
        {
            get { return output; }
            set { output = value; }
        }
        private List<double> tm;
        public List<double> Tm
        {
            get { return tm; }
            set { tm = value; }
        }
        #endregion rtdata
    }
    [Serializable]
    public class CHistoryInput
    {
        public CHistoryInput() { }
        public void Config() { return; }
        #region clusterizationdata
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
        #endregion clusterizationdata
    }
    public class CHistoryProcessor<T, TRT>
        where T : CHistoryInput, new()
        where TRT : CRTInput, new()
    {
        public CHistoryProcessor() { }
        #region cfg
        ClusterProcessorCfg cfg;
        #endregion cfg
        #region clusterizationdata
        T hd;
        private List<double> z;
        public List<double> Z
        {
            get { return z; }
            set { z = value; }
        }
        #endregion clusterizationdata
        #region interface
        public virtual void Config(ClusterProcessorCfg cfg)
        {
            //histData = new T();
            this.cfg = cfg;
            hd = new T();
        }
        public virtual void Init()
        {
        }
        public virtual void Activate()
        {
        }
        public virtual void Exe()
        {
            List<double> entry = new List<double>();
            entry.Add(11);
            hd.X.Add(entry);
            hd.Y.Add(entry);
            hd.T.Add(11);
            entry.Add(111);
            hd.X.Add(entry);
            hd.Y.Add(entry);
            hd.T.Add(111);
        }
        #endregion interface
        public virtual void GetHistoryData(ref T histData)
        {
            histData.X = hd.X;
            histData.Y = hd.Y;
            histData.T = hd.T;
            return;
        }
    }
    public class CProcessorBase<T> where T : CHistoryInput, new()
    {
        public CProcessorBase() { }
        #region cfg
        ClusterProcessorCfg cfg;
        #endregion cfg
        #region rtdata
        private List<double> input;
        public List<double> Input
        {
            get { return input; }
            set { input = value; }
        }
        private List<double> output;
        public List<double> Output
        {
            get { return output; }
            set { output = value; }
        }
        private List<double> tm;
        public List<double> Tm
        {
            get { return tm; }
            set { tm = value; }
        }
        #endregion rtdata
        #region clusterizationdata
        private List<List<double>> x;
        public List<List<double>> X
        {
            get { return x; }
            set { x = value; }
        }
        private List<List<double>> y;
        public List<List<double>> Y
        {
            get { return y; }
            set { y = value; }
        }
        private List<double> t;
        public List<double> Time
        {
            get { return t; }
            set { t = value; }
        }
        private List<double> z;
        public List<double> Z
        {
            get { return z; }
            set { z = value; }
        }
        #endregion clusterizationdata
        #region interface
        public virtual void Config(ClusterProcessorCfg cfg) { this.cfg = cfg; return; }
        public virtual void Init() { return; }
        public virtual void Activate() { return; }
        public virtual void Exe() { return; }
        public virtual void DeInit() { return; }
        public virtual void DeActivate() { return; }
        #endregion interface
        public virtual void GetHistoryData(ref T histData) { return; }
        public virtual void SupplyData(ref List<double> Z) { return; }
    }
    public class CProcessor<T, TRT> : CProcessorBase<T>
        where T : CHistoryInput, new()
        where TRT : CRTInput, new()
    {
        public CProcessor() { }
        #region cfg
        ClusterProcessorCfg cfg;
        #endregion cfg
        T histData;
        CHistoryProcessor<T, TRT> hp;
        CClusterRT<TRT> crt;
        #region interface
        public override void Config(ClusterProcessorCfg cfg)
        {
            base.Config(cfg);
            hp = new CHistoryProcessor<T, TRT>();
            crt = new CClusterRT<TRT>();

            hp.Config(cfg);
            crt.Config(cfg);
            this.cfg = cfg;
            histData = new T();
        }
        public override void Init()
        {
            base.Init();
            hp.Init();
            crt.Init();
        }
        public override void Activate()
        {
            base.Activate();
            hp.Activate();
            crt.Activate();
        }
        public override void Exe()
        {
            base.Exe();
            hp.Exe();
            crt.Exe();

            hp.GetHistoryData(ref histData);
            X = histData.X;
            Y = histData.Y;
            Time = histData.T;
            Console.Write("X={0} \n Y= {1} \n Time = {2}", X, Y, Time);
        }
        #endregion interface
        public override void GetHistoryData(ref T histData)
        {
            histData.X = X;
            histData.Y = Y;
            histData.T = Time;
            return;
        }
        public override void SupplyData(ref List<double> Z) { Z = this.Z; return; }
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
    public class BaseMath
    {
        public BaseMath() { }
        public virtual double Sum(List<double> vec)
        {
            double result = 0;

            for (int i = 0; i < vec.Count; i++)
            {
                result += vec[i];
            }
            return result;
        }
        public virtual double Average(List<double> vec)
        {
            double sum = Sum(vec);
            double result = sum / vec.Count;
            return result;
        }
        public virtual double StdDev(ref List<double> vec, double mean)
        {
            List<double> vecCur = new List<double>(vec.Count);
            for (int i = 0; i < vec.Count; i++) { vecCur.Add(Math.Pow((vec[i] - mean), 2)); }
            double variances = Sum(vecCur) / (vec.Count - 1);
            double result = Math.Sqrt(variances);
            return result;
        }
    }
}
