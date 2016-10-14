using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalClusterProcessorClassLibrary
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
    public class CProcessorNodeCfg : CConfig
    {
        public CProcessorNodeCfg():base() { }
    }
    public class CClusterRT<TRT> where TRT : CRTInputNode, new()
    {
        public CClusterRT() { }
        CProcessorNodeCfg cfg;
        #region rtdata
        private TRT real_time_data;
        public TRT realTimeData
        {
            get { return real_time_data; }
            set { real_time_data = value; }
        }
        #endregion rtdata
        #region interface
        public virtual void Config(CProcessorNodeCfg cfg)
        {
            this.cfg = cfg;
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
        public virtual TRT GetRTData()
        {
            return this.realTimeData;
        }
        public virtual bool Exe(TRT realTimeData, ClusterCenter clustercenters)
        {
            bool result=true;
            int xclustercount=clustercenters.xC.Count();
            int x0clustercount = clustercenters.xC[0].Count();
            if((xclustercount>=1)&&(x0clustercount>=1)
            {
            }
            else
            {
            }

            return result;
        }
        public virtual void DeActivate()
        {
            return;
        }
        public virtual void DeInit()
        {
            this.realTimeData = null;
            return;
        }
        #endregion interface
    }
    public class CRTInputNode
    {
        public CRTInputNode() { }
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
    public class CHistoryInputNode
    {
        public CHistoryInputNode() { }
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
        where T : CHistoryInputNode, new()
        where TRT : CRTInputNode, new()
    {
        public CHistoryProcessor() { }
        #region cfg
        CProcessorNodeCfg cfg;
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
        public virtual void Config(CProcessorNodeCfg cfg)
        {
            //histData = new T();
            this.cfg = cfg;
            hd = new T();
            hd.CC = cfg.СlusterСenters;
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
        public virtual void DeActivate()
        {
            return;
        }
        public virtual void DeInit()
        {
            this.hd = null;
            return;
        }
        #endregion interface
        public virtual T GetHistoryData()
        {
            return hd;
        }
    }
    public class CProcessorNodeBase<T, TRT>
        where T : CHistoryInputNode, new()
        where TRT : CRTInputNode, new()
    {
        public CProcessorNodeBase() { }
        #region cfg
        CProcessorNodeCfg cfg;
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
        public T histData;
        //public ClusterCenter clusterCenters;
        private List<double> z;
        public List<double> Z
        {
            get { return z; }
            set { z = value; }
        }
        #endregion clusterizationdata
        #region interface
        public CHistoryProcessor<T, TRT> hp;
        public CClusterRT<TRT> crt;
        public Clusterization<T> clusterization;
        public virtual void Config(CProcessorNodeCfg cfg) {
            this.cfg = cfg;
            //histData = new T();
            hp = new CHistoryProcessor<T, TRT>();
            crt = new CClusterRT<TRT>();
            clusterization = new Clusterization<T>();
            //clusterCenters = new ClusterCenter();
            return;
        }
        public virtual void Init()
        {
            hp.Init();
            crt.Init();
            clusterization.Init();

            histData = this.hp.GetHistoryData();
            return;
        }
        public virtual void Activate()
        {
            hp.Activate();
            crt.Activate();
            clusterization.Activate();
            return;
        }
        //public virtual void Exe(TRT realTimeData, out T historyData)
        public virtual bool Exe(TRT realTimeData, out ClusterCenter clustercenters)
        {
            bool result = true;
            clustercenters=this.histData.CC;
            this.crt.Exe(realTimeData, clustercenters);
            //TODO
            clustercenters = this.GetClusterCenters();
            //historyData = this.GetHistoryData();
            return result;
        }
        public virtual bool Calc(TRT realTimeData, out ClusterCenter clustercenters)
        {
            bool result = true;
            //TODO
            clustercenters = this.GetClusterCenters();
            //historyData = this.GetHistoryData();
            return result;
        }
        public virtual void DeActivate()
        {
            hp.DeActivate();
            crt.DeActivate();
            clusterization.DeActivate();
            return;
        }
        public virtual void DeInit()
        {
            hp.DeInit();
            crt.DeInit();
            clusterization.DeInit();
            this.crt = null;
            this.hp = null;
            this.clusterization = null;
            return;
        }
        #endregion interface
        protected virtual T GetHistoryData()
        {
            return this.histData;
        }
        protected virtual ClusterCenter GetClusterCenters()
        {
            //return this.clusterCenters;
            return this.histData.CC;
        }
        public virtual void SupplyData(ref List<double> Z) { return; }
    }
    public class CProcessorNode<T, TRT> : CProcessorNodeBase<T,TRT>
        where T : CHistoryInputNode, new()
        where TRT : CRTInputNode, new()
    {
        public CProcessorNode():base() { }
        #region cfg
        CProcessorNodeCfg cfg;
        #endregion cfg
        #region interface
        public override void Config(CProcessorNodeCfg cfg)
        {
            this.cfg = cfg;
            base.Config(cfg);
            hp.Config(cfg);
            crt.Config(cfg);
            clusterization.Config(cfg);
            //histData = new T();
        }
        public override void Init()
        {
            base.Init();
            hp.Init();
            crt.Init();
            clusterization.Init();
        }
        public override void Activate()
        {
            base.Activate();
            hp.Activate();
            crt.Activate();
            clusterization.Activate();
        }
        //public override void Exe(TRT realTimeData, out T historyData)
        public override bool Exe(TRT realTimeData, out ClusterCenter clustercenters)
        {
            bool result = true;
            //base.Exe(realTimeData, out historyData);
            base.Exe(realTimeData, out clustercenters);
            hp.Exe();
            crt.Exe();

            histData = hp.GetHistoryData();
            this.histData = histData;
            Console.Write("X={0}    \nY= {1}         \n    Time = {2}",
                    this.histData.X,this.histData.Y, this.histData.T);
            return result;
        }
        public override void DeActivate()
        {
            base.DeActivate();
            hp.DeActivate();
            crt.DeActivate();
            clusterization.DeActivate();
        }
        public override void DeInit()
        {
            base.DeInit();
            hp.DeInit();
            crt.DeInit();
            clusterization.DeInit();
        }
        #endregion interface
        protected override T GetHistoryData()
        {
            //histData.X = X;
            //histData.Y = Y;
            //histData.T = Time;
            return this.histData;
        }
        public override void SupplyData(ref List<double> Z) { Z = this.Z; return; }
    }
    [Serializable]
    public class CConfig
    {
        public CConfig() {}
        public virtual void Config() { }
        #region ClusterProcessorCfg
        //private CConfig ccfg;// = new CConfig();
        //public CConfig CCfg
        //{
        //    get { return ccfg; }
        //    set { ccfg = value; }
        //}
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
        private string storage_name = "HistoryStorageName_FuncID.xml";
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

        private ClusterCenter cluster_centers = new ClusterCenter();
        public ClusterCenter СlusterСenters
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
