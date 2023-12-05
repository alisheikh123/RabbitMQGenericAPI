using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Producor_Web_API.Interface;
using Producor_Web_API.Model;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Producor_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducerController : ControllerBase
    {
        private readonly IRabitMQProducer _rabitMQProducer;
        public ProducerController(IRabitMQProducer rabitMQProducer)
        {
            _rabitMQProducer = rabitMQProducer;
        }
        [HttpPost]
        public IActionResult GetApplicationsData(string table, string action, string model)
        {
            if (Enum.IsDefined(typeof(TableEnum), table) && Enum.IsDefined(typeof(ActionEnum), action))
            {
                var dataModel = new RequestParameter
                {
                    Table = table,
                    Action = action,
                    Model = model

                };
                _rabitMQProducer.SendProductMessage(dataModel);

            }
            else
            {
                return NotFound("Invalid Table or Action Name");
            }

            return Ok();
        }
        static byte[] SerializeObjects<T>(T obj)
        {
            string jsonString = JsonConvert.SerializeObject(obj);
            return Encoding.UTF8.GetBytes(jsonString);
        }

        // Deserialize a byte array to an object of type T using JSON

    }
}
