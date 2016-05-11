using System;
using System.IO;
using System.Diagnostics;
using System.Configuration;

using SendMail.Models;
using SendMail.ServiceMail;
using SendMail.Util;
using System.Data;
using DevExpress.Web;
using SendMail.Models;
using System.Collections.Generic;
using Quartz;
using Quartz.Impl;
using System.Web.UI;

namespace SendMail
{
    public partial class frmContact : Page
    {
        #region Data Member

        #endregion

        #region Data structure
   
        #endregion

        #region Private Method
        #endregion

        #region Public Method
        #endregion

        #region Event

        protected void Page_Load(object sender, EventArgs e)
        {
  
        }


        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (FileUpload1.HasFile)
                {
                    string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                    string Extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                    string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                    txtNameFileUpload.Text = FileName;
                    string FilePath = Server.MapPath(FolderPath + FileName);
                    FileUpload1.SaveAs(FilePath);

                }
                else
                {
                    String message = "Bạn chưa chọn file hoặc file này đang được mở!";
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
                }
            }
            catch (Exception v_e)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + v_e + "');", true);
                Debugger.Log(1, "Send Mail", "Failed: " + v_e);
            }
        }

        protected void btnImportList_Click(object sender, EventArgs e)
        {
            try
            {

                SendMailEntities db = new SendMailEntities();

                List<Contact> listContact = new List<Contact>();

                string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                string FileName = txtNameFileUpload.Text;
                string Extension = Path.GetExtension(FileName);
                string FilePath = Server.MapPath(FolderPath + FileName);
                if (FileName != "")
                {
                    DataTable dt = ImportExcel.ImportExcel2DataTable(FilePath, Extension);
                    foreach (DataRow dr in dt.Rows)
                    {
                        Contact contact = new Contact();
                        contact.Email = dr["Email"].ToString();
                        contact.FirstName = dr["FirstName"].ToString();
                        contact.LastName = dr["LastName"].ToString();
                        contact.FullName = dr["FullName"].ToString();
                        contact.Phone = dr["Phone"].ToString();
                        contact.Adress = dr["Address"].ToString();
                        if (dr["Gender"].ToString().Trim().ToUpper().Equals("NỮ"))
                        {
                            contact.Gender = 0;
                        }
                        else
                        {
                            contact.Gender = 1;
                        }

                        contact.Birthday = DateTime.Parse(dr["Birthday"].ToString());
                        listContact.Add(contact);
                    }

                    db.Contacts.AddRange(listContact);
                    db.SaveChanges();

                    gridView.DataBind();
                }
                else
                {
                    String message = "Bạn chưa chọn file hoặc file này đang được mở!";
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
                }
            }
            catch (Exception v_e)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + HandleException.SqlExcforContact(v_e) + "');", true);
                Debugger.Log(1, "Send Mail", "Failed: " + v_e);
            }
        }

        #endregion
    }
}