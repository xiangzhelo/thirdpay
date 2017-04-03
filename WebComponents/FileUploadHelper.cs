using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace viviapi.WebComponents
{
    public class FileUploadHelper
    {
        #region IsAllowedExtension
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hifile"></param>
        /// <returns></returns>
        public static bool IsAllowedExtension(System.Web.UI.WebControls.FileUpload hifile)
        {
            if (hifile.HasFile == false)
                return false;

            System.IO.FileStream fs = new System.IO.FileStream(hifile.PostedFile.FileName
                , System.IO.FileMode.Open
                , System.IO.FileAccess.Read);

            System.IO.BinaryReader r = new System.IO.BinaryReader(fs);
            string fileclass = "";
           
            //这里的位长要具体判断.
            byte buffer;
            try
            {
                buffer = r.ReadByte();
                fileclass = buffer.ToString();
                
                buffer = r.ReadByte();
                fileclass += buffer.ToString();

            }
            catch
            {

            }
            r.Close();
            fs.Close();
            if (fileclass == "255216" || fileclass == "7173")//说明255216是jpg;7173是gif;6677是BMP,13780是PNG;7790是exe,8297是rar
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        #endregion

    }
}
