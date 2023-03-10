using System;
using System.Collections.Generic;
using System.Text;

namespace Web_Api.OpenMeteo
{ 
    public class Daily
    {
        public string[]? Time { get; set; }

        public int[]? Weathercode { get; set; }

        public float[]? Temperature_2m_max { get; set; }
                
        public float[]? Temperature_2m_min { get; set; }

        public float[]? Apparent_temperature_max { get; set; }

        public float[]? Apparent_temperature_min { get; set; }

        public string[]? Sunrise { get; set; }

        public string[]? Sunset { get; set; }
               
        public float[]? Precipitation_sum { get; set; }

        public float[]? Rain_sum { get; set; }

        public float[]? Showers_sum { get; set; }

        public float[]? Snowfall_sum { get; set; }

        public float[]? Precipitation_hours { get; set; }

        public float[]? Windspeed_10m_max { get; set; }

        public float[]? Windgusts_10m_max { get; set; }

        public float[]? Winddirection_10m_dominant { get; set; }

        public float[]? Shortwave_radiation_sum { get; set; }

        public float[]? Et0_fao_evapotranspiration { get; set; }
        public string WeathercodeToString(int weathercode)
        {
            switch (weathercode)
            {
                case 0:
                    return "Ясно";
                case 1:
                    return "Ясно";
                case 2:
                    return "Неболшая облачность";
                case 3:
                    return "Пасмурно";
                case 45:
                    return "Туман";
                case 48:
                    return "Туман";
                case 51:
                    return "Возможны осадки";
                case 53:
                    return "Возможны осадки";
                case 55:
                    return "Возможны осадки";
                case 56:
                    return "Моросит";
                case 57:
                    return "Моросит";
                case 61:
                    return "Небольшой дождь";
                case 63:
                    return "Дождь";
                case 65:
                    return "Ливень";
                case 66:
                    return "Ледяной дождь";
                case 67:
                    return "Ледяной ливень";
                case 71:
                    return "Возможен снег";
                case 73:
                    return "Идет снег";
                case 75:
                    return "Сильный снегопад";
                case 77:
                    return "Снегопад";
                case 80:
                    return "Дождь";
                case 81:
                    return "Ливень";
                case 82:
                    return "Ливень. Опасная погода!";
                case 85:
                    return "Снегопад";
                case 86:
                    return "Сильный снегопад";
                case 95:
                    return "Гром";
                case 96:
                    return "Мелкий град";
                case 99:
                    return "Крупный град, опасно!";
                default:
                    return "Invalid weathercode";
            }
        }
    }
}
