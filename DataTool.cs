using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNN_demo
{
    public class DataTool
    {
        /// <summary>
        /// 归一化函数
        /// </summary>
        /// <param name="data">需要归一化的数据</param>
        /// <param name="max">极大值</param>
        /// <param name="min">极小值</param>
        /// <returns>归一化结果</returns>
        public static double Normalize(double data,double max,double min)
        {
            return (max - data) / (max-min);
        }
        /// <summary>
        /// 归一化函数
        /// </summary>
        /// <param name="data"></param>
        /// <param name="max"></param>
        /// <param name="min"></param>
        /// <returns>归一化结果</returns>
        public static double Normalize(int data, int max, int min)
        {
            return (max - data) / (max - min);
        }
        /// <summary>
        /// 归一化函数
        /// </summary>
        /// <param name="data">需要归一化数组</param>
        /// <returns>归一化结果</returns>
        public static double[] Normalize(double[] data)
        {
            var max = data.Max();
            var min = data.Min();
            List<double> datalist = new List<double>();
            for(int i=0;i<data.Length;i++)
            {
                double result= (max - data[i]) / (max - min);
                datalist.Add(result);
            }
            return datalist.ToArray();
        }
        /// <summary>
        /// 归一化函数
        /// </summary>
        /// <param name="data"></param>
        /// <returns>归一化结果</returns>
        public static double[] Normalize(int[] data)
        {
            var max = data.Max();
            var min = data.Min();
            List<double> datalist = new List<double>();
            for (int i = 0; i < data.Length; i++)
            {
                double result = (max - data[i]) / (max - min);
                datalist.Add(result);
            }
            return datalist.ToArray();
        }
        /// <summary>
        /// 反归一化
        /// </summary>
        /// <param name="max"></param>
        /// <param name="min"></param>
        /// <param name="input"></param>
        /// <returns>反归一化结果</returns>
        public static double DeNormalize(double max,double min,double input)
        {
            return max- input * (max - min);
        }

    }
}
