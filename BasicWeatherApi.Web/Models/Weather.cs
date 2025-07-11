namespace BasicWeatherApi.Web.Models
{
    public class Weather
    {
        /*hourly
			double[] temp_2m
			int[] realtive_humidity
			double[] apparent_temp_2m
			
		daily
			double max_temp
			double min_temp
			string sunrise
			string sunset
			double uv

		current
			double[] current_temp
			// custom hour can be grabbed from ^
			vvvv change to dictionaries later?*/
        public double[]? temp_2m { get; set; }
        public int[]? relative_humidity  { get; set; }
        public double[]? apparent_temp_2m { get; set; }
        public double? max_temp { get; set; }
        public double? min_temp { get; set; }
        public string? sunrise { get; set; }
        public string? sunset { get; set; }
        public double? uv { get; set; }
        
        public double? current_temp { get; set; }
    }
}