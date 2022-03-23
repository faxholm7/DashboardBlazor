using System.Net;

namespace Inverter.Api.Controllers
{
    public class FtpClientController
    {
        private readonly string? username = "studerende";
        private readonly string? password = "kmdp4gslmg46jhs";
        public async Task<string> FindFilename(string wildcard)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(wildcard);
            request.Method = WebRequestMethods.Ftp.ListDirectory;

            request.Credentials = new NetworkCredential(username, password);

            FtpWebResponse response = (FtpWebResponse)await request.GetResponseAsync();
            var stream = response.GetResponseStream();
            var reader = new StreamReader(stream);
            //response.Close();
            return reader.ReadToEnd();
        }
        public async Task<StreamReader> DownloadFile(string filename)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(filename);
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            request.Credentials = new NetworkCredential(username, password);

            FtpWebResponse response = (FtpWebResponse)await request.GetResponseAsync();
            var stream = response.GetResponseStream();
            //response.Close();
            return new StreamReader(stream);
        }
    }
}
