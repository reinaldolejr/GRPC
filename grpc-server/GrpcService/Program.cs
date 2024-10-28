using GrpcService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyAllowSpecificOrigins",
        builder =>
        {
            builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

var app = builder.Build();

app.MapGrpcService<GreeterService>();
app.MapGrpcService<TimeServiceImpl>();
app.MapGet("/", () => "gRPC Service is running!");
app.UseCors("MyAllowSpecificOrigins");

app.Run();