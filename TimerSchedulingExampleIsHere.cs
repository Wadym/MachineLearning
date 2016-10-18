using System;
using System.Collections.Generic;
using System.Text;
using CPE_Lib;
using System.Threading;

namespace DPE_Sto_Cfg_Loaders
{
    public class CPE_Clasters_Module_Config
    {
        private double updaterate = 10000;
        public double UpdateRate
        {
            get { return updaterate; }
            set { updaterate = value; }
        }

        private bool calc_on_start = true;

        public bool CalcOnStart
        {
            get { return calc_on_start; }
            set { calc_on_start = value; }
        }

    }


    public class CPE_Clasters_Module: SliceProcessing
    {
        public override void Config(CPE_Clasters_Module_Config cfg)
        {
            this.cfg = cfg;
        }
        private CPE_Clasters_Module_Config cfg;

        double updaterate = 100000;

        public override void Init()
        {
            this.th = new Thread(new ThreadStart(this.ClasterizationThread));
            this.th.Start();
            this.timer = new System.Timers.Timer(this.updaterate);
            this.timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
            this.timer.AutoReset = true;
            
        }
        public void Activate()
        {
            this.timer.Start();
            if (this.cfg.CalcOnStart) this.sem.Release();
        }

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.sem.Release();
        }

        Thread th;
        Semaphore sem = new Semaphore(0, 10000);
        System.Timers.Timer timer;

        bool deinit = false;
        void ClasterizationThread()
        {
            while (!this.deinit)
            {
                bool res = sem.WaitOne();
                if (!this.deinit)
                {
                    this.CalcClusters();
                }
            }
        }

        void CalcClusters()
        {

        }

        public override void DeInit()
        {
            this.deinit = true;
        }
        public override bool Process(Slice input)
        {
            //this.class_rt.CalcRT(input);
            //this.class_hist.StoreHistory(input);
        }
    }

    //public class HistoryCollector
    //{
    //    public virtual Process(Slice input)
    //    {
    //    }
    //}
}

