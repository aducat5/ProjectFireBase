using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using static Google.Cloud.Firestore.V1.StructuredQuery.Types;
using System.Security.Cryptography.X509Certificates;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//adding CORS policy for dev client
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDevClient", b =>
    {
        b.WithOrigins("http://localhost:5173")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var projectId = "fir-denemesi-7ffbe";

builder.Services
  .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options => {
      var isProduction = builder.Environment.IsProduction();
      var issuer = $"https://securetoken.google.com/{projectId}";
      options.Authority = issuer;
      options.TokenValidationParameters.ValidAudience = projectId;
      options.TokenValidationParameters.ValidIssuer = issuer;
      options.TokenValidationParameters.ValidateIssuer = isProduction;
      options.TokenValidationParameters.ValidateAudience = isProduction;
      options.TokenValidationParameters.ValidateLifetime = isProduction;
      options.TokenValidationParameters.RequireSignedTokens = isProduction;

      if (isProduction)
      {
          var jwtKeySetUrl = "https://www.googleapis.com/robot/v1/metadata/x509/securetoken@system.gserviceaccount.com";
          options.TokenValidationParameters.IssuerSigningKeyResolver = (s, securityToken, identifier, parameters) => {
              // get JsonWebKeySet from AWS
              var keyset = new HttpClient()
                  .GetFromJsonAsync<Dictionary<string, string>>(jwtKeySetUrl).Result;

              // serialize the result
              var keys = keyset!.Values.Select(
                  d => new X509SecurityKey(new X509Certificate2(Encoding.UTF8.GetBytes(d))));

              // cast the result to be the type expected by IssuerSigningKeyResolver
              return keys;
          };
      }
  });

builder.Services.AddAuthorization();


// Add services to the container.

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

//use the stated dev policy
app.UseCors("AllowAngularDevClient");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
