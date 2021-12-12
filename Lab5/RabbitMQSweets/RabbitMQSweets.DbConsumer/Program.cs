using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Microsoft.Data.SqlClient;

var factory = new ConnectionFactory()
{
    Uri = new Uri("amqp://guest:guest@localhost:5672")
};

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare("sweets-queue", true, false, false, null);

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (sender, e) =>
{
    var body = e.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine(message);
    SendToDb(message);
    Console.WriteLine("Send to db!");
};

channel.BasicConsume("sweets-queue", true, consumer);
Console.ReadLine();

void SendToDb(string candy)
{
    var connectionString = @"Data Source=DESKTOP-H1GPQOS\SQLEXPRESS01;Initial Catalog=Sweets;Integrated Security=True;Connect Timeout=30;Encrypt=False";
    
    var queryString = @$"INSERT INTO dbo.Sweet VALUES('{Guid.NewGuid()}', '{candy}');";

    using var sqlConnection = new SqlConnection(connectionString);
    
    var command = new SqlCommand(queryString, sqlConnection);
    command.Connection.Open();
    command.ExecuteNonQuery();
}

