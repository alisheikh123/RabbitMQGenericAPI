using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Producor_Web_API.Interface;
using Producor_Web_API.Service;
using RabbitMQ.Client;
using System.Text;

namespace Producor_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IRabitMQProducer _rabitMQProducer;

        public PublisherController(IRabitMQProducer rabitMQProducer)
        {
            _rabitMQProducer = rabitMQProducer;
        }
        [HttpPost("/publish")]
        public IActionResult PublishMessage([FromBody] string message)
        {
            _rabitMQProducer.SendProductMessage(message);

            return Ok("Message published to RabbitMQ");
        }
    }
}
