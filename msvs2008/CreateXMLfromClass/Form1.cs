using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using UniversalClusterProcessorClassLibrary;
using ClusterProcessorClassLibrary;

namespace CreateXMLfromClass
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //void SerializeData(CPE_ClusterProcessorCfg cfg)
        void SerializeData(CProcessorCfg cfg)
        {
            XmlSerializer xmlFormat = new XmlSerializer(cfg.GetType());
            using (Stream fStream = new FileStream("sample.xml",
            FileMode.Create, FileAccess.Write, FileShare.None))
            {
                xmlFormat.Serialize(fStream, cfg);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //CPE_ClusterProcessorCfg cfg = new CPE_ClusterProcessorCfg();
            CProcessorCfg cfg = new CProcessorCfg();
            for (int i = 0; i < 10; i++)
            {
                //cfg.Nodes.Add(new CPE_ClusterProcessorNodeCfg());
                cfg.Nodes.Add(new CProcessorNodeCfg());
                for (int j = 0; j < 100; j++)
                {
                    cfg.Nodes[i].СlusterСenters[j].tC.Add(0);
                    cfg.Nodes[i].cluster_center.xC.Add(new List<double>(0){0,0,0});
                    cfg.Nodes[i].cluster_center.yC.Add(new List<double>(0){0});
                }
                cfg.Nodes[i].CPCfg.Add(new CProcessorNodeCfg());
            }
            this.SerializeData(cfg);
        }

        private void btnRunClassScheleton_Click(object sender, EventArgs e)
        {
            CProcessorNode<CHistoryInputNode, CRTInputNode> cp = new CProcessorNode<CHistoryInputNode, CRTInputNode>();
            CProcessorNodeCfg cfg = new CProcessorNodeCfg();
            cp.Config(cfg);
            cp.Init();
            cp.Activate();
            long count = 1;
            long c = 0;
            while (c < count) 
            {
                cp.Exe();
            }
            cp.DeActivate();
            cp.DeInit();
        }
    }
}
