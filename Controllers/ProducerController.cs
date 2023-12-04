using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        public ProducerController()
        {

        }
        [HttpPost]
        public IActionResult GetApplicationsData(string table, string action, string model)
        {
            if (Enum.IsDefined(typeof(TableEnum), table) && Enum.IsDefined(typeof(ActionEnum), action))

            {  //Create Connnection of Rabbit MQ{
                var factory = new ConnectionFactory { HostName = "localhost" };
                var conn = factory.CreateConnection();

                // Create Channel and make queue
                var channel = conn.CreateModel();
                channel.QueueDeclare(queue: "letterbox", durable: false, exclusive: false, autoDelete: false, arguments: null);


                var dataModel = new RequestParameter
                {
                    Table = table,
                    Action = action,
                    Model = model

                };

                var message = JsonConvert.SerializeObject(dataModel);
                var encodeMessage = Encoding.Default.GetBytes(JsonConvert.SerializeObject(Regex.Replace(message, @"\s+", "")));
                channel.BasicPublish("", "letterbox", null, encodeMessage);
                //conn.Close();

            }
            else
            {
                return NotFound("Invalid Table or Action Name");
            }
            
            return Ok();
        }
    }
}
