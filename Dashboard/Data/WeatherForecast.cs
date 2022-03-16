namespace Dashboard.Data
{
    public class WeatherForecast
    {       
        public float Temperature { get; set; }

        public string? Conditions { get; set; }

        public float Cloudcover { get; set; }
        public string? Location { get; set; }
        public DateTime Day { get; set; }
    }
}