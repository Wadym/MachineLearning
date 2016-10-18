using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

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
    public class CClusterRT<TRT,T>
        where TRT : CRTInputNode, new()
        where T : CHistoryInputNode, new()
    {
        public CClusterRT() { }
        CProcessorNodeCfg cfg;
        #region rtdata
        Thread th;
        public Semaphore sem = new Semaphore(0, 10000);
        bool deinit = false;
        private Clusterization<TRT, T> _clusterization;
        public Clusterization<TRT, T> clusterizationobj
        {
            get { return _clusterization; }
            set { _clusterization = value; }
        }
        private CHistoryProcessor<T, TRT> _hp;
        public CHistoryProcessor<T, TRT> hp
        {
            get { return _hp; }
            set { _hp = value; }
        }
        private TRT real_time_data;
        public TRT realTimeData
        {
            get { return real_time_data; }
            set { real_time_data = value; }
        }
        private T history_data;
        public T historyData
        {
            get { return history_data; }
            set { history_data = value; }
        }
        #endregion rtdata
        #region interface
        public virtual TRT GetRTData()
        {
            return this.real_time_data;
        }
        public virtual void Config(CProcessorNodeCfg cfg)
        {
            this.cfg = cfg;
            real_time_data = new TRT();
            //input = new List<double>();
            //output = new List<double>();
            //tm = new List<double>();
        }
        public virtual void Init()
        {
            this.th = new Thread(new ThreadStart(ClusterizationTh));
            this.th.Start();
            //record_number_for_clusterization = this.history_data.T.Count();
        }
        public virtual void Activate()
        {
        }
        public virtual bool CalcRT(CProcessorNodeBase<T, TRT> context)
        {
            bool result=true;
            //TODO

            //TODO Output from CalcRT should be defined yet
            return result;
        }
        public virtual void ClusterizationTh()
        {
            while (!this.deinit)
            {
                bool res = sem.WaitOne();
                if (!this.deinit)
                {
                    historyData = hp.GetHistoryData();
                    
                    clusterizationobj.Exe(historyData);
                    hp.historyData.CC = clusterizationobj.GetClusterCenters();
                }
            }
            //Clusterization.Exe(historyData);
            return;
        }
        //public virtual bool Exe(ref TRT realTimeData, ref ClusterCenter clustercenters, ref Clusterization<T> clusterizationobj, ref T historyData)
        public virtual bool Exe(CProcessorNodeBase<T, TRT> context)
        {
            bool result=true;
            result = (context.historyData.CC.xC.Count() == context.historyData.CC.tC.Count()) && (context.historyData.CC.xC.Count() == context.historyData.CC.yC.Count()) ? true : false;
            int xclustercount = context.historyData.CC.xC.Count();
            int x0clustercount = context.historyData.CC.xC[0].Count();
            if((xclustercount>=1)&&(x0clustercount>=1))
            {
                //CalcRT(ref realTimeData, ref clustercenters, ref historyData); //context.historyData.CC.
                result = CalcRT(context);
            }
            else
            {
                if (((context.historyData.T.Count() > cfg.RequiredRecordsNumberForInitialClusterization) || (context.historyData.CC.tC.Count() >= 1))
                    && (context.historyData.T.Count() > cfg.RequiredRecordsNumberForClusterization))
                {
                    context.historyData = context.hp.GetHistoryData();
                    sem.Release();
                    //Thread clusterizationThread = new Thread(new ThreadStart(ClusterizationTh));
                    //if (!clusterizationThread.IsAlive)
                    //{
                    //    clusterizationThread.Start();
                    //}
                }
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
            deinit = true;
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
        private double tm;
        public double Tm
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
        private T history_data;
        public T historyData
        {
            get { return history_data; }
            set { history_data = value;}
        }
        private TRT real_time_data;
        public TRT realTimeData
        {
            get { return real_time_data; }
            set {}
        }

        private Queue<TRT> qeueu;
        public Queue<TRT> QeUeU
        {
            get { return qeueu; }
            set { qeueu = value; }
        }
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
            history_data = new T();
            history_data.CC = cfg.СlusterСenters;
            qeueu = new Queue<TRT>();
        }
        public virtual void Init()
        {
        }
        public virtual void Activate()
        {
        }
        public virtual bool Exe(CProcessorNodeBase<T, TRT> context)
        {
            bool result = true;
            this.real_time_data = context.realTimeData;
            if (QeUeU.Count() >= cfg.MaxRecordsNumberForClusterization)
            {
                foreach( List<double> e in historyData.X) { e.Clear(); }
                foreach (List<double> e in historyData.Y) { e.Clear(); }
                historyData.T.Clear();
                foreach (TRT e in QeUeU)
                {
                    historyData.X.Add(e.Input);
                    historyData.Y.Add(e.Output);
                    historyData.T.Add(e.Tm);
                }
                context.crt.historyData = historyData;

                context.crt.sem.Release();
            }
            QeUeU.Enqueue(realTimeData);

            return result;
        }
        public virtual void DeActivate()
        {
            return;
        }
        public virtual void DeInit()
        {
            return;
        }
        #endregion interface
        public virtual T GetHistoryData()
        {
            return historyData;
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
        private List<double> target;
        public List<double> Target
        {
            get { return target; }
            set { target = value; }
        }
        #endregion rtdata
        #region clusterizationdata
        private T history_data;
        public T historyData
        {
            get { return history_data; }
            set { history_data = value; }
        }
        private TRT real_time_data;
        public TRT realTimeData
        {
            get { return real_time_data; }
            set { real_time_data = value; }
        }

        #endregion clusterizationdata
        #region interface
        public CHistoryProcessor<T, TRT> hp;
        public CClusterRT<TRT,T> crt;
        public Clusterization<TRT, T> clusterizationobj;
        public virtual void Config(CProcessorNodeCfg cfg)
        {
            this.cfg = cfg;
            crt = new CClusterRT<TRT,T>();
            crt.Config(cfg);
            //histData = new T();
            hp = new CHistoryProcessor<T, TRT>();
            hp.Config(cfg);
            clusterizationobj = new Clusterization<TRT, T>();
            clusterizationobj.Config(cfg);
            //clusterCenters = new ClusterCenter();
            return;
        }
        public virtual void Init()
        {
            crt.Init();
            real_time_data=crt.GetRTData();
            hp.Init();
            history_data = hp.GetHistoryData();
            clusterizationobj.Init();
            crt.clusterizationobj = clusterizationobj;
            crt.hp = hp;
            return;
        }
        public virtual void Activate()
        {
            hp.Activate();
            crt.Activate();
            clusterizationobj.Activate();
            return;
        }
        //public virtual void Exe(TRT realTimeData, out T historyData)
        public virtual bool Exe(TRT realTimeData, out ClusterCenter clustercenters)
        {
            bool result = true;
            
            this.realTimeData = realTimeData;

            history_data = hp.GetHistoryData();
            clustercenters = history_data.CC;
            result = this.crt.Exe(this);//ref realTimeData, ref clustercenters, ref clusterizationobj, ref history_data);
            //TODO
            hp.Exe(this);

            clustercenters = crt.historyData.CC;// hp.GetHistoryData().CC;
            //historyData = this.GetHistoryData();
            return result;
        }
        public virtual bool Calc(TRT realTimeData, out ClusterCenter clustercenters)
        {
            bool result = true;
            //TODO
            clustercenters = crt.historyData.CC;// this.GetClusterCenters();
            //historyData = this.GetHistoryData();
            return result;
        }
        public virtual void DeActivate()
        {
            hp.DeActivate();
            crt.DeActivate();
            clusterizationobj.DeActivate();
            return;
        }
        public virtual void DeInit()
        {
            hp.DeInit();
            crt.DeInit();
            clusterizationobj.DeInit();
            return;
        }
        #endregion interface
        protected virtual T GetHistoryData()
        {
            return this.historyData;
        }
        protected virtual ClusterCenter GetClusterCenters()
        {
            return this.historyData.CC;
        }
        public virtual void SupplyData(ref List<double> Z) { return; }
    }
    public class CProcessorNode<T, TRT> : CProcessorNodeBase<T,TRT>
        where T : CHistoryInputNode, new()
        where TRT : CRTInputNode, new()
    {
        public CProcessorNode():base() { }
        #region interface
        #endregion interface
    }
    [Serializable]
    public class CConfig
    {
        public CConfig() {}
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
        private string storage_name = "HistoryStorageName_FuncID.xml";
        public string StorageName
        {
            get { return storage_name; }
            set { storage_name = value; }
        }
        private long required_records_number_for_clusterization;
        public long RequiredRecordsNumberForClusterization
        {
            get { return required_records_number_for_clusterization; }
            set { required_records_number_for_clusterization = value; }
        }
        private long required_records_number_for_initial_clusterization = 7 * 24 * 60 * 60 / 2;
        public long RequiredRecordsNumberForInitialClusterization
        {
            get { return required_records_number_for_initial_clusterization; }
            set { required_records_number_for_initial_clusterization = value; }
        }
        private long max_records_number_for_clusterization;
        public long MaxRecordsNumberForClusterization
        {
            get { return max_records_number_for_clusterization; }
            set { max_records_number_for_clusterization = value; }
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
