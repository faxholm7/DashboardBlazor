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
        private string? file = "danfoss-21" + DateTime.Now.ToString("MMddHH*"); //"danfoss-210712120003";
        private string? folder = "ftp://inverter.westeurope.cloudapp.azure.com/"; 
        

        [HttpGet]
        public async Task<InverterServiceModel> GetInverter()
        {
            FtpClientController ftpClientController = new FtpClientController();
            string filename = await ftpClientController.FindFilename(folder + file);

            var model = new InverterServiceModel();

            if (filename != "")
            {
                Console.Write(filename);
                var reader = await ftpClientController.DownloadFile(folder + filename);   
                var csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture)
                {
                    HasHeaderRecord = false,
                    Delimiter = ";",
                    MissingFieldFound = null,
                };                
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
                reader.Close();
            }
            else
            {
                model.StartTime = DateTime.Now.AddHours(-1).ToString("HH") + ":00";
                model.EnergyStart = "0";
                model.EndTime = DateTime.Now.ToString("HH") + ":00";
                model.EnergyEnd = "0";
            }         
            return model;
        }

        [HttpGet]
        [Route("60min")]
        public async Task<InverterServiceModel[]> GetFullProduction()
        {
            FtpClientController ftpClientController = new FtpClientController();
            string filename = await ftpClientController.FindFilename(folder + file);

            if (filename != "")
            {
                var reader = await ftpClientController.DownloadFile(folder + filename);
                var csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture)
                {
                    HasHeaderRecord = false,
                    Delimiter = ";",
                    MissingFieldFound = null,
                };
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
                reader.Close();
              //  response.Close();
                return result.ToArray();

            }
            int hour = DateTime.Now.Hour;
            return await Task.FromResult(Enumerable.Range(0, 60).Select(index => new InverterServiceModel
            {
                StartTime = (hour-1 + ":" + index).ToString(),
                EnergyStart = "0",
                EndTime = DateTime.Now.ToString(),
                EnergyEnd = "0"
            }).ToArray());
        }
    }
}

