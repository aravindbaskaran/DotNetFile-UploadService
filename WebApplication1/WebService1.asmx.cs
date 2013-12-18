using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;


namespace WebApplication1
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
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet=true)]
        public User getValue()
        {
            User response = new User();
            response.UserName = "Test";
            return response;
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

        // Works all around!
        public string TransferFile()
        {
            HttpContext postedContext = HttpContext.Current;
            String sOut = "";
            // Get other parameters in request using Params.Get
            String fileName = postedContext.Request.Params.Get("FileName");
            String fileIdentifier = postedContext.Request.Params.Get("FileIdentifier");

            String applnId = postedContext.Request.Params.Get("ApplnId");
            String user = postedContext.Request.Params.Get("UserId");
            String password = postedContext.Request.Params.Get("Password");
            String fileNo = postedContext.Request.Params.Get("FileNo");
            String fileRow = postedContext.Request.Params.Get("FileRowSrno");
            String remark = postedContext.Request.Params.Get("Remark");
            String image1 = "undefined", image2 = "undefined", image3 = "undefined", image4 = "undefined", image5 = "undefined";

            HttpFileCollection FilesInRequest = postedContext.Request.Files;

            for (int i = 0; i < FilesInRequest.AllKeys.Length; i++)
            {
                // Loop through to get all files in the request
                HttpPostedFile item = FilesInRequest.Get(i);
                string filename = item.FileName;
                byte[] fileBytes = new byte[item.ContentLength];
                item.InputStream.Read(fileBytes, 0, item.ContentLength);
                var appData = Server.MapPath("~/App_Data/");
                
                var file = Path.Combine(appData, Path.GetFileName(filename));
                File.WriteAllBytes(file, fileBytes);
                switch (i)
                {
                    case 0:
                        image1 = filename;
                        break;
                    case 1:
                        image2 = filename;
                        break;
                    case 2:
                        image3 = filename;
                        break;
                    case 3:
                        image4 = filename;
                        break;
                    case 4:
                        image5 = filename;
                        break;
                }
            }
            sOut = "Image1 - " + image1 +
                "Image2 - " + image2 +
                "Image3 - " + image3 +
                "Image4 - " + image4 +
                "Image5 - " + image5;
            return sOut;
        }
    }
    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
    }
}
