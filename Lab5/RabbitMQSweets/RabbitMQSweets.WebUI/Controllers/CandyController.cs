using Microsoft.AspNetCore.Mvc;
using RabbitMQSweets.WebUI.Models;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQSweets.WebUI.Entities;

namespace RabbitMQSweets.WebUI.Controllers;

public class CandyController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult CreateCandy(CandyModel candyModel)
    {
        var candy = MapToCandy(candyModel);
        
        if (!ModelState.IsValid)
            return View("Index", candyModel);
        
        PublishCandyToRabbitMq(candy);

        return View("Index");
    }
    
    static void PublishCandyToRabbitMq(Candy candy)
    {
        var factory = new ConnectionFactory()
        {
            Uri = new Uri("amqp://guest:guest@localhost:5672")
        };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare("sweets-queue", true, false, false, null);

        var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(candy));
        
        channel.BasicPublish(string.Empty, "sweets-queue", null, body);
    }

    Candy MapToCandy(CandyModel model)
    {
        string[] ingredients = null;
        try
        {
            ingredients = model.Ingredients.Trim().Split(',');
        }
        catch
        {
            ModelState.AddModelError("Ingredients", "Invalid ingredients list");
        }

        CandyType[] candyTypes = null;
        try
        {
           candyTypes = model.CandyTypes.Trim().Split(',').Select(x => (CandyType)Enum.Parse(typeof(CandyType), x)).ToArray();
        }
        catch
        {
            ModelState.AddModelError("CandyTypes", "Invalid candy types list");
        }
        
        var candyValue = new CandyValue
        {
            Carbohydrates = model.Carbohydrates,
            Fats = model.Fats,
            Proteins = model.Proteins
        };
        
        var candy = new Candy
        {
            Id = Guid.NewGuid().ToString(),
            Name = model.Name,
            Production = model.Production,
            Energy = model.Energy,
            Ingredients = ingredients,
            CandyTypes = candyTypes,
            CandyValue = candyValue
        };

        return candy;
    }
}