using System.Text.Json.Serialization;
using Api.Middleware;
using Core.Data;
using Core.Model;
using Core.Services;
using Core.Services.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<DataContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))); 

// Add services to the container.
builder.Services.AddScoped<ICardService, CardService>();
builder.Services.AddScoped<IDeckService, DeckService>();
builder.Services.AddScoped<INoteService, NoteService>();
builder.Services.AddScoped<INoteTypeService, NoteTypeService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthManager, AuthManager>();
builder.Services.AddScoped<ICssService, CssService>();
builder.Services.AddScoped<IHtmlService, HtmlService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ITemplateService, TemplateService>();
builder.Services.AddScoped<IFlashcardAlgorithmService, FlashcardAlgorithmService>();


builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddIdentity<User, IdentityRole>(options =>
    {
        options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789/-._@+";
    })
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<DataContext>();
builder.AddCustomJwtMiddleware(); 
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    // app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "OpenAPI V1"));
    // app.UseReDoc(c =>
    // {
    //     c.DocumentTitle = "REDOC API Documentation";
    //     c.SpecUrl = "/openapi/v1.json";
    // });   
    // app.Services.UseSeedDatabaseMiddleware();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();