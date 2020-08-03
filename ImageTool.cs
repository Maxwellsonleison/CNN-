using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNN_demo
{
    class ImageTool
    {
        private Image _image;
        public ImageTool(Image image)
        {
            image = _image;
        }
        /// <summary>
        /// 获取图像像素数据
        /// </summary>
        /// <returns>object或List<Color[]></returns>
        public List<Color[]> GetImageDate()
        {
            if (_image == null) throw new NullReferenceException("image is null or error rerference.");
            List<Color[]> imageres = new List<Color[]>();
            Bitmap bt = _image as Bitmap;
            for(int i=0;i<bt.Width;i++)
            {
                List<Color> iColor = new List<Color>();
                for(int j=0;j<bt.Height;j++)
                {     
                    Color pixel = bt.GetPixel(i, j);
                    iColor.Add(pixel);
                }
                imageres.Add(iColor.ToArray());
            }
            return imageres;
        }

        /// <summary>
        /// 返回图像
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Image BackForImage(object data)
        {
            if (data == null) return null;

            var imgres=data as List<Color[]>;
            try
            {
                Bitmap bt = new Bitmap(imgres[0].Length,imgres.Count);
                for(int line = 0; line < imgres.Count; line++)
                {
                    for(int i=0;i<imgres[line].Length;i++)
                    {
                        bt.SetPixel(i,line, imgres[line][i]);
                    }
                }
                return bt;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string ImageType()
        {
            return _image.RawFormat.ToString();
        }
    }
}
