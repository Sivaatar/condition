using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using CurrentProject.model;

namespace CurrentProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class weatherController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public weatherController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                         select city,Temperature,Humidity,Possibility_of_rain,wind_level from
                          weather.climate
            ";

            DataTable table = new DataTable();
            string sqlDatasource=_configuration.GetConnectionString("weathercon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDatasource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult(table);
        }


        [HttpPost]
        public JsonResult Post(weather c)
        {
            string query = @"
                        insert into weather.climate (city,Temperature,Humidity,Possibility_of_rain,wind_level) values(@city,@Temperature,@Humidity,@Possibility_of_rain,@wind_level)
            ";

            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("weathercon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDatasource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@city",c.city);
                    myCommand.Parameters.AddWithValue("@Temperature", c.Temperature);
                    myCommand.Parameters.AddWithValue("@Humidity",c.Humidity);
                    myCommand.Parameters.AddWithValue("@Possibility_of_rain",c.Possibility_of_rain);
                    myCommand.Parameters.AddWithValue("@wind_level", c.wind_level);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult("added successfully");
        }


        [HttpPut]
        public JsonResult Put(weather c)
        {
            string query = @"
                        update weather.climate set Temperature=@Temperature,Humidity=@Humidity,Possibility_of_rain=@Possibility_of_rain,wind_level=@wind_level
                        where city=@city
            ";

            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("weathercon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDatasource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@city", c.city);
                    myCommand.Parameters.AddWithValue("@Temperature", c.Temperature);
                    myCommand.Parameters.AddWithValue("@Humidity", c.Humidity);
                    myCommand.Parameters.AddWithValue("@Possibility_of_rain", c.Possibility_of_rain);
                     myCommand.Parameters.AddWithValue("@wind_level", c.wind_level);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult("updated successfully");
        }
        [HttpDelete]
        public JsonResult Delete(weather c)
        {
            string query = @"
                            DELETE FROM weather.climate WHERE city=@city;

            ";

            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("weathercon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDatasource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@city", c.city);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult("updated successfully");
        }
    }
}
