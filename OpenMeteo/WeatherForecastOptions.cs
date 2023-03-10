using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Web_Api.OpenMeteo
{
    public class WeatherForecastOptions
    {        
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public TemperatureUnitType Temperature_Unit { get; set; }
        public WindspeedUnitType Windspeed_Unit { get; set; }
        public PrecipitationUnitType Precipitation_Unit { get; set; }
        public CellSelectionType Cell_Selection { get; set; }
        public string Timezone { get; set; }
        public DailyOptions Daily { get { return _daily; } set { if (value != null) _daily = value; } }
        public WeatherModelOptions Models { get { return _models; } set { if (value != null) _models = value; } }

        public bool Current_Weather { get; set; }
        public TimeformatType Timeformat { get; set; }
        public int Past_Days { get; set; }
        public string Start_date { get; set; }
        public string End_date { get; set; }

        private DailyOptions _daily = DailyOptions.All;
        private WeatherModelOptions _models = new WeatherModelOptions();
                
        public WeatherForecastOptions(float latitude, float longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
            Temperature_Unit = TemperatureUnitType.celsius;
            Windspeed_Unit = WindspeedUnitType.kmh;
            Precipitation_Unit = PrecipitationUnitType.mm;
            Timeformat = TimeformatType.iso8601;
            Cell_Selection = CellSelectionType.land;
            Timezone = "GMT";
            Current_Weather = true;
           
            Start_date = DateTime.UtcNow.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            End_date = DateTime.UtcNow.Add(new System.TimeSpan(7, 0, 0, 0)).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        }
       
    }

    public enum TemperatureUnitType
    {
        celsius,
        fahrenheit
    }

    public enum WindspeedUnitType
    {
        kmh,
        ms,
        mph,
        kn
    }

    public enum PrecipitationUnitType
    {
        mm,
        inch
    }

    public enum TimeformatType
    {
        iso8601,
        unixtime
    }

    public enum CellSelectionType
    {
        land,
        sea,
        nearest
    }
}
