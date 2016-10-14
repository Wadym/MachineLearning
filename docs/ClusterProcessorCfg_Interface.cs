using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalClusterProcessorClassLibrary
{
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

	public class CProcessorNodeCfg : CConfig
	{
		public CProcessorNodeCfg():base() { }
	}

	public class CClusterRT<TRT> where TRT : CRTInputNode, new()
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
        public virtual void Config(CProcessorNodeCfg cfg)
		{
		}

		public virtual void Init()
		{
		}

		public virtual void Activate()
		{
		}

		public virtual void GetRTData(ref TRT procData)
		{
			return;
		}

		public virtual void Exe()
		{
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
		}

		public virtual void Init()
		{
		}

		public virtual void Activate()
		{
		}

		public virtual void Exe()
		{
		}

		#endregion interface
        public virtual void GetHistoryData(ref T histData)
		{
			return;
		}
	}

	public class CProcessorNodeBase<T> where T : CHistoryInputNode, new()
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
        public virtual void Config(CProcessorNodeCfg cfg) { this.cfg = cfg; return; }
		public virtual void Init() { return; }
		public virtual void Activate() { return; }
		public virtual void Exe() { return; }
		public virtual void DeInit() { return; }
		public virtual void DeActivate() { return; }
		#endregion interface
        public virtual void GetHistoryData(ref T histData) { return; }
		public virtual void SupplyData(ref List<double> Z) { return; }
	}

	public class CProcessorNode<T, TRT> : CProcessorNodeBase<T>
        where T : CHistoryInputNode, new()
	where TRT : CRTInputNode, new()
	{
		public CProcessorNode() { }
		#region cfg
        CProcessorNodeCfg cfg;
		#endregion cfg
        T histData;
		CHistoryProcessor<T, TRT> hp;
		CClusterRT<TRT> crt;
		#region interface
        public override void Config(CProcessorNodeCfg cfg)
		{
		}

		public override void Init()
		{
		}

		public override void Activate()
		{
		}

		public override void Exe()
		{
		}

		#endregion interface
        public override void GetHistoryData(ref T histData)
		{
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
			return result;
		}

		public virtual double Average(List<double> vec)
		{
			return result;
		}

		public virtual double StdDev(ref List<double> vec, double mean)
		{
		}
	}
}