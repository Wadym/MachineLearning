using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace ClusterProcessorClassLibrary
{
    class ClusterizationLogging
    {
        public ClusterizationLogging(){}
        string __full_path = "C:\\out.txt";
        //FileMode __mode = new FileMode();
        //FileAccess __accessibility = new FileAccess();
        CProcessorNodeCfg __cfg = new CProcessorNodeCfg();
        //Stream __strm;
        public StreamWriter __writer { get; set; }
        bool __headlineprinted = false;
        string __headline = "Cluster Time;XC;YC";

        public ClusterizationLogging(string path/*,FileMode mode,FileAccess accessibility*/,CProcessorNodeCfg cfg)
        {
            __full_path = path + @"\" + cfg.ClusterCentersStorageName;
            //__mode = mode;
            //__accessibility = accessibility;
            __cfg = cfg;
        }
        public bool Config(string path/*,FileMode mode,FileAccess accessibility*/,CProcessorNodeCfg cfg)//,string headline)
        {
            bool succeeded = true;
            string srnd = " ";
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            srnd = rnd.Next(rnd.Next()).ToString();
            __full_path = path + @"\" + cfg.ClusterCentersStorageName + srnd+".csv";
            //__mode = mode;
            //__accessibility = accessibility;
            __cfg = cfg;
            //if (headline != null) { __headline = headline; }
            __writer = new StreamWriter(__full_path, false, Encoding.ASCII, 12288);
            //__strm = new FileStream(__path, __mode, __accessibility);
            return succeeded;
        }
        public bool Init()
        {
            bool succeeded = true;
            return succeeded;
        }
        public bool Activate()
        {
            bool succeeded = true;
            return succeeded;
        }
        public bool Exe(string line, ClusterCenter cc)
        {
            bool succeeded = true;
            try
            {
                //if (headline != null) { __headline = headline; }
                __headline = cc.ToHeadlineString();
                if (!__headlineprinted) WriteHeadLine(__headline);
                WriteLine(line);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return succeeded;
        }
        public bool DeActivate()
        {
            bool succeeded = true;
            __writer.Close();
            return succeeded;
        }
        public bool DeInit()
        {
            bool succeeded = true;
            return succeeded;
        }
        [Conditional("DEBUG")]
        public void WriteHeadLine(string input)
        {
            __writer.WriteLine(input);
            __headlineprinted = true;
            return;
        }
        [Conditional("DEBUG")]
        public void WriteLine(string line)
        {
            __writer.WriteLine(line);
            return;
        }
    }
}
