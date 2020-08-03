using System;
using System.Collections.Generic;

namespace CNN_demo
{

    class CNN
    {
        /// <summary>
        /// CNN结构类，代表CNN一个层
        /// </summary>
        public class Layer
        {
            private List<double[,]> chan;
            /// <summary>
            /// 表示通道数，通常指一个层有几个Matrix
            /// </summary>
            public uint Channel { get; set; }
            /// <summary>
            /// 矩阵的维度
            /// </summary>
            public  int Rank { get;internal set; }
            /// <summary>
            /// 通道数值的索引，即矩阵某一行的索引
            /// </summary>
            public int Index { get;internal set; }
            public  double[,] Elements { get; set; }

            public Layer(int ranks,int index)
            {
                Rank = ranks;
                Index = index;
                Elements = new double[Rank,index];
                for(int i=0;i<Rank;i++ )
                {
                    for(int j=0;j<Index;j++)
                    {
                        Elements[i,j] = 0;
                    }
                }
            }

            public Layer(int ranks, int index,uint channel)
            {
                Rank = ranks;
                Index = index;
                for(int num=0;num< channel;num++)
                Elements = new double[Rank,index];
                for (int i = 0; i < Rank; i++)
                {
                    for (int j = 0; j < Index; j++)
                    {
                        Elements[i,j] = 0;
                    }
                    chan.Add(Elements);
                }
            }
        }
        //步长
        const int Trade = 1;
        //学习率
        const double StudyRates = 0.6;
        //loss
        const double Loss = 0.001;
        //卷积核大小，数量表示维度
        public int Kernelsize = 3;

        public double[,] Kernel;

        /// <summary>
        /// 卷积方法
        /// </summary>
        /// <param name="ly">层</param>
        /// <param name="kernel">卷积核</param>
        /// <param name="trade">步长</param>
        /// <returns>卷积结果</returns>
        public double[,] Convolution(Layer ly,double[,] kernel,int trade)
        {
            if (ly.Elements == null) return null;
                       
            int len= kernel.GetLength(0);
            double[,] outresult = new double[(ly.Rank - len) / trade + 1 , (ly.Rank - len) / trade + 1 ];
            double[,] _convultion = new double[len,len];
            for(int startrow=0;startrow<=ly.Rank-len;startrow+=trade)
            {
                for(int startcol=0;startcol<=ly.Index-len;startcol+=trade)
                {
                    //对元素逐个进行卷积
                    for(int k_num=0;k_num<kernel.GetLength(0);k_num++)
                    {
                        for(int v_num = 0; v_num < kernel.GetLength(0); v_num++)
                        {
                            _convultion[k_num, v_num] = ly.Elements[startrow+k_num,startcol+v_num] * 
                                kernel[k_num,v_num];

                        }
                    }
                    double c = 0;
                    for(int row=0;row<_convultion.GetLength(0);row++)
                    {
                        for(int pitch=0;pitch<_convultion.GetLength(0);pitch++)
                        {
                            c += _convultion[row,pitch];
                        }
                    }
                    outresult[startrow, startcol] = c;
                    
                }
            }
            return outresult;
        }
        
        /// <summary>
        /// 池化运算
        /// </summary>
        /// <param name="param">超参数</param>
        /// <param name="input">输入matrix</param>
        /// <returns></returns>
        public double[,] Pooling(int param,double[,] input)
        {
            if (param <= 0) throw new Exception("unaccept parameter");
            double[,] result = new double[input.GetLength(0) -param+1, input.GetLength(0) - param+1];
            double max = 0;
            for(int num=0;num<= input.GetLength(0) - param; num++)
            {
                for(int j=0;j<= input.GetLength(0) - param; j++)
                {                   
                    max = input[num, j];
                        //对元素逐一池化
                     for (int ss=0;ss<param;ss++)
                    {
                        for(int p=0;p<param;p++)
                        {
                            if(max < input[num + ss, j + p])
                            {
                                max = input[num + ss, j + p];
                            }
                            
                        }
                    }
                    result[num, j] = max;
                }
            }
            return result;
        }
        public double Activation(double input,ActiveFunType type)
        {
            double output = 0xff;
            switch(type)
            {
                case ActiveFunType.ReLu:
                    output=ReLu(input);
                    break;
                case ActiveFunType.Sigmod:
                    output = SigMod(input);
                    break;
                case ActiveFunType.Binary:
                    output = Binary(input);
                    break;
            }
            return output;
        }
        #region ActiveFunction
        public double ReLu(double input)
        {
            if (input <= 0) return 0;
            return (Math.Abs(input)+input)/2;
        }

        public double SigMod(double input)
        {
            return 1 / (1 + Math.Exp(-input));
        }

        public int Binary(double input)
        {
            return input > 0 ? 0 : 1;
        }
        #endregion
    }
    /// <summary>
    /// 激活函数类型
    /// </summary>
    enum ActiveFunType
    {
        /// <summary>
        /// Relu
        /// </summary>
        ReLu=0,
        /// <summary>
        /// sigmod
        /// </summary>
        Sigmod=1,
        /// <summary>
        /// binary
        /// </summary>
        Binary=2
    }
}
