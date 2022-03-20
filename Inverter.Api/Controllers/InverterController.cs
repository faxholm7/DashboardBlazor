using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace Inverter.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InverterController : ControllerBase
    {
        private static readonly string? file = "danfoss-21" + DateTime.Now.ToString("MMddHH*"); //"danfoss-210712120003";
        private static readonly string? folder = "ftp://inverter.westeurope.cloudapp.azure.com/";
        private static readonly string? username = "studerende";
        private readonly string? password = "kmdp4gslmg46jhs";

        [HttpGet]
        public async Task<InverterServiceModel> GetInverter()
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(folder + file);
            request.Method = WebRequestMethods.Ftp.ListDirectory;

            request.Credentials = new NetworkCredential(username, password);

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            var stream = response.GetResponseStream();
            var reader = new StreamReader(stream);
            string filename = reader.ReadToEnd();

            var model = new InverterServiceModel();

            if (filename != "")
            {
               
                request = (FtpWebRequest)WebRequest.Create(folder + filename);
                request.Method = WebRequestMethods.Ftp.DownloadFile;

                request.Credentials = new NetworkCredential(username, password);

                response = (FtpWebResponse)await request.GetResponseAsync();
                stream = response.GetResponseStream();

                var csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture)
                {
                    HasHeaderRecord = false,
                    Delimiter = ";",
                    MissingFieldFound = null,
                };
                reader = new StreamReader(stream);
                var csvReader = new CsvReader(reader, csvConfig);
                
                var i = 0;
                while (csvReader.Read())
                {
                    if (i == 6)
                    {
                        model.StartTime = csvReader.GetField(1);
                        model.EnergyStart = csvReader.GetField(37);
                    }
                    if (csvReader.GetField(1) != null)
                    {
                        model.EndTime = csvReader.GetField(1);
                        model.EnergyEnd = csvReader.GetField(37);
                    }
                    ++i;
                }
                
            }
            else
            {
                model.StartTime = null;
                model.EnergyStart = null;
                model.EndTime = null;
                model.EnergyEnd = null;
               
            }
            reader.Close();
            response.Close();
            return model;


        }
    }
}

