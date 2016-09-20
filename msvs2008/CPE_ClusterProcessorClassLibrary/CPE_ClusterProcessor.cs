using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClusterProcessorClassLibrary;

namespace CPE_ClusterProcessorClassLibrary
{
    [Serializable]
    public class CPE_ClusterProcessorCfg
    {
        private List<CPE_ClusterProcessorNodeCfg> nodes = new List<CPE_ClusterProcessorNodeCfg>();
        public List<CPE_ClusterProcessorNodeCfg> Nodes
        {
            get { return nodes; }
            set { nodes = value; }
        }
   }
    public class CPE_ClusterProcessor
    {
        CPE_ClusterProcessorCfg cfg = new CPE_ClusterProcessorCfg();
        public CPE_ClusterProcessor(CPE_ClusterProcessorCfg cfg)
        {
            this.cfg = cfg;
        }
        public CPE_ClusterProcessor()
        {
        }
        private List<CPE_ClusterProcessorNode> mdl = new List<CPE_ClusterProcessorNode>();
        public List<CPE_ClusterProcessorNode> ClusterProcessorNode
        {
            get { return mdl; }
            set { mdl = value; }
        }
    }
}
