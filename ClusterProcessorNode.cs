using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using System.Diagnostics;

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
        public override string ToString()
        {
            string CCState;
            CCState="";
            if ((TC.Count != 0) && (XC.Count != 0) && (YC.Count != 0))
            {
                //lock (TC)
                {
                    //lock (XC)
                    {
                        //lock (YC)
                        {
                            foreach (double t in TC) { CCState += t.ToString() + ";"; }
                            foreach (List<double> exc in XC) { foreach (double dblxc in exc) { CCState += dblxc.ToString() + ";"; } }
                            foreach (List<double> eyc in YC) { foreach (double dblyc in eyc) { CCState += dblyc.ToString() + ";"; } }
                            CCState.Remove(CCState.Length - 1);
                        }
                    }
                }
            }
            return CCState;
        }
        public virtual string ToHeadlineString()
        {
            string CCHLine;
            CCHLine = "";
            if ((TC.Count != 0) && (XC.Count != 0) && (YC.Count != 0))
            {
                if (TC.Count != 0)
                {
                    int i = 0;
                    foreach (double t in TC) { CCHLine += "Cluster Time " + i.ToString() + ";"; i++; }
                    int ii = 0;

                    int iii = 0;

                    foreach (List<double> exc in XC)
                    {
                        int jj = 0;
                        foreach (double dblxc in exc)
                        {
                            CCHLine += "XC_" + ii.ToString() + "_" + jj.ToString() + ";";
                            jj++;
                        }
                        ii++;
                    }
                    foreach (List<double> eyc in YC)
                    {
                        int jjj = 0;
                        foreach (double dblyc in eyc)
                        {
                            CCHLine += "YC_" + iii.ToString() + "_" + jjj.ToString() + ";"; jjj++;
                        }
                        iii++;
                    }
                    CCHLine.Remove(CCHLine.Length - 1);
                }
            }
            return CCHLine;
        }
        public virtual ClusterCenter Clone(ClusterCenter in_)
        {
            if (in_.xC.Count == 0) return null;
            for (int ie = 0; ie < in_.xC.Count; ie++)
            {
                if (in_.xC[ie].Count == 0) return null;
                List<double> entr = new List<double>(in_.xC[0].Count);
                for (int ii = 0; ii < in_.xC[0].Count; ii++)
                {
                    entr.Add(in_.xC[ie][ii]);
                }
                xC.Add(entr);
            }
            if (in_.yC.Count == 0) return null;
            for (int ie = 0; ie < in_.yC.Count; ie++)
            {
                if (in_.yC[ie].Count == 0) return null;
                List<double> entr = new List<double>(in_.yC[0].Count);
                for (int ii = 0; ii < in_.yC[0].Count; ii++)
                {
                    entr.Add(in_.yC[ie][ii]);
                }
                yC.Add(entr);
            }
            if (in_.tC.Count == 0) return null;
            for (int ie = 0; ie < in_.tC.Count; ie++)
            {
                double entr;
                entr=in_.tC[ie];
                tC.Add(entr);
            }
            return this;
        }
    }
    [Serializable]
    public class CProcessorNodeCfg : CechaConfig
    {
        public CProcessorNodeCfg():base() { }
    }
    public class CClusterRT<TRT,T>
        where TRT : CRTInputNode, new()
        where T : CHistoryInputNode, new()
    {
        public CClusterRT() { }
        public CProcessorNodeCfg cfg;
        private ClusterCenter modelRT_ = new ClusterCenter();
        public ClusterCenter modelRT
        {
            get { return modelRT_; }
            //set { modelRT_ = value; }
            set
            {
                modelRT_.Clone( value);
            }
        }
        private ClusterizationLogging clogging = new ClusterizationLogging();
        #region rtdata
        private object baton_ = new object();
        public object baton
        {
            get { return baton_; }
            set { baton_ = value; }
        }
        Thread th;
        public Semaphore sem = new Semaphore(0, 10000);
        bool deinit = false;
        private bool _initialclusterization = true;
        public bool initialclusterization
        {
            get { return _initialclusterization; }
            set { _initialclusterization = value; }
        }
        private bool _clusterizationDone = false;
        public bool clusterizationDone
        {
            get { return _clusterizationDone; }
            set { _clusterizationDone = value; }
        }
        private Queue<bool> qeueu;
        public Queue<bool> QeUeU
        {
            get { return qeueu; }
            set { qeueu = value; }
        }
        private Clusterization<TRT, T> _clusterization = new Clusterization<TRT,T>();
        public Clusterization<TRT, T> clusterizationobj
        {
            get { return _clusterization; }
            set { _clusterization = value; }
        }
        private CHistoryProcessor<T, TRT> _hp = new CHistoryProcessor<T,TRT>();
        public CHistoryProcessor<T, TRT> hp
        {
            get { return _hp; }
            set { _hp = value; }
        }
        private TRT real_time_data = new TRT();
        public TRT realTimeData
        {
            get { return real_time_data; }
            set { real_time_data = value; }
        }
        private T history_data = new T();
        public T hD
        {
            get { return history_data; }
            set { history_data = value; }
        }
        #endregion rtdata
        #region interface
        public virtual TRT GetRTData()
        {
            return this.realTimeData;
        }
        public virtual bool Config(CProcessorNodeCfg cfg, Clusterization<TRT, T> clusterizationobj, CHistoryProcessor<T, TRT> hp)
        {
            bool result = true;
            this.cfg = cfg;
            real_time_data = new TRT();
            this.hp = hp;
            this.clusterizationobj = clusterizationobj;
            QeUeU = new Queue<bool>();
            QeUeU.Enqueue(clusterizationDone);
            modelRT = cfg.СlusterСenters;
            clogging.Config(@"..\Data\STO\res\cluster_center\", cfg);//, new ClusterCenter().ToHeadlineString());
            //Init has not been used in methodic yet. add it here
            this.Init();
            clogging.Init();
            return result;
        }
        public virtual void Init()
        {
            this.th = new Thread(new ThreadStart(ClusterizationTh));
            this.th.Start();
        }
        public virtual void Activate()
        {
            clogging.Activate();
        }
        public virtual bool Calculate(TRT realTimeData, ref COutput target)
        {
            bool result = false;
            this.realTimeData = realTimeData;
            int xclustercount = hp.historyData.CC.xC.Count;
            int x0clustercount = (hp.historyData.CC.xC.Count != 0) ? hp.historyData.CC.xC[0].Count : 0;
            bool allzeros = true;
            for (int ntc = 0; ntc < hp.historyData.CC.tC.Count; ntc++)
            {
                allzeros = allzeros && (Math.Abs(hp.historyData.CC.tC[ntc]) <= 0.0001) ? true : false;
                if (allzeros)
                {
                    hp.historyData.CC.tC.Clear();
                    hp.historyData.CC.xC.Clear();
                    hp.historyData.CC.yC.Clear();
                }
            }
            if ((xclustercount >= 1) && (x0clustercount >= 1) && (!allzeros))
            {
                lock (baton)
                {
                    result = CalcRT(realTimeData, ref target);
                }
            }
            return result;
        }
        public virtual bool CalcRT(TRT realTimeDataF, ref COutput target)
        {
            bool result=true;
            List<List<double>> x = new List<List<double>>();
            List<List<double>> y = new List<List<double>>();
            List<double> t = new List<double>();
            x.Add(realTimeDataF.Input);
            y.Add(realTimeDataF.Output);
            t.Add(realTimeDataF.Tm);
            hD.CC = this.hp.historyData.CC;
            if (!(result = (hD.CC.tC.Count == cfg.NumberOfClusters)))
            {
                target.thetaT = t;
                return result;
            }
            List<List<double>> xC = new List<List<double>>(cfg.NumberOfClusters);
            List<List<double>> yC = new List<List<double>>(cfg.NumberOfClusters);
            List<double> tC = new List<double>(cfg.NumberOfClusters);
            xC = hD.CC.xC;
            yC = hD.CC.yC;
            tC = hD.CC.tC;
            List<List<double>> output = new List<List<double>>();
            List<double> thetaT = new List<double>();
            output = target.output;
            thetaT = target.thetaT;
            lock (baton)
            {
                result = this.clusterizationobj.formingModel(x, y, t, xC, yC, tC,
                    cfg.NumberOfClusters, cfg.Alpha, cfg.Beta,
                    ref output, ref thetaT);
                target.output = output;
                target.thetaT = thetaT;
                if (!(result))
                {
                    target.thetaT = t;
                    return result;
                }
            }
            //if (result)
            //{
            //    hD.CC.xC.Clear();
            //    hD.CC.yC.Clear();
            //    hD.CC.tC.Clear();
            //    for (int i = 0; i < xC.Count; i++)
            //    {
            //        hD.CC.xC.Add(xC[i]);
            //        hD.CC.yC.Add(yC[i]);
            //        hD.CC.tC.Add(tC[i]);
            //    }
                //target.output = hD.Y;
                //target.thetaT = hD.T;
            //}
            return result;
        }
        public virtual void ClusterizationTh()
        {
            while (!this.deinit)
            {
                bool res = sem.WaitOne();
                lock (baton)
                {
                    if (!this.deinit)
                    {
                        hD = hp.GetHistoryData();
                        hD.CC.Clone(modelRT);
                        clusterizationDone = false;
                        //initialclusterization = false;
                        if ((clusterizationobj.Exe(hD)))
                        {
                            hp.historyData.CC = clusterizationobj.GetClusterCenters();
                            this.hD = hp.historyData;
                            modelRT.Clone(hD.CC);
                        }
                        clusterizationDone = true;
                    }
                }
            }
            return;
        }
        public virtual bool Exe(TRT realTimeData,ref COutput output)
        {
            bool result=false;
            this.realTimeData = realTimeData;
            if(!(result = (hp.historyData.CC.xC.Count == hp.historyData.CC.tC.Count) && (hp.historyData.CC.xC.Count == hp.historyData.CC.yC.Count) ? true : false))return result;
            int xclustercount = hp.historyData.CC.xC.Count;
            int x0clustercount = (hp.historyData.CC.xC.Count!=0)?hp.historyData.CC.xC[0].Count:0;
            bool allzeros = true;
            for (int ntc = 0; ntc < hp.historyData.CC.tC.Count; ntc++)
            {
                allzeros = allzeros&&(Math.Abs(hp.historyData.CC.tC[ntc]) <= 0.0001) ? true : false;
                if(allzeros)
                {
                    hp.historyData.CC.tC.Clear();
                    hp.historyData.CC.xC.Clear();
                    hp.historyData.CC.yC.Clear();
                }
            }
            if((xclustercount>=1)&&(x0clustercount>=1)&&(!allzeros))
            {
                lock (this.baton)
                {
                    result = CalcRT(realTimeData, ref output);
                    //ADD:QeUeU.Enqueue(clusterizationDone);
                    //ADD:if(QeUeU.Count>2)
                    //ADD:{
                    //ADD:bool cd1 = QeUeU.Dequeue();
                    //ADD:bool cd2 = QeUeU.Dequeue();
                    //ADD:if((cd1==false)&&(cd2==true))
                    //ADD:{
                            clogging.Exe(hp.GetHistoryData().CC.ToString(), hp.historyData.CC);
                    //ADD:}
                    //ADD:}
                }
            }
            //else
            //{
            //hp.historyData = hp.GetHistoryData();
            
            //hD = hp.GetHistoryData();
            if ((((hp.historyData.T.Count >= cfg.RequiredRecordsNumberForInitialClusterization) || (hp.historyData.CC.tC.Count >= 1)) && (initialclusterization))
                    || ((hp.historyData.T.Count >= cfg.RequiredRecordsNumberForClusterization) && (!initialclusterization)))
                {
                    lock (this.baton)
                    {
                        if (clusterizationDone || initialclusterization) sem.Release();
                    }
                }
            //}
            return result;
        }
        public virtual void DeActivate()
        {
            clogging.DeActivate();
            QeUeU.Clear();
            return;
        }
        public virtual void DeInit()
        {
            this.realTimeData = null;
            deinit = true;
            clogging.DeInit();
            return;
        }
        #endregion interface
    }
    public class CRTInputNode
    {
        public CRTInputNode() { }
        public virtual bool Config() { return true; }
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
        public bool Config() { return true; }
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

        private CProcessorNodeCfg cfg = new CProcessorNodeCfg();
        public CProcessorNodeCfg Cfg
        {
            get { return cfg; }
            set { cfg = value; }
        }
        //public CechaConfig cfg;
        #endregion cfg

        private Stream streamCC;
        public Stream StreamCC
        {
            get { return streamCC; }
            set { streamCC = value; }
        }
        private Stream streamH;
        public Stream StreamH
        {
            get { return streamH; }
            set { streamH = value; }
        }
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
            set { real_time_data = value; }
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

        private CClusterRT<TRT, T> _crt;
        public CClusterRT<TRT, T> crt
        {
            get { return _crt; }
            set { _crt = value; }
        }
        private Clusterization<TRT, T> _clusterizationobj;
        public Clusterization<TRT, T> clusterizationobj
        {
            get { return _clusterizationobj; }
            set { _clusterizationobj = value; }
        }
        public virtual bool Config(CProcessorNodeCfg cfg, Clusterization<TRT, T> clusterizationobj,
            CClusterRT<TRT, T> crt, Stream stream1, Stream stream2)
        //public virtual bool Config(CechaConfig cfg)
        {
            bool result = true;
            this.StreamCC = stream1;
            this.StreamH = stream2;
            this.Cfg = cfg;
            historyData = new T();
            historyData.CC = cfg.СlusterСenters;
            QeUeU = new Queue<TRT>();
            this.crt = crt;
            this.clusterizationobj = clusterizationobj;
            return result;
        }
        public virtual void Init()
        {
        }
        void SerializeData<TS>(StreamWriter stream, TS obj)
        {
            XmlSerializer xmlFormat = new XmlSerializer(obj.GetType());
            {
                xmlFormat.Serialize(stream, obj);
            }
        }
        void DeSerializeData<TS>(StreamReader stream, ref TS obj)
        {
            XmlSerializer xmlFormat = new XmlSerializer(obj.GetType());
            {
                obj = (TS)xmlFormat.Deserialize(stream);
            }
        }
        public virtual void Activate()
        {
            StreamReader sHr = new StreamReader(StreamH);
            T hd = new T();
            this.DeSerializeData<T>(sHr, ref hd);
            this.historyData = hd;
        }
        public virtual bool Exe(CProcessorNodeBase<T, TRT> context)
        {
            bool result = true;
            this.real_time_data = context.realTimeData;
            {
                if (QeUeU.Count >= Cfg.MaxRecordsNumberForClusterization)
                {
                    foreach (List<double> e in historyData.X) { e.Clear(); }
                    historyData.X.Clear();
                    foreach (List<double> e in historyData.Y) { e.Clear(); }
                    historyData.Y.Clear();
                    historyData.T.Clear();
                    foreach (TRT e in QeUeU)
                    {
                        historyData.X.Add(e.Input);
                        historyData.Y.Add(e.Output);
                        historyData.T.Add(e.Tm);
                    }
                    context.crt.hD = historyData;
                    context.hp.historyData = historyData;
                    context.historyData = historyData;
                    QeUeU.Clear();
                    context.crt.sem.Release();
                }
                QeUeU.Enqueue(realTimeData);
                //if ((((QeUeU.Count + historyData.T.Count) >= Cfg.RequiredRecordsNumberForClusterization) && (!this.crt.initialclusterization))
                //     || ((QeUeU.Count + historyData.T.Count) >= Cfg.RequiredRecordsNumberForInitialClusterization))
                if (((QeUeU.Count >= Cfg.RequiredRecordsNumberForClusterization) && (!this.crt.initialclusterization))
                     || (QeUeU.Count >= Cfg.RequiredRecordsNumberForInitialClusterization))
                {
                    foreach (TRT que in QeUeU)
                    {
                        historyData.X.Add(que.Input);
                        historyData.Y.Add(que.Output);
                        historyData.T.Add(que.Tm);
                    }
                    context.crt.hD = historyData;
                    context.hp.historyData = historyData;
                    context.historyData = historyData;
                    QeUeU.Clear();
                }
            }
            return result;
        }
        public virtual void DeActivate()
        {
            StreamWriter sHr = new StreamWriter(StreamH);
            T hd = new T();
            hd = historyData;
            SerializeData<T>(sHr, hd);
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
    public class COutput
    {
        public COutput() { }
        public COutput(int C)
        {
            List<double> l = new List<double>();
            l.Add(0);
            output.Add(l);
            for (int c = 1; c < C; c++)
            {
                output[0].Add(0);
            }
            thetaT.Add(0);
        }
        public COutput(int C, int R)
        {
            List<double> l = new List<double>();
            l.Add(0);
            thetaT.Add(0);
            for(int r=1;r<R;r++)
            {
                output.Add(l);
                thetaT.Add(0);
                for (int c = 0; c < C; c++)
                {
                    output[R].Add(0);
                }
            }
        }
        private List<List<double>> _output = new List<List<double>>();
        public List<List<double>> output
        {
            get { return _output; }
            set { _output = value; }
        }
        private List<double> _thetaT = new List<double>();
        public List<double> thetaT
        {
            get { return _thetaT; }
            set { _thetaT = value; }
        }
    }
    public class CProcessorNodeBase<T, TRT>
        where T : CHistoryInputNode, new()
        where TRT : CRTInputNode, new()
    {
        public CProcessorNodeBase() { }
        #region cfg
        public CProcessorNodeCfg cfg;
        //public CechaConfig cfg;
        #endregion cfg
        #region rtdata
        private COutput _target;
        public COutput Target
        {
            get { return _target; }
            set { _target = value; }
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
        COutput output = new COutput();
        #region interface
        public CHistoryProcessor<T, TRT> hp { get; set; }
        public CClusterRT<TRT, T> crt { get; set; }
        public Clusterization<TRT, T> clusterizationobj { get; set; }
        public virtual bool Config(CProcessorNodeCfg cfg, Stream stream1, Stream stream2)
        //public virtual bool Config(CechaConfig cfg, Stream stream1, Stream styream2)
        {
            bool result = false;
            this.cfg = cfg;
            crt = new CClusterRT<TRT,T>();
            hp = new CHistoryProcessor<T, TRT>();
            clusterizationobj = new Clusterization<TRT, T>();
            result = hp.Config(cfg, clusterizationobj, crt, stream1, stream2);
            result = clusterizationobj.Config(cfg, crt, hp);
            result = crt.Config(cfg, clusterizationobj,hp);
            return result;
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
        public virtual bool Exe(TRT realTimeData, ref ClusterCenter clustercenters, ref COutput Target)
        {
            bool result = true;
            
            this.realTimeData = realTimeData;

            historyData = hp.GetHistoryData();
            clustercenters = historyData.CC;
            //result = this.crt.Exe(this,ref Target);//ref realTimeData, ref clustercenters, ref clusterizationobj, ref history_data);
            result = this.crt.Exe(realTimeData, ref Target);//ref realTimeData, ref clustercenters, ref clusterizationobj, ref history_data);
            hp.Exe(this);

            clustercenters = crt.hD.CC;
            return result;
        }
        public virtual bool Calc(TRT realTimeData, ref ClusterCenter clustercenters, ref COutput Target)
        {
            bool result = false;
            this.realTimeData = realTimeData;
            historyData = hp.GetHistoryData();
            clustercenters = hp.historyData.CC;
            //result = this.crt.Exe(this, ref Target);
            //result = this.crt.Calculate(this, ref Target);
            result = this.crt.Calculate(realTimeData, ref Target);
            //TODO
            //clustercenters = crt.hD.CC;
            
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
        public virtual T GetHistoryData()
        {
            return this.historyData;
        }
        public virtual ClusterCenter GetClusterCenters()
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
    public class CechaConfig
    {
        public CechaConfig() {}
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
