using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace Inverter.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
                        model.EnergyStart = csvReader.GetField(37);// int.Parse(csvReader.GetField(37));
                    }
                    if (csvReader.GetField(1) != null)
                    {
                        model.EndTime = csvReader.GetField(1);
                        model.EnergyEnd = csvReader.GetField(37);// int.Parse(csvReader.GetField(37));
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
        [HttpGet]
        [Route("60min")]
        public async Task<InverterServiceModel[]> GetFullProduction()
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(folder + file);
            request.Method = WebRequestMethods.Ftp.ListDirectory;

            request.Credentials = new NetworkCredential(username, password);

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            var stream = response.GetResponseStream();
            var reader = new StreamReader(stream);
            string filename = reader.ReadToEnd();
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
                List<InverterServiceModel> result = new List<InverterServiceModel>();
                var i = 0;
                while (csvReader.Read())
                {
                    if (i > 5 && csvReader.GetField(1) != null)
                    {
                        result.Add(new InverterServiceModel
                        {
                            StartTime = csvReader.GetField(1),
                            EndTime = csvReader.GetField(1),
                            EnergyStart = csvReader.GetField(37),
                            EnergyEnd = csvReader.GetField(37)
                        });
                    }
                    ++i;
                }
                 return result.ToArray();

            }
            //else
            //{
            //    return await Task.FromResult(Enumerable.Range(0, 1).Select(index => new InverterServiceModel
            //    {
            //        StartTime = null,
            //        EnergyStart = null,
            //        EndTime = null,
            //        EnergyEnd = null
            //    }).ToArray());


            //}

            return await Task.FromResult(Enumerable.Range(0, 1).Select(index => new InverterServiceModel
            {
                StartTime = null,
                EnergyStart = null,
                EndTime = null,
                EnergyEnd = null
            }).ToArray());



        }
    }
}

