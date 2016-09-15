using System;
using System.Collections.Generic;
using System.Text;
using log4net;

namespace CPE_Lib
{
    public class CPE_Engine
    {
        public CPE_Engine()
        {
            logger = log4net.LogManager.GetLogger(this.GetType());
        }


        private List<SliceProcessing> slice_process = new List<SliceProcessing>();
        public List<SliceProcessing> Slice_Process
        {
            get { return slice_process; }
            set { slice_process = value; }
        }

        public void Init()
        {

            for (int i = 0; i < this.Slice_Process.Count; i++)
            {
                this.Slice_Process[i].Init();
            }
        }

        public void Activate()
        {
            for (int i = 0; i < this.Slice_Process.Count; i++)
            {
                this.Slice_Process[i].Activate();
            }
        }
        public void DeActivate()
        {
            for (int i = 0; i < this.Slice_Process.Count; i++)
            {
                this.Slice_Process[i].DeActivate();
            }
        }

        public void DeInit()
        {
            for (int i = 0; i < this.Slice_Process.Count; i++)
            {
                this.Slice_Process[i].DeInit();
            }
        }
        ILog logger;
        public bool Process(Slice data)
        {
            bool result = false;
            bool process = true;
            logger.DebugFormat("Process");
            for (int i = 0; i < this.Slice_Process.Count; i++)
            {
                try
                {

                    bool done = this.Slice_Process[i].Process(data);
                    logger.DebugFormat("Process block[{0}] = {1}", this.Slice_Process[i].GetType(), done);
                    //              done = true;
                    if (false == done)
                    {
                        logger.InfoFormat("Process block[{0}] = break!", this.Slice_Process[i].GetType());
                        process = false;
                        break;
                    }
                }
                catch (Exception ex)
                {
                    this.logger.ErrorFormat("Process - block={0}, type={1}, message = {2}", i, this.Slice_Process[i].GetType(), ex.Message);
                }
            }
            return result;
        }
    }
}
