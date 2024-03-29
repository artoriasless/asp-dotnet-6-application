using DakSite;
using DakSite.Middleware;
using DakSite.Services;
using Microsoft.EntityFrameworkCore;
using NSwag;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

#region 基础视图初始化逻辑
builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
builder.Services.AddOpenApiDocument(options =>
{
    options.PostProcess = document =>
    {
        document.Info = new OpenApiInfo
        {
            Version = "v1",
            Title = "Dak Site Server API",
            Description = "",
            TermsOfService = "",
        };
    };
});
#endregion

#region 允许跨域
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        }
    );
});
#endregion

#region 初始化 dbcontext ，连接 mysql 数据库
string connectionString = builder.Configuration.GetConnectionString("MySql") ?? "";

builder.Services.AddDbContext<MySqlDBContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});
#endregion

#region 注册 service 服务
// 添加应用初始化服务
builder.Services.AddHostedService<InitializeService>();

// 添加应用定时任务服务
builder.Services.AddHostedService<IntervalTaskService>();

builder.Services.AddSingleton<TestService>();
#endregion

WebApplication app = builder.Build();
bool isDev = app.Environment.EnvironmentName == "Debug";

if (isDev)
{
    // 开发环境下启用 Swagger
    app.UseOpenApi();
    app.UseSwaggerUi();
}
else
{
    // 生产环境，启用严格 https 模式
    app.UseHsts();
}

app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(name: "default", pattern: "{controller}/{action}");

if (isDev)
{
    // 开发环境，重定向到 nextjs 服务器
    app.UseMiddleware<RedirectToNextServerInDev>();
}
else
{
    // 生产环境，所有未匹配的请求，都返回 index.html ，由前端处理
    app.MapFallbackToFile("index.html");
}
app.Run();
