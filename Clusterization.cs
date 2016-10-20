using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace UniversalClusterProcessorClassLibrary
{
    [Serializable]
    public class Clusterization<TRT, T> : Cluster<TRT, T>
        where TRT : CRTInputNode, new()
        where T : CHistoryInputNode, new()
    {
        public Clusterization():base() { }
        CProcessorNodeCfg cfg;
        private List<List<double>> nXc = new List<List<double>>();
        private List<List<double>> nYc = new List<List<double>>();
        ClusterCenter cc = new ClusterCenter();
        public virtual void Config(CProcessorNodeCfg cfg)
        {
            this.cfg = cfg;
        }
        public override void Init() { }
        public override void Activate() { }
        public override bool Exe(TRT inRTData,T inData)
        {
            bool result = true;
            int iModel=2;
            List<List<double>> x = new List<List<double>>();
            List<List<double>> y = new List<List<double>>();
            List<double> t = new List<double>();
            x.Add(inRTData.Input);
            y.Add(inRTData.Output);
            t.Add(inRTData.Tm);
            cc = inData.CC;
            if (!(result =(cc.tC.Count == cfg.NumberOfClusters))) return result;
            List<List<double>> xC = new List<List<double>>(cfg.NumberOfClusters);
            List<List<double>> yC = new List<List<double>>(cfg.NumberOfClusters);
            List<double> tC = new List<double>(cfg.NumberOfClusters);
            xC = cc.xC;
            yC = cc.yC;
            tC = cc.tC;
//TODO
            formingModel(iModel, x, y, t, xC, yC, tC, cfg.NumberOfClusters, cfg.Alpha, cfg.Beta);
            cc.xC.Clear();
            cc.yC.Clear();
            cc.tC.Clear();
            for (int i = 0; i < xC.Count; i++)
            {
                cc.xC.Add(xC[i]);
                cc.yC.Add(yC[i]);
                cc.tC.Add(tC[i]);
            }
            return result;
        }
        public override bool Exe(T inData)
        {
            bool result = true;
            int iModel;

            //T inData = new T();
            //inData.Exe();
            List<List<double>> x = new List<List<double>>();
            List<List<double>> y = new List<List<double>>();
            List<double> t = new List<double>();
            x = inData.X;
            y = inData.Y;
            t = inData.T;
            cc = inData.CC;
            
            List<List<double>> xC = new List<List<double>>(cfg.NumberOfClusters);
            List<List<double>> yC = new List<List<double>>(cfg.NumberOfClusters);
            List<double> tC = new List<double>(cfg.NumberOfClusters);
            
            if ((cc.tC.Count == 0) || (cc.xC.Count == 0) || (cc.yC.Count == 0))
            {
                iModel = 1;
                for (int ie = 0; ie < cfg.NumberOfClusters; ie++)
                {
                    List<double> entr = new List<double>(x[0].Count);
                    for (int ii = 0; ii < x[0].Count; ii++)
                    {
                        entr.Add((double)0.0);
                    }
                    xC.Add(entr);
                }
                for (int ie = 0; ie < cfg.NumberOfClusters; ie++)
                {
                    //List<double> entr = new List<double>(y[0].Count);
                    //for (int ii = 0; ii < y[0].Count; ii++)
                    List<double> entr = new List<double>(y.Count);
                    for (int ii = 0; ii < y.Count; ii++)
                    {
                        entr.Add((double)0.0);
                    }
                    yC.Add(entr);
                }
                for (int ie = 0; ie < cfg.NumberOfClusters; ie++) { tC.Add((double)0); }
            }
            else
            {
                iModel = 2;
                xC = cc.xC;
                yC = cc.yC;
                tC = cc.tC;
            }
            this.formingModel(iModel, x, y, t, xC, yC, tC, cfg.NumberOfClusters, cfg.Alpha, cfg.Beta);
            cc.xC.Clear();
            cc.yC.Clear();
            cc.tC.Clear();
            for (int i = 0; i < xC.Count; i++)
            {
                cc.xC.Add(xC[i]);
                cc.yC.Add(yC[i]);
                cc.tC.Add(tC[i]);
            }
            
            //Generalist<ClusterCenter>.SaveAsXmlFormatClusterCenter(cc, this.FilenameClusterCenters);

            return result;
        }
        public override void DeActivate() { }
        public override void DeInit() { }
        public virtual ClusterCenter GetClusterCenters()
        {
            return this.cc;
        }
        public virtual bool formingModel(List<List<double>> x, List<List<double>> y, List<double> t, List<List<double>> xC, List<List<double>> yC, List<double> tC, int nc, double alpha, double beta,
                                        ref List<List<double>> z, ref List<double> thetaT)
        {
            bool result = true;
            double t0 = t[t.Count - 1];
            int n = xC.Count;
            int m = xC[0].Count;
            List<List<double>> nXc = new List<List<double>>(n);
            for (int ie = 0; ie < n; ie++)
            {
                List<double> entr = new List<double>(m);
                for (int ii = 0; ii < m; ii++)
                {
                    entr.Add((double)0.0);
                }
                nXc.Add(entr);
            }
            List<double> mX = new List<double>(m); for (int ie = 0; ie < m; ie++) { mX.Add(0.0); }
            List<double> sX = new List<double>(m); for (int ie = 0; ie < m; ie++) { sX.Add(0.0); }
            norm(xC, ref nXc, ref mX, ref sX);
            List<List<double>> nX = new List<List<double>>(x.Count);
            for (int ie = 0; ie < x.Count; ie++)
            {
                List<double> entr = new List<double>(x[0].Count);
                for (int ii = 0; ii < x[0].Count; ii++)
                {
                    entr.Add((double)0.0);
                }
                nX.Add(entr);
            }
            normT(x, ref mX, ref sX, ref nX);
            List<List<double>> nYc = new List<List<double>>(yC.Count);
            for (int ie = 0; ie < yC.Count; ie++)
            {
                List<double> entr = new List<double>(yC[0].Count);
                for (int ii = 0; ii < yC[0].Count; ii++)
                {
                    entr.Add((double)0.0);
                }
                nYc.Add(entr);
            }
            List<double> mY = new List<double>(yC[0].Count); for (int ie = 0; ie < yC[0].Count; ie++) { mY.Add(0.0); }
            List<double> sY = new List<double>(yC[0].Count); for (int ie = 0; ie < yC[0].Count; ie++) { sY.Add(0.0); }
            norm(yC, ref nYc, ref mY, ref sY);
            //normT(xC, ref mX, ref sX, ref nXc);
            try
            {
                //prediction(nXc, t0, nX, nY, tConctd, alpha, beta, ref nYc, ref tC);
                List<List<double>> nZ = new List<List<double>>(z.Count);
                for (int ie = 0; ie < z.Count; ie++)
                {
                    List<double> entr = new List<double>(z[0].Count);
                    for (int ii = 0; ii < z[0].Count; ii++)
                    {
                        entr.Add((double)0.0);
                    }
                    nZ.Add(entr);
                }
                result = prediction(nX, t0, nXc,
                                    nYc, tC, alpha, beta,
                                    ref nZ, ref thetaT);
                //ref List<List<double>> z     List<double> thetaT
                
                //denorm(nXc, mX, sX, ref xC);
                //denorm(nYc, mY, sY, ref yC);
                denorm(nZ, mY, sY, ref z);
            }
            catch (Exception ex)
            {
                //TODO log it
                //System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            return result;
        }
        public virtual bool formingModel(int iModel, List<List<double>> x, List<List<double>> y, List<double> t, List<List<double>> xC, List<List<double>> yC, List<double> tC, int nc, double alpha, double beta)
        //public virtual bool formingModel(int iModel, List<List<double>> x, List<List<double>> y, List<double> t, List<List<double>> xC, List<double> yC, List<double> tC, int nc, double alpha, double beta)
        {
            double t0 = t[t.Count - 1];
            List<double[]> collection = new List<double[]>();
            List<double[]> clusters = new List<double[]>();
            if (iModel == 1)
            {
                List<List<double>> nX = new List<List<double>>(x.Count);
                for (int ie = 0; ie < x.Count; ie++)
                {
                    List<double> entr = new List<double>(x[0].Count);
                    for (int ii = 0; ii < x[0].Count; ii++)
                    {
                        entr.Add((double)0.0);
                    }
                    nX.Add(entr);
                }
                List<double> mX = new List<double>(x[0].Count); for (int ie = 0; ie < x[0].Count; ie++) { mX.Add(0.0); }
                List<double> sX = new List<double>(x[0].Count); for (int ie = 0; ie < x[0].Count; ie++) { sX.Add(0.0); }
                norm(x, ref nX, ref mX, ref sX);
                List<List<double>> nY = new List<List<double>>(y.Count);
                for (int ie = 0; ie < y.Count; ie++)
                {
                    List<double> entr = new List<double>(y[0].Count);
                    for (int ii = 0; ii < y[0].Count; ii++)
                    {
                        entr.Add((double)0.0);
                    }
                    nY.Add(entr);
                }
                List<double> mY = new List<double>(y[0].Count); for (int ie = 0; ie < y[0].Count; ie++) { mY.Add(0.0); }
                List<double> sY = new List<double>(y[0].Count); for (int ie = 0; ie < y[0].Count; ie++) { sY.Add(0.0); }
                norm(y, ref nY, ref mY, ref sY);
                long kAtt = 1;
                while (kAtt < 100)
                {
                    try
                    {
                        collection.Clear();
                        for (int i = 0; i < x.Count; i++)
                        {
                            double[] x1 = new double[x[0].Count];
                            for (int j = 0; j < x[0].Count; j++)
                            {
                                x1[j] = nX[i][j];
                            }
                            collection.Add(x1);
                        };
                        double[] x1C = new double[xC[0].Count];
                        clusters.Clear();
                        clustering(nc
                       , collection
                       , ref clusters
                        , 1
                        , cfg.DeltaErrMax
                        , cfg.IterationMax);
                        for (int ie = 0; ie < clusters.Count; ie++)
                        {
                            List<double> entr = new List<double>(clusters[0].GetLength(0));
                            for (int ii = 0; ii < clusters[0].GetLength(0); ii++)
                            {
                                entr.Add(clusters[ie][ii]);
                            }
                            nXc.Add(entr);
                        }
                        int d0 = nXc.Count;
                        int d1 = nY[0].Count;
                        List<List<double>> nYc = new List<List<double>>(d0);//, d1);
                        for (int ie = 0; ie < d0; ie++)
                        {
                            List<double> entr = new List<double>(x[0].Count);
                            for (int ii = 0; ii < d1; ii++)
                            {
                                entr.Add((double)0.0);
                            }
                            nYc.Add(entr);
                        }
                        prediction(nXc, t0, nX, nY, t, alpha, beta, ref nYc, ref tC);
                        denorm(nXc, mX, sX, ref xC);
                        denorm(nYc, mY, sY, ref yC);
                        kAtt = 100;
                    }
                    catch (Exception ex)
                    {
                        kAtt += 1;
                        //System.Console.WriteLine("Hey you! qu-qu, iModel {0}; kAtt {1}", iModel, kAtt);
                        //System.Windows.Forms.MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                List<List<double>> xConctd = new List<List<double>>(xC.Count + x.Count);
                for (int ie = 0; ie < xC.Count; ie++)
                {
                    xConctd.Add(xC[ie]);
                }
                for (int ie = 0; ie < x.Count; ie++)
                {
                    xConctd.Add(x[ie]);
                }
                List<List<double>> yConctd = new List<List<double>>(yC.Count + y.Count);
                for (int ie = 0; ie < yC.Count; ie++)
                {
                    yConctd.Add(yC[ie]);
                }
                for (int ie = 0; ie < y.Count; ie++)
                {
                    yConctd.Add(y[ie]);
                }
                List<double> tConctd = new List<double>(tC.Count + t.Count);
                for (int ie = 0; ie < tC.Count; ie++)
                {
                    tConctd.Add(tC[ie]);
                }
                for (int ie = 0; ie < t.Count; ie++)
                {
                    tConctd.Add(t[ie]);
                }
                int n = xConctd.Count;
                int m = xConctd[0].Count;
                List<List<double>> nX = new List<List<double>>(n);
                for (int ie = 0; ie < n; ie++)
                {
                    List<double> entr = new List<double>(m);
                    for (int ii = 0; ii < m; ii++)
                    {
                        entr.Add((double)0.0);
                    }
                    nX.Add(entr);
                }
                List<double> mX = new List<double>(m); for (int ie = 0; ie < m; ie++) { mX.Add(0.0); }
                List<double> sX = new List<double>(m); for (int ie = 0; ie < m; ie++) { sX.Add(0.0); }
                norm(xConctd, ref nX, ref mX, ref sX);
                List<List<double>> nY = new List<List<double>>(yConctd.Count);
                for (int ie = 0; ie < yConctd.Count; ie++)
                {
                    List<double> entr = new List<double>(yConctd[0].Count);
                    for (int ii = 0; ii < yConctd[0].Count; ii++)
                    {
                        entr.Add((double)0.0);
                    }
                    nY.Add(entr);
                }
                List<double> mY = new List<double>(yConctd[0].Count); for (int ie = 0; ie < yConctd[0].Count; ie++) { mY.Add(0.0); }
                List<double> sY = new List<double>(yConctd[0].Count); for (int ie = 0; ie < yConctd[0].Count; ie++) { sY.Add(0.0); }
                norm(yConctd, ref nY, ref mY, ref sY);
                normT(xC, ref mX, ref sX, ref nXc);
                try
                {
                    collection.Clear();
                    for (int i = 0; i < nX.Count; i++)
                    {
                        double[] x1 = new double[nX[0].Count];
                        for (int j = 0; j < nX[0].Count; j++)
                        {
                            x1[j] = nX[i][j];
                        }
                        collection.Add(x1);
                    };
                    clusters.Clear();
                    for (int k = 0; k < nXc.Count; k++)
                    {
                        double[] cluster0 = new double[nXc[0].Count];
                        for (int i = 0; i < nXc[0].Count; i++)
                        {
                            cluster0[i] = nXc[k][i];
                        }
                        clusters.Add(cluster0);
                    }
                    clustering(nc
                   , collection
                   , ref clusters
                    , 2
                    , cfg.DeltaErrMax
                    , cfg.IterationMax);
                    int d0 = nXc.Count;
                    int d1 = nY[0].Count;
                    for (int ie = 0; ie < d0; ie++)
                    {
                        List<double> entr = new List<double>(x[0].Count);
                        for (int ii = 0; ii < d1; ii++)
                        {
                            entr.Add((double)0.0);
                        }
                        nYc.Add(entr);
                    }
                    prediction(nXc, t0, nX, nY, tConctd, alpha, beta, ref nYc, ref tC);
                    denorm(nXc, mX, sX, ref xC);
                    denorm(nYc, mY, sY, ref yC);
                }
                catch (Exception ex)
                {
                    //TODO log it
                    //System.Windows.Forms.MessageBox.Show(ex.Message);
                }
            }
            return true;
        }
    }

    public class Cluster<TRT, T> : BaseMath
        where TRT : CRTInputNode, new()
        where T : CHistoryInputNode, new()
    {
        CConfig cfg = new CConfig();
        public Cluster() { }
        private double[] x = null;
        public double[] X
        {
            get { return x; }
            set { x = value; }
        }
        private double[] y = null;
        public double[] Y
        {
            get { return y; }
            set { y = value; }
        }
        [DllImport("..\\..\\..\\..\\Debug\\win32prj.dll")]
        private extern unsafe static bool SolveMatrixEqDir(double[] a, double[] b, int m, double[] xdll);
        public virtual void Config() { }
        public virtual void Config(CConfig cfg) { this.cfg = cfg; }
        public virtual void Init() { }
        public virtual void Activate() { }
        public virtual bool Exe(T inData)
        {
            bool result = true;
            return result;
        }
        public virtual bool Exe(TRT inRTData, T inData)
        {
            bool result = true;
            return result;
        }
        public virtual void DeActivate() { }
        public virtual void DeInit() { }
        public virtual bool denorm(List<List<double>> nX, List<double> mX, List<double> sX, ref List<List<double>> x)
        {
            int[] d = new int[2];
            d[0] = nX.Count;
            d[1] = nX[0].Count;
            int n = d[0];
            int m = d[1];
            for (int ke = 0; ke < x.Count; ke++) { for (int ki = 0; ki < x[0].Count; ki++) { x[ke][ki] = 0; } }
            for (int i1 = 0; i1 < n; i1++)
            {
                for (int j1 = 0; j1 < m; j1++)
                {
                    if (sX[j1] > 0.0)
                    {
                        x[i1][j1] = nX[i1][j1] * sX[j1] + mX[j1];
                    }
                    else
                    {
                        x[i1][j1] = nX[i1][j1];
                    }
                }
            }
            return true;
        }
        public virtual bool normT(List<List<double>> x, ref List<double> mX, ref List<double> sX, ref List<List<double>> nX)
        {
            int n = x.Count;
            int m = x[0].Count;
            for (int ke = 0; ke < n; ke++) { for (int ki = 0; ki < m; ki++) { nX[ke][ki] = 0; } }
            for (int i1 = 0; i1 < n; i1++)
            {
                for (int j1 = 0; j1 < m; j1++)
                {
                    if (sX[0] > 0.0)
                    {
                        nX[i1][j1] = (x[i1][j1] - mX[j1]) / sX[j1];
                    }
                    else
                    {
                        nX[i1][j1] = x[i1][j1];
                    }
                }
            }
            return true;
        }
        public virtual bool norm(List<List<double>> x, ref List<List<double>> nX, ref List<double> mX, ref List<double> sX)
        {
            for (int i1 = 0; i1 < nX[0].Count; i1++)
            {
                List<double> clmn = new List<double>(x.Count);
                for (int i2 = 0; i2 < x.Count; i2++) { clmn.Add(x[i2][i1]); }
                double clmAvrg = Average(clmn);
                mX[i1] = clmAvrg;
                sX[i1] = StdDev(ref clmn, clmAvrg);
            }
            for (int i1 = 0; i1 < x.Count; i1++)
            {
                for (int j1 = 0; j1 < nX[0].Count; j1++)
                {
                    if (sX[j1] > 0.0)
                    {
                        nX[i1][j1] = (x[i1][j1] - mX[j1]) / sX[j1];
                    }
                    else
                    {
                        nX[i1][j1] = x[i1][j1];
                    }
                }
            }
            return true;
        }
        public virtual bool prediction(List<List<double>> xT, double theta0, List<List<double>> xL,
                                                  List<List<double>> yL, List<double> tL, double alpha, double beta,
                                              ref List<List<double>> z, ref List<double> thetaT)
        {
            for (int i = 0; i < xT.Count; i++)
            {
                int m = xT[0].Count;
                List<double> x0 = new List<double>(m);
                for (int i1 = 0; i1 < m; i1++)
                {
                    x0.Add(xT[i][i1]);
                }
                List<double> wDist = new List<double>(xL.Count);
                for (int ie = 0; ie < xL.Count; ie++) { wDist.Add((double)0.0); }
                weight(x0, xL, alpha, ref wDist);
                List<double> wAge = new List<double>(tL.Count);
                for (int ie = 0; ie < tL.Count; ie++) { wAge.Add((double)0.0); }
                age(theta0, tL, beta, ref wAge);
                List<double> w = new List<double>(wDist.Count);
                for (int ie = 0; ie < wDist.Count; ie++) { w.Add((double)0.0); }
                for (int i1 = 0; i1 < wDist.Count; i1++) { w[i1] = wDist[i1] * wAge[i1]; }
                for (int j = 0; j < yL[0].Count; j++)
                {
                    List<double> a = new List<double>(xL[0].Count);//[m+1,1];
                    for (int ie = 0; ie < (xL[0].Count + 1); ie++) { a.Add((double)0.0); }
                    List<double> yL1 = new List<double>(yL.Count);
                    for (int ie = 0; ie < yL.Count; ie++) { yL1.Add((double)0.0); }
                    for (int i1 = 0; i1 < yL.Count; i1++) { yL1[i1] = yL[i1][j]; }
                    weightOLS(xL, yL1, w, ref a);
                    double zz = 0;
                    result(x0, a, ref zz);
                    z[i][j] = zz;
                }
                double sum_w = 0; foreach (double item in w) { sum_w += item; }
                double sum_wTimestL = 0; for (int i2 = 0; i2 < w.Count; i2++) { sum_wTimestL += w[i2] * tL[i2]; }
                thetaT[i] = sum_wTimestL / sum_w;
            }
            return true;
        }
        public virtual bool prediction(List<List<double>> xT, double theta0, List<List<double>> xL,
                                       List<List<double>> yL, List<double> tL, double alpha, double beta,
                                       ref List<List<double>> z)
        {
            for (int i = 0; i < xT.Count; i++)
            {
                int m = xT[0].Count;
                List<double> x0 = new List<double>(m);
                for (int i1 = 0; i1 < m; i1++)
                {
                    x0.Add(xT[i][i1]);
                }
                List<double> wDist = new List<double>(xL.Count);
                for (int ie = 0; ie < xL.Count; ie++) { wDist.Add((double)0.0); }
                weight(x0, xL, alpha, ref wDist);
                List<double> wAge = new List<double>(tL.Count);
                for (int ie = 0; ie < tL.Count; ie++) { wAge.Add((double)0.0); }
                age(theta0, tL, beta, ref wAge);
                List<double> w = new List<double>(wDist.Count);
                for (int ie = 0; ie < wDist.Count; ie++) { w.Add((double)0.0); }
                for (int i1 = 0; i1 < wDist.Count; i1++) { w[i1] = wDist[i1] * wAge[i1]; }
                for (int j = 0; j < yL[0].Count; j++)
                {
                    List<double> a = new List<double>(xL[0].Count);//[m+1,1];
                    for (int ie = 0; ie < (xL[0].Count + 1); ie++) { a.Add((double)0.0); }
                    List<double> yL1 = new List<double>(yL.Count);
                    for (int ie = 0; ie < yL.Count; ie++) { yL1.Add((double)0.0); }
                    for (int i1 = 0; i1 < yL.Count; i1++) { yL1[i1] = yL[i1][j]; }
                    weightOLS(xL, yL1, w, ref a);
                    double zz = 0;
                    result(x0, a, ref zz);
                    z[i][j] = zz;
                }
            }
            return true;
        }
        public virtual bool weight(List<double> x0, List<List<double>> x, double alpha, ref List<double> w)
        {
            int[] d = new int[2];
            d[0] = x.Count;
            d[1] = x[0].Count;
            long n = d[0];
            long m = d[1];
            for (int i1 = 0; i1 < w.Count; i1++) { w[i1] = 0; }
            for (int i1 = 0; i1 < n; i1++)
            {
                double R2 = 0.0;
                for (int j1 = 0; j1 < m; j1++)
                {
                    R2 = R2 + Math.Pow(x0[j1] - x[i1][j1], 2);
                }
                double R = Math.Sqrt(R2);
                w[i1] = Math.Exp(-R * alpha);
            }
            List<double> www = new List<double>(new double[w.Count]);
            for (int i1 = 0; i1 < w.Count; i1++) { www[i1] = (w[i1]); }
            www.Sort();
            double wMax = www[www.Count - 1];
            for (int ind = 0; ind < www.Count; ind++)
            {
                w[ind] = w[ind] / wMax;
            }
            return true;
        }
        public virtual bool age(double theta0, List<double> tL, double beta, ref List<double> ww)
        {
            long n = tL.Count;
            for (int i1 = 0; i1 < n; i1++)
            {
                ww[i1] = Math.Exp(-(beta) * (theta0 - tL[i1]));
            }
            List<double> www = new List<double>(new double[ww.Count]);
            for (int i1 = 0; i1 < ww.Count; i1++) { www[i1] = ww[i1]; }
            www.Sort();
            double wMax = www[www.Count - 1];
            for (int ind = 0; ind < www.Count; ind++)
            {
                ww[ind] = ww[ind] / wMax;
            }
            return true;
        }
        public virtual bool weightOLS(List<List<double>> x, List<double> y, List<double> w, ref List<double> a)
        {
            int[] d = new int[2];
            d[0] = x.Count;
            d[1] = x[0].Count;
            int n = d[0];
            int m = d[1];
            double[,] A = new double[m + 1, m + 1];
            for (int i1 = 0; i1 < (m + 1); i1++)
            {
                for (int j1 = 0; j1 < (m + 1); j1++)
                {
                    for (int k1 = 0; k1 < n; k1++)
                    {
                        if ((i1 < (m + 1 - 1)) && (j1 < (m + 1 - 1)))
                        {
                            A[i1, j1] = A[i1, j1] + w[k1] * x[k1][i1] * x[k1][j1];
                        }
                        if ((i1 < (m + 1 - 1)) && (j1 == (m + 1 - 1)))
                        {
                            A[i1, j1] = A[i1, j1] + w[k1] * x[k1][i1];
                        }
                        if ((i1 == (m + 1 - 1)) && (j1 < (m + 1 - 1)))
                        {
                            A[i1, j1] = A[i1, j1] + w[k1] * x[k1][j1];
                        }
                        if ((i1 == (m + 1 - 1)) && (j1 == (m + 1 - 1)))
                        {
                            A[i1, j1] = A[i1, j1] + w[k1];
                        }
                    }
                }
            }
            double[,] B = new double[m + 1, m + 1];
            for (int i1 = 0; i1 < m + 1; i1++)
            {
                for (int k1 = 0; k1 < n; k1++)
                {
                    if (i1 < m + 1 - 1)
                    {
                        B[i1, 0] = B[i1, 0] + w[k1] * y[k1] * x[k1][i1];
                    }
                    if (i1 == m + 1 - 1)
                    {
                        B[i1, 0] = B[i1, 0] + w[k1] * y[k1];
                    }
                }
            }
            unsafe
            {
                double[] xa = new double[m + 1];
                double[] AA = new double[(m + 1) * (m + 1)];
                double[] BB = new double[(m + 1) * (m + 1)];
                for (int i = 0; i < xa.GetLength(0); i++) { xa[i] = (double)0.0; }
                for (int i = 0; i < (m + 1); i++)
                {
                    for (int j = 0; j < (m + 1); j++)
                    {
                        AA[i * (m + 1) + j] = A[i, j];
                    }
                }
                for (int i = 0; i < (m + 1); i++)
                {
                    for (int j = 0; j < (m + 1); j++)
                    {
                        BB[i * (m + 1) + j] = B[i, j];
                    }
                }
                int dims = m + 1;
                bool resultSolveMatrixEq = false;
                resultSolveMatrixEq = SolveMatrixEqDir(AA, BB, dims, xa);
                //Thread.Sleep(500);
                // back
                for (int i = 0; i < dims; i++) { a[i] = xa[i]; }
            }
            return true;
        }
        public virtual bool result(List<double> x, List<double> a, ref double y)
        {
            y = a[a.Count - 1];
            for (int i1 = 0; i1 < (a.Count - 1 - 1); i1++)
            {
                y = y + a[i1] * x[i1];
            }
            return true;
        }
        public virtual double distance(double[] x1, double[] x2, int type)
        {
            double dist = 0;
            if (type == 1)
            {
                double dist2 = 0.0;
                for (int i = 0; i < x1.Length; i++)
                {
                    dist2 = dist2 + Math.Pow(x1[i] - x2[i], 2);
                }
                dist = Math.Sqrt(dist2);
            }
            return dist;
        }
        public virtual bool clustering(int nC
            , List<double[]> collection
            , ref List<double[]> clusters
            , int initialClusters
            , double deltaErrMax
            , int iterationMax)
        {
            bool ok = true;
            // ******** initial position of the centers of the clusters
            if (initialClusters == 1)
            {
                // --------- Random chose points for the initial cluster centers
                Random rnd = new Random();
                int[] num = new int[nC];
                for (int k = 0; k < nC; k++)
                    //num[k] = 0;
                    num[k] = k;
                for (int k = 0; k < nC; k++)
                {
                    int a = 0;
                    while (a == 0)
                    {
                        num[k] = rnd.Next(0, collection.Count - 1);
                        a = 1;
                        for (int k1 = 0; k1 < nC; k1++)
                            if (k != k1 && num[k] == num[k1])
                                a = 0;
                    }
                }
                // --------- Initial position of the clusters centers
                int nX = collection[0].Length;
                for (int k = 0; k < nC; k++)
                {
                    double[] cluster0 = new double[nX];
                    for (int i = 0; i < nX; i++)
                    {
                        cluster0[i] = collection[num[k]][i];
                    }
                    clusters.Add(cluster0);
                }
            }
            // ********* Iterations
            int iteration = 1;
            double deltaErr = 0.0;
            double err;
            double errOld = 0.0;
            while (deltaErr > deltaErrMax || iteration < 2 && ok == true)
            {
                // --------- The distribution of points by clusters
                int[] clusterNum = new int[collection.Count];
                for (int j = 0; j < collection.Count; j++)
                {
                    clusterNum[j] = 0;
                }

                for (int j = 0; j < collection.Count; j++)
                {
                    double dist = distance(clusters[0], collection[j], 1);
                    double distMin = dist;
                    for (int k = 1; k < nC; k++)
                    {
                        dist = distance(clusters[k], collection[j], 1);
                        if (dist < distMin)
                        {
                            clusterNum[j] = k;
                            distMin = dist;
                        }
                    }
                }
                // --------Estimation of coordinates of the new clusters centers
                double[] capacity = new double[nC];
                for (int k = 0; k < nC; k++)
                {
                    capacity[k] = 0;
                    for (int i = 0; i < collection[0].Length; i++)
                    {
                        clusters[k][i] = 0.0;
                    }
                }
                for (int j = 0; j < collection.Count; j++)
                {
                    for (int k = 0; k < nC; k++)
                    {
                        if (clusterNum[j] == k)
                        {
                            for (int i = 0; i < collection[0].Length; i++)
                            {
                                clusters[k][i] = clusters[k][i] + collection[j][i];
                            }
                            capacity[k] = capacity[k] + 1;
                        }
                    }
                }
                for (int k = 0; k < nC; k++)
                    if (capacity[k] < 1)
                        ok = false;
                if (ok == true)
                {
                    for (int k = 0; k < nC; k++)
                    {
                        for (int i = 0; i < collection[0].Length; i++)
                        {
                            clusters[k][i] = clusters[k][i] / capacity[k];
                        }
                    }
                    // ********* Estimation of sum distanses from the cluster centers
                    err = 0.0;
                    for (int j = 0; j < collection.Count; j++)
                    {
                        for (int k = 0; k < nC; k++)
                        {
                            if (clusterNum[j] == k)
                            {
                                for (int i = 0; i < collection[0].Length; i++)
                                    err = err + Math.Pow(clusters[k][i] - collection[j][i], 2);
                            }
                        }
                    }
                    deltaErr = Math.Abs(errOld - err);
                    errOld = err;
                }
                iteration = iteration + 1;
                if (iteration > iterationMax)
                    ok = false;
            }
            return ok;
        }    
    }
}
