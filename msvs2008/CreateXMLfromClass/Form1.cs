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
using ClusterProcessorClassLibrary;
using CPE_ClusterProcessorClassLibrary;

namespace CreateXMLfromClass
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        void SerializeData(CPE_ClusterProcessorCfg cfg)
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
            CPE_ClusterProcessorCfg cfg = new CPE_ClusterProcessorCfg();
            for (int i = 0; i < 10; i++)
            {
                cfg.Nodes.Add(new CPE_ClusterProcessorNodeCfg());
                for (int j = 0; j < 100; j++)
                {
                    cfg.Nodes[i].cluster_center.tC.Add(0);
                    cfg.Nodes[i].cluster_center.xC.Add(new List<double>(0){0,0,0});
                    cfg.Nodes[i].cluster_center.yC.Add(new List<double>(0){0});
                }
                cfg.Nodes[i].CPCfg.Add(new ClusterProcessorCfg());
            }
            this.SerializeData(cfg);
        }
    }
}
