using Ecommerce.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Configuração do ambiente
if (builder.Environment.IsDevelopment())
{
    builder.Logging.SetMinimumLevel(LogLevel.Debug);
}

// Configuração dos serviços
builder.Services.AddEcommerceContext(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuração do pipeline de requisições HTTP
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ecommerce API");
        c.RoutePrefix = string.Empty;
    });
}

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
