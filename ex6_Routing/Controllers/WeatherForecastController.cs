using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

namespace ex6_Routing.Controllers
{
    [ApiController]
    [Route("api")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("{action}/{customParam:int}")]
        /*
         * constraint: int
         * 
         * uri for test:
         *      /api/get/1
         *      /api/get/6
         *      /api/get/456
         *      /api/get/1000       
         */
        public IEnumerable<WeatherForecast> Get(int customParam)
        {
            var rng = new Random();
            return Enumerable.Range(1, customParam).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet]
        [Route("{action}/{param:alpha}")]
        /*
         * constraint: alpha
         * 
         * uri for test:
         *      /api/echo/wow
         *      /api/echo/you
         *      /api/echo/are
         *      /api/echo/cool
         *      /api/echo/dadasddfdfasdf
         * **/
        public IActionResult Echo(string param)
        {
            return Ok(param);
        }

        [HttpGet]
        [Route("users/{id:guid}/{name:required:alpha:minlength(3)}/{age:regex(\\d):range(18, 99):int}/{active:bool}")]
        /*
         * constraints:
         *      guid
         *      alpha
         *      required
         *      minlength()
         *      regex()
         *      range()
         *      int
         *      bool
         *      
         * 
         * uri for test:
         *      /api/users/fc9bdd5f-8768-48f3-a464-9446b768c323/sergiy/19/true
         *      /api/users/6ab6bab7-9784-4fbb-9f4b-9b07e6b6091b/vitaliy/39/false
         *      /api/users/1821a3fb-0b48-49dc-98e8-268703bd96f1/kristina/22/true
         *      /api/users/88e0752a-411e-4782-97ef-9184889d8999/julia/38/false
         *      
         * **/
        public string CheckUser(Guid id, string name, int age, bool active)
        {
            string message = $"User:\n" +
                $"id: {id}\n" +
                $"name: {name}\n" +
                $"age: {age}\n" +
                $"account is {(active ? "active" : "inactive")}";

            return message;
        }


        [HttpGet]
        [Route("{shopName:maxlength(20)}/{itemName:length(2, 100)}/{price:double}/{date:datetime}")]
        /*
         * constraints:
         *      maxlength()
         *      length()
         *      double
         *      datetime
         * 
         * 
         * uri for test:
         *      /api/someshop/joycar/75/2002-08-27T10:45:45
         *      /api/clevershop/beer/55.5/2002-08-27T10:45:45
         *      /api/techshop/laptop/15456.26/2020-04-13T09:15:44
         *      /api/service/somedetail/423.6/2021-02-10T06:27:14
         *      
         * **/
        public IActionResult SomeMethod(string shopName, string itemName, double price, DateTime date)
        {
            var json = JsonSerializer.Serialize(new
            {
                ShopName = shopName,
                ItemName = itemName,
                Price = price,
                Date = date
            });
            return Ok(json);
        }

        [HttpGet("numbers/{decimalNumber:decimal}/{longNumber:long:min(10)}/{floatNumber:float}")]
        /*
         * constraints:
         *      decimal
         *      long
         *      min()
         *      float
         *      
         * uri for test
         *     /api/numbers/12.4444/12/3.14 
         *     /api/numbers/144/14445666662/1.5556487914
         *     /api/numbers/88.65456/456/2.89
         *     /api/numbers/5.6/4/2.5
         * **/
        public IActionResult SomeAnotherMethod(decimal decimalNumber, long longNumber, float floatNumber)
        {
            var json = JsonSerializer.Serialize(new
            {
                DecimalNumber = decimalNumber,
                LongNumber = longNumber,
                FloatNumber = floatNumber
            });

            return Ok(json);
        }


        [HttpGet("job/{userP:alpha:position_cust}/{experience:int:max(40)}")]
        /*
         * constraints:
         *      custom constraint - position_cust
         *      max()
         *      
         * uri for test
         *      /api/job/admin/4
         *      /api/job/director/36
         *      /api/job/employee/2
         *      /api/job/employee/27
         * **/
        public IActionResult PositionMethod(string userP, int experience)
        {
            var json = JsonSerializer.Serialize(new
            {
                UserPosition = userP,
                Experience = experience
            });

            return Ok(json);
        }
    }
}
