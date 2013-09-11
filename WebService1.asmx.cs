using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WebApplication2
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        // Not mobile friendly
        public void Upload(string filename, byte[] DocBuffer)
        {
            var appData = Server.MapPath("~/App_Data");
            var file = Path.Combine(appData, Path.GetFileName(filename));
            File.WriteAllBytes(file, DocBuffer);
        }

        [WebMethod]
        // Works all around!
        public string TransferFile()
        {
            HttpContext postedContext = HttpContext.Current;
            // Get other parameters in request using Params.Get
            String fileName = postedContext.Request.Params.Get("FileName");
            HttpFileCollection FilesInRequest = postedContext.Request.Files;
            
            for(int i = 0; i < FilesInRequest.AllKeys.Length; i++)
            {
                // Loop through to get all files in the request
                HttpPostedFile item = FilesInRequest.Get(i);
                string filename = item.FileName;
                byte[] fileBytes = new byte[item.ContentLength];
                item.InputStream.Read(fileBytes, 0, item.ContentLength);
                var appData = Server.MapPath("~/App_Data");
                var file = Path.Combine(appData, Path.GetFileName(filename));
                File.WriteAllBytes(file, fileBytes);
            }
            return "Superb, saved";
        }
    }
}
