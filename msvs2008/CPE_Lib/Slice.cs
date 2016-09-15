using System;
using System.Collections.Generic;
using System.Text;

namespace CPE_Lib
{
    public class Slice
    {
        public Slice()
        {
            this.Data = new List<List<double>>();
            this.DataAttr = new List<List<int>>();
            this.Links = new Dictionary<Col, List<double>>();
            this.LinksAttr = new Dictionary<Col, List<int>>(); 
            
        }            
        
        public Slice(int input_count, int output_count):this()
        {
            this.InputData = new double[input_count];
            this.InputStatus = new int[input_count];
            this.InputDataNames = new string[input_count];
            
            this.OutputData = new double[output_count];
            this.OutputStatus = new int[output_count];
            this.OutputDataNames = new string[output_count];
            for (int i = 0; i < output_count; i++)
            {
                this.OutputStatus[i] = -1;
                this.OutputData[i] = -999;
            }

        }

        public enum Col { ISP, ModeDetectMean,ModeDetectDelta,Cluster,PostProocessor};     //type for position in Slice


        public Dictionary<Col, List<double>> Links { get; set; }        //link to data
        public Dictionary<Col, List<int>> LinksAttr { get; set; }       //link to data Attr


        private DateTime timestamp = new DateTime();

        public DateTime Timestamp
        {
            get { return timestamp; }
            set { timestamp = value; }
        }

        
        public List<List<double>> Data {get;set;}
        public List<List<int>> DataAttr { get; set; }
 

        private List<List<string>> data_names = new List<List<string>>();
        public List<List<string>> DataNames
        {
            get { return data_names; }
            set { data_names = value; }
        }


        private List<int> attr = new List<int>();
        public List<int> Attr
        {
            get { return attr; }
            set { attr = value; }
        }
        private List<int> status = new List<int>();
        public List<int> Status
        {
            get { return status; }
            set { status = value; }
        }

        private double[] input_data = new double[0];
        /// <summary>
        /// Массив входных значений. 
        /// </summary>
        public double[] InputData
        {
            get { return input_data; }
            set { input_data = value; }
        }
        private string[] input_data_names = new string[0];
        /// <summary>
        /// Массив входных значений. 
        /// </summary>
        public string[] InputDataNames
        {
            get { return input_data_names; }
            set { input_data_names = value; }
        }

        private int[] input_status = new int[0];
        /// <summary>
        /// Массив статусов входных значений. Если данные корректны, статус 0
        /// </summary>
        public int[] InputStatus
        {
            get { return input_status; }
            set { input_status = value; }
        }
        private double[] output_data = new double[0];
        public double[] OutputData
        {
            get { return output_data; }
            set { output_data = value; }
        }

        private string[] output_data_names = new string[0];
        public string[] OutputDataNames
        {
            get { return output_data_names; }
            set { output_data_names = value; }
        }
        private int[] output_status;
        public int[] OutputStatus
        {
            get { return output_status; }
            set { output_status = value; }
        }


        private List<string> pos_name = new List<string>();
        public List<string> Pos_Name
        {
            get { return pos_name; }
            set { pos_name = value; }
        }

        

        //void Init(Slice in3)
        //{
        //    in3.Links[
        //}
        //Dictionary<string, List<double>> links = new Dictionary<string, List<double>>();
        //public Dictionary<string, List<double>> Links
        //{
        //    get { return this.links; }
        //    set { this.links = value; }
        //}
    }
}
