using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System;
using System.IO;
using System.Diagnostics;
using System.Configuration;


namespace SendMail.Util
{
    public static class TempHtmlReader
    {
        public static string tempEmail(string userName, string title, string message, string path)
        {
            string body = string.Empty;
            //using streamreader for reading my htmltemplate   

            using (StreamReader reader = new StreamReader(path))
            {

                body = reader.ReadToEnd();

            }

            body = body.Replace("{userName}", userName); //replacing the required things  

            body = body.Replace("{title}", title);

            body = body.Replace("{message}", message);

            return body;  
        }
    }
}