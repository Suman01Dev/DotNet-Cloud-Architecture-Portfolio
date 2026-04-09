var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// REMOVED: builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// REMOVED: The entire if(app.Environment.IsDevelopment()) block containing app.UseSwagger()

app.UseCors("AllowAngular");
app.UseAuthorization();
app.MapControllers();
app.Run();