using BookMyShowWebApplication.Signalr;
using BookMyShowWebApplication;
using BookMyShowWebApplicationModal.config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using GrpcSdk;
var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<DataConfig>(builder.Configuration.GetSection("ConnectionString"));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddSignalR();
builder.Services.AddServiceGrpc();
RegisterServices.RegisterService(builder.Services);

// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// Configure JWT Bearer authentication
builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
   
})
.AddJwtBearer(options => {
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidAudience = "http://localhost:5101",
        ValidIssuer = "http://localhost:5101",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]??string.Empty)),
        ClockSkew = TimeSpan.Zero
    };

    //cookies Token setup
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = jt =>
        {
            jt.Request.Cookies.TryGetValue("accessToken", out var accessToken);
            if (!string.IsNullOrEmpty(accessToken))
                jt.Token = accessToken;
            return Task.CompletedTask;
        }
    };
});


// Configure authorization policies
builder.Services.AddAuthorization(options =>
{
    // Define a policy named "AdminOnly" that requires the user to have the "Admin" role
    options.AddPolicy("AdminOnly", policy =>
    {
        policy.RequireRole("Admin");
    });
    options.AddPolicy("userOnly", policy =>
    {
        policy.RequireRole("user");
    });
   options.AddPolicy("TheatersOnly", policy =>
    {
        policy.RequireRole("Theaters");
    });
      options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

var app = builder.Build();
app.UseDefaultFiles();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors(policy =>
    policy
        .WithOrigins("http://localhost:4200") // Specify the exact origin
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials() // Allow credentials
);

// Order of middleware matters

app.UseAuthentication();  // Add this line to ensure authentication is applied
app.UseAuthorization();   // Ensure this is placed after UseRouting and UseAuthentication
//app.UseWebSockets();

// add the Signair
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<MessageHub>("/MessageHub");
});

await app.RunAsync();
