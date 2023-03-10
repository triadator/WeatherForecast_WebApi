using System;
using System.Collections;
using System.Collections.Generic;

namespace Web_Api.OpenMeteo
{
    public class DailyOptions : IEnumerable, ICollection<DailyOptionsParameter>
    {
        //По итогу проще добавить все сразу, чем настраивать в коде объем желаемых данных в дейлик
        public static DailyOptions All { get { return new DailyOptions((DailyOptionsParameter[])Enum.GetValues(typeof(DailyOptionsParameter))); } }
        
        public List<DailyOptionsParameter> Parameter { get { return new List<DailyOptionsParameter>(_parameter); } }

        public int Count => _parameter.Count;

        public bool IsReadOnly => false;

        private readonly List<DailyOptionsParameter> _parameter = new List<DailyOptionsParameter>();

        public DailyOptions()
        {

        }

        public DailyOptions(DailyOptionsParameter parameter)
        {
            Add(parameter);
        }

        public DailyOptions(DailyOptionsParameter[] parameter)
        {
            Add(parameter);
        }

        //индексатор дейлика
        public DailyOptionsParameter this[int index]
        {
            get { return _parameter[index]; }
            set
            {
                _parameter[index] = value;
            }
        }

        public void Add(DailyOptionsParameter param)
        {
            // Check that the parameter isn't already added
            if (_parameter.Contains(param)) return;

            _parameter.Add(param);
        }

        public void Add(DailyOptionsParameter[] param)
        {
            foreach (DailyOptionsParameter paramToAdd in param)
            {
                Add(paramToAdd);
            }
        }

        public void Clear()
        {
            _parameter.Clear();
        }

        public bool Contains(DailyOptionsParameter item)
        {
            return _parameter.Contains(item);
        }

        public bool Remove(DailyOptionsParameter item)
        {
            return _parameter.Remove(item);
        }

        public void CopyTo(DailyOptionsParameter[] array, int arrayIndex)
        {
            _parameter.CopyTo(array, arrayIndex);
        }

        public IEnumerator<DailyOptionsParameter> GetEnumerator()
        {
            return _parameter.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    public enum DailyOptionsParameter
    {
        weathercode,
        temperature_2m_max,
        temperature_2m_min,
        apparent_temperature_max,
        apparent_temperature_min,
        sunrise,
        sunset,
        precipitation_sum,
        rain_sum,
        showers_sum,
        snowfall_sum,
        precipitation_hours,
        windspeed_10m_max,
        windgusts_10m_max,
        winddirection_10m_dominant,
        shortwave_radiation_sum,
        et0_fao_evapotranspiration
    }
}
