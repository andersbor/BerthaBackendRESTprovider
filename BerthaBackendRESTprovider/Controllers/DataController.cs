using System.Collections.Generic;
using System.Data.SqlClient;
using BerthaBackendRESTprovider.model;
using Microsoft.AspNetCore.Mvc;

namespace BerthaBackendRESTprovider.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private static readonly string ConnectionString = Controllers.ConnectionString.GetConnectionString();

        // GET: api/Data
        [HttpGet]
        public IEnumerable<ExtendedMeasurement> Get()
        {
            return null;
        }

        // GET: api/Data/5
        [Route("{userid}")]
        public IEnumerable<ExtendedMeasurement> GetByUserId(string userid)
        {
            const string selectString = "select * from berthabackend where userid=@userid order by utc";
            using (SqlConnection databaseConnection = new SqlConnection(ConnectionString))
            {
                databaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(selectString, databaseConnection))
                {
                    selectCommand.Parameters.AddWithValue("@userid", userid);
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        List<ExtendedMeasurement> list = new List<ExtendedMeasurement>();
                        while (reader.Read())
                        {
                            ExtendedMeasurement measurement = ReadData(reader);
                            list.Add(measurement);
                        }
                        return list;
                    }
                }
            }
        }

        private ExtendedMeasurement ReadData(SqlDataReader reader)
        {
            int id = reader.GetInt32(0);
            int deviceId = reader.GetInt32(1);
            double pm25 = reader.GetDouble(2);
            double pm10 = reader.GetDouble(3);
            int co2 = reader.GetInt32(4);
            int o3 = reader.GetInt32(5);
            double pressure = reader.GetDouble(6);
            double temp = reader.GetDouble(7);
            double humidity = reader.GetDouble(8);
            long utc = reader.GetInt64(9);
            double latitude = reader.GetDouble(10);
            double longitude = reader.GetDouble(11);
            int noise = reader.IsDBNull(12) ? -1 : reader.GetInt32(12);
            string userId = reader.GetString(13);
            //string publisher = reader.IsDBNull(3) ? null : reader.GetString(3);
            //decimal price = reader.GetDecimal(4);
            ExtendedMeasurement measurement = new ExtendedMeasurement
            {
                DeviceId = deviceId,
                Pm25 = pm25,
                Pm10 = pm10,
                Co2 = co2,
                O3 = o3,
                Pressure = pressure,
                Humidity = humidity,
                Temperature = temp,
                Utc = utc,
                Latitude = latitude,
                Longitude = longitude,
                Noise = noise,
                UserId = userId
            };
            return measurement;
        }

        // GET: api/Data/5
        [Route("{userid}/{after}")]
        public IEnumerable<ExtendedMeasurement> GetByUserIdAfter(string userid, int after)
        {
            const string selectString = "select * from berthabackend where userid=@userid and utc > @after order by utc";
            using (SqlConnection databaseConnection = new SqlConnection(ConnectionString))
            {
                databaseConnection.Open();
                using (SqlCommand selectCommand = new SqlCommand(selectString, databaseConnection))
                {
                    selectCommand.Parameters.AddWithValue("@userid", userid);
                    selectCommand.Parameters.AddWithValue("@after", after);
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        List<ExtendedMeasurement> list = new List<ExtendedMeasurement>();
                        while (reader.Read())
                        {
                            ExtendedMeasurement measurement = ReadData(reader);
                            list.Add(measurement);
                        }
                        return list;
                    }
                }
            }
        }


        // POST: api/Data
        [HttpPost]
        public int Post([FromBody] ExtendedMeasurement value)
        {
            const string insertString = "insert into berthabackend (deviceId, pm25, pm10, co2, o3, pressure, temp, humidity, utc, latitude, longitude, noise, userid) values (@deviceId, @pm25, @pm10, @co2, @o3, @pressure, @temp, @humidity, @utc, @latitude, @longitude, @noise, @userid)";
            using (SqlConnection databaseConnection = new SqlConnection(ConnectionString))
            {
                databaseConnection.Open();
                using (SqlCommand insertCommand = new SqlCommand(insertString, databaseConnection))
                {
                    insertCommand.Parameters.AddWithValue("@deviceId", value.DeviceId);
                    insertCommand.Parameters.AddWithValue("@pm25", value.Pm25);
                    insertCommand.Parameters.AddWithValue("@pm10", value.Pm10);
                    insertCommand.Parameters.AddWithValue("@co2", value.Co2);
                    insertCommand.Parameters.AddWithValue("@o3", value.O3);
                    insertCommand.Parameters.AddWithValue("@pressure", value.Pressure);
                    insertCommand.Parameters.AddWithValue("@temp", value.Temperature);
                    insertCommand.Parameters.AddWithValue("@humidity", value.Humidity);
                    insertCommand.Parameters.AddWithValue("@utc", value.Utc);
                    insertCommand.Parameters.AddWithValue("@latitude", value.Latitude);
                    insertCommand.Parameters.AddWithValue("@longitude", value.Longitude);
                    insertCommand.Parameters.AddWithValue("@noise", value.Noise);
                    insertCommand.Parameters.AddWithValue("@userId", value.UserId);
                    int rowsAffected = insertCommand.ExecuteNonQuery();
                    return rowsAffected;
                }
            }
        }

        // PUT: api/Data/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
