using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
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
        public IActionResult GetApplicationsData(string model)
        {
            //Create Connnection of Rabbit MQ
            var factory = new ConnectionFactory { HostName = "localhost" };
            var conn = factory.CreateConnection();
            
            // Create Channel and make queue
            var channel = conn.CreateModel();
            channel.QueueDeclare(queue: "letterbox", durable: false, exclusive: false, autoDelete: false, arguments: null);

        
            var encodeMessage = Encoding.Default.GetBytes(model);
            for (int i = 0; i < 5; i++)
            {
                channel.BasicPublish("", "letterbox", null, encodeMessage);
            }

            conn.Close();
            return Ok();
        }
    }
}
