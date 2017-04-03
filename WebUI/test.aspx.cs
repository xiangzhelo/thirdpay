using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Aspose.Cells;

namespace viviAPI.WebUI7uka
{
    public partial class test : viviapi.WebComponents.Web.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (files.PostedFile.ContentLength > 10 * 1024 * 1024)
            //{
            //    lblMessage.Visible = true;
            //    lblMessage.Text = "不能大于10M";
            //    return;
            //}

            

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string check1(FileUpload file)
        {
            string msg = "";
            bool fileOk = false;

            string fileExtension = System.IO.Path.GetExtension(file.FileName).ToLower();
            
            string[] allowExtension = { ".xls", ".xlsx" };
            
            int j = 0;
            for (int i = 0; i < allowExtension.Length; i++)
            {
                if (fileExtension == allowExtension[i])
                {
                    fileOk = true;
                }
                else
                {
                    j++;
                }
            }
            if (j > 0)
            {
                msg = "文件格式不正确";
            }

            return msg;
        }

        //.xls application/vnd.ms-excel 
        //.xlsx application/vnd.openxmlformats-officedocument.spreadsheetml.sheet 
        public string check2(FileUpload file)
        {
            string msg = "";
            string type = file.PostedFile.ContentType.ToLower();


            return msg;
        }

        public string check3(FileUpload file)
        {
            string msg = "";
            Stream fs = FileUpload1.FileContent;
            BinaryReader r = new System.IO.BinaryReader(fs);
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


            return msg;
        }

        //.xlsx 8075 
        //.xls 208207 
        //application/vnd.openxmlformats-officedocument.spreadsheetml.sheet
        protected void Button1_Click(object sender, EventArgs e)
        {

            try
            {
                //string fileclass = FileUpload1.PostedFile.ContentType.ToLower();
                //Response.Write(fileclass);

                LoadOptions _options = new LoadOptions(LoadFormat.Xlsx);
                Workbook workbook = new Workbook(FileUpload1.FileContent, _options);

                //            workbook.Open(FileUpload1.FileContent);
                //workbook.Open(Server.MapPath("~/merchant/download/templetfile.xlsx"), FileFormatType.Xlsx);

                Cells cells = workbook.Worksheets[0].Cells;
                System.Data.DataTable dataTable = cells.ExportDataTable(9, 0, cells.MaxDataRow, 7);

                GridView1.DataSource = dataTable;
                GridView1.DataBind();
            }
            catch(Exception ex)
            {
                AlertAndRedirect(ex.Message);
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string pattern = "^(80133|YA|YB|YC|YD)";    

            Regex regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);

            bool result = false;

            string _value = this.TextBox1.Text.Trim();
            if (_value == null || _value.Length == 0)
            {
                result = false;
            }
            else
            {
                result = regex.IsMatch(_value.ToUpper());
            }

            this.Label1.Text = result.ToString();
        }
    }
}
