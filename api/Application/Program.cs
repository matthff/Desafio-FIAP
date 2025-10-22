using Api.CrossCutting.Dependecies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Register your repositories (using scoped lifetime for DB contexts)
ConfigureRepository.ConfigureDependenciesRepository(builder.Services, builder.Configuration);

// Register your services
ConfigureService.ConfigureDependenciesServices(builder.Services);

// Register AutoMapper profiles
ConfigureAutoMapper.ConfigureAutoMapperProfiles(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
