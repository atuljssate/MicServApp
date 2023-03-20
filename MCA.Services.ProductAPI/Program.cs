using AutoMapper;
using MCA.Services.ProductAPI;
using MCA.Services.ProductAPI.DbContexts;
using MCA.Services.ProductAPI.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
sqlServerOptions => sqlServerOptions.CommandTimeout(300)));

IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddControllers();

//builder.Services.AddAuthentication("Bearer")
//    .AddJwtBearer("Bearer", options =>
//    {
//        options.Authority = "https://localhost:44311/";
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateAudience = false
//        };
//    });
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "https://sso.voith.com/am/oauth2";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false
        };
    });
//builder.Services.AddAuthentication("Bearer")
//    .AddJwtBearer("Bearer", options =>
//    {
//        //options.Authority = "https://sso.voith.com/am/oauth2";
//        options.Authority =  "https://localhost:44311/";
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//             ValidateAudience = false
//            //ValidIssuer = "https://sso.voith.com/am/oauth2",
//            //ValidateIssuerSigningKey = false,
//            //ValidateIssuer = false,
//            //ValidateLifetime = false,
//            //ValidAudience = "http://localhost:4200",//ValidAudience = "2c744fhbdu94inn8u4sv4kg0ft",
//            //ValidateAudience = false,
//            //IssuerSigningKeyResolver = (s, securityToken, identifier, parameters) =>
//            //{
//            //    var keys = new HttpClient().GetFromJsonAsync<JsonWebKeySet>(parameters.ValidIssuer + "/.well-known/openid-configuration");
//            //    return (IEnumerable<SecurityKey>)keys;
//            //},


//            //IssuerSigningKeyResolver = (string token, SecurityToken securityToken, string kid, TokenValidationParameters validationParameters) =>
//            //{

//            //    List<SecurityKey> keys = new List<SecurityKey>();

//            //    var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("xgEUq/ZaaTdwRJqnjHL11xP42EU="));
//            //    keys.Add(signingKey);
//            //    var signingKey1 = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("J8F4rISnZ5nAMoxI1acTOscvE+Q="));
//            //    keys.Add(signingKey1);
//            //    return keys;
//            //},

//            //IssuerSigningKeyResolver = async (s, securityToken, identifier, parameters) =>
//            //{
//            //    // get JsonWebKeySet from AWS
//            //    ConfigurationManager<OpenIdConnectConfiguration> configurationManager =
//            //                new ConfigurationManager<OpenIdConnectConfiguration>(parameters.ValidIssuer + "/.well-known/jwks.json", new OpenIdConnectConfigurationRetriever());
//            //    OpenIdConnectConfiguration openIdConnectConfiguration = await configurationManager.GetConfigurationAsync(CancellationToken.None).ConfigureAwait(false);
//            //    return openIdConnectConfiguration.SigningKeys;
//            //},


//        };
//    });

//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("ApiScope", policy =>
//    {
//        policy.RequireAuthenticatedUser();
//        policy.RequireClaim("scope", "msa");
//    });
//});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MSA.Services.ProductAPI", Version = "v1" });
    c.EnableAnnotations();
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"Endter 'Bearer' [space] and your token",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        { new OpenApiSecurityScheme
        {
            Reference=new OpenApiReference{
                Type=ReferenceType.SecurityScheme,
                Id="Bearer"
            },
            Scheme="oauth2",
            Name="Bearer",
            In=ParameterLocation.Header
        },
        new List<string>()
    }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
