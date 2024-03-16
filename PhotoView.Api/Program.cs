using PhotoView.Api.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services
	.AddEndpointsApiExplorer()
	.AddSwaggerGen()
	.AddHttpContextAccessor()
	;
builder.Services.AddControllers()
	;
builder.Services.AddApiServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseMiddleware<ApiKeyMiddleware>();

app.MapControllers();

app.Run();