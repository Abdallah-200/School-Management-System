using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using School_Management_System.Application.Interfaces;
using School_Management_System.Application.Mapping;
using School_Management_System.Application.Services;
using School_Management_System.Domain.Interfaces;
using School_Management_System.Infrastructure.Data;
using School_Management_System.Infrastructure.Repositories;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using System.IdentityModel.Tokens.Jwt;

namespace School_Management_System.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ── Controllers & Validation ──────────────────────────────────
            builder.Services.AddControllers()
                    .AddJsonOptions(options =>
                     {
                      options.JsonSerializerOptions.Converters.Add(
                      new JsonStringEnumConverter());
                     });
            builder.Services.AddValidatorsFromAssemblyContaining<Program>();
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            // ── Swagger + JWT auth button in Swagger UI ───────────────────
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "School Management API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name         = "Authorization",
                    Type         = SecuritySchemeType.Http,
                    Scheme       = "Bearer",
                    BearerFormat = "JWT",
                    In           = ParameterLocation.Header,
                    Description  = "Enter your JWT token (without 'Bearer ' prefix)"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            // ── Database ──────────────────────────────────────────────────
            builder.Services.AddDbContext<SchoolContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // ── Repositories ──────────────────────────────────────────────
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            // ✓ FIX: Register IUserRepository for efficient email/token lookups
            builder.Services.AddScoped<IUserRepository,         UserRepository>();
            builder.Services.AddScoped<ICourseRepository,       CourseRepository>();
            builder.Services.AddScoped<IStudentClassRepository, StudentClassRepository>();
            builder.Services.AddScoped<IAssignmentRepository,   AssignmentRepository>();
            builder.Services.AddScoped<IAttendanceRepository,   AttendanceRepository>();
            builder.Services.AddScoped<ISubmissionRepository,   SubmissionRepository>();
            // ✓ FIX: These were missing — caused runtime crashes on every request
            builder.Services.AddScoped<IClassRepository,        ClassRepository>();
            builder.Services.AddScoped<IGradeRepository,        GradeRepository>();
            builder.Services.AddScoped<INotificationRepository, NotificationRepository>();

            // ── Services ──────────────────────────────────────────────────
            builder.Services.AddScoped<IJwtService,                 JwtService>();
            builder.Services.AddScoped<IAuthService,                AuthService>();
            builder.Services.AddScoped<IUserService,                UserService>();
            builder.Services.AddScoped<IDepartmentService,          DepartmentService>();
            builder.Services.AddScoped<ICourseService,              CourseService>();
            builder.Services.AddScoped<IClassService,               ClassService>();
            builder.Services.AddScoped<IStudentClassService,        StudentClassService>();
            builder.Services.AddScoped<IAssignmentService,          AssignmentService>();
            // ✓ FIX: These 5 services were missing — half the app would crash at runtime
            builder.Services.AddScoped<IAttendanceService,          AttendanceService>();
            builder.Services.AddScoped<ISubmissionService,          SubmissionService>();
            builder.Services.AddScoped<INotificationService,        NotificationService>();
            builder.Services.AddScoped<IStudentGradeService,        StudentGradeService>();
            builder.Services.AddScoped<IStudentAttendanceService,   StudentAttendanceService>();
            builder.Services.AddScoped<IStudentAssignmentService,   StudentAssignmentService>();

            // ── JWT Authentication ────────────────────────────────────────
            var jwtKey      = builder.Configuration["Jwt:Key"]
                              ?? throw new InvalidOperationException("Jwt:Key is not configured.");
            var jwtIssuer = builder.Configuration["Jwt:Issuer"]
                ?? throw new Exception("Jwt:Issuer missing");

            var jwtAudience = builder.Configuration["Jwt:Audience"]
                ?? throw new Exception("Jwt:Audience missing");

            builder.Services
        .AddAuthentication(o =>
        {
            o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(o =>
        {
            o.RequireHttpsMetadata = !builder.Environment.IsDevelopment();
            o.SaveToken = true;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = jwtIssuer,
                ValidAudience = jwtAudience,
                IssuerSigningKey = new SymmetricSecurityKey(
                                   Encoding.UTF8.GetBytes(jwtKey)),

                RoleClaimType = ClaimTypes.Role,
                NameClaimType = ClaimTypes.NameIdentifier,

                ClockSkew = TimeSpan.FromMinutes(2)
            };
        });

            builder.Services.AddAuthorization();

            // ── CORS (Angular dev server) ─────────────────────────────────
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AngularDev", policy =>
                    policy.WithOrigins("http://localhost:4200")
                          .AllowAnyHeader()
                          .AllowAnyMethod());
            });

            // ── Build app ─────────────────────────────────────────────────
            var app = builder.Build();

            // ✓ FIX: Global exception handler — prevents stack traces leaking to clients
            app.UseExceptionHandler(errApp =>
            {
                errApp.Run(async ctx =>
                {
                    var feature = ctx.Features.Get<IExceptionHandlerFeature>();
                    var ex      = feature?.Error;

                    ctx.Response.ContentType = "application/problem+json";
                    ctx.Response.StatusCode  = ex switch
                    {
                        KeyNotFoundException     => StatusCodes.Status404NotFound,
                        UnauthorizedAccessException => StatusCodes.Status403Forbidden,
                        InvalidOperationException   => StatusCodes.Status400BadRequest,
                        _                           => StatusCodes.Status500InternalServerError
                    };

                    // Log ex here with ILogger / Serilog in a real app
                    await ctx.Response.WriteAsJsonAsync(new
                    {
                        status  = ctx.Response.StatusCode,
                        title   = ex?.Message ?? "An unexpected error occurred.",
                        traceId = ctx.TraceIdentifier
                    });
                });
            });

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseHttpsRedirection();
            }
            app.UseCors("AngularDev");
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
