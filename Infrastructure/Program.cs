using Core.Entities;
using Infrastructure.Gateways.Queue;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<BaseQueue<Contato>, BaseQueue<Contato>>(sp =>
{
    var factory = new ConnectionFactory() { HostName = "localhost", UserName = "guest", Password = "guest" };
    var connection = factory.CreateConnection();

    return new BaseQueue<Contato>(connection, "queue");
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
