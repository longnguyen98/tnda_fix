using ImageProcessor;
using System;
using System.IO;
using System.Web;

namespace tnda_fix.Models
{
    public class Tools
    {
        private static readonly string[] VietNamChar = new string[]
  {
        "aAeEoOuUiIdDyY",
        "áàạảãâấầậẩẫăắằặẳẵ",
        "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
        "éèẹẻẽêếềệểễ",
        "ÉÈẸẺẼÊẾỀỆỂỄ",
        "óòọỏõôốồộổỗơớờợởỡ",
        "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
        "úùụủũưứừựửữ",
        "ÚÙỤỦŨƯỨỪỰỬỮ",
        "íìịỉĩ",
        "ÍÌỊỈĨ",
        "đ",
        "Đ",
        "ýỳỵỷỹ",
        "ÝỲỴỶỸ"
  };
        public static string convert(string str)
        {
            //Thay thế và lọc dấu từng char      
            for (int i = 1; i < VietNamChar.Length; i++)
            {
                for (int j = 0; j < VietNamChar[i].Length; j++)
                {
                    str = str.Replace(VietNamChar[i][j], VietNamChar[0][i - 1]);
                }
            }
            return str;
        }
        public static string getUniqueNum()
        {
            string s = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
            return s;
        }
        public static string uploadAndResizeImg(HttpPostedFileBase file, string _path, string _filename)
        {
            Stream outStream = new FileStream(_path, FileMode.Create, FileAccess.ReadWrite);
            //
            ImageFactory imf = new ImageFactory
            {
                PreserveExifData = true
            };
            imf.Load(file.InputStream);
            int h, w;
            h = imf.Image.Height / 10;
            w = imf.Image.Width / 10;
            //
            imf.Resize(new System.Drawing.Size(w, h));
            imf.Save(outStream);
            //
            return "/img/upload/" + _filename;
        }
    }
}