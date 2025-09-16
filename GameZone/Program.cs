var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add DB context to DI
var connectionString = builder.Configuration.GetConnectionString("Default");
var serverVersion = new MySqlServerVersion(new Version(9, 0, 0));
builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(connectionString, serverVersion));

builder.Services.AddScoped<ICategoriesService, CategoriesService>();
builder.Services.AddScoped<IDevicesService, DevicesService>();
builder.Services.AddScoped<IGamesService, GamesService>();

// We can AddOptions and validate it with data annotations or even with custom validatione through Validate()
builder.Services.AddOptions<FileConfiguration>()
                .Bind(builder.Configuration.GetSection("FileConfiguration"))
                .ValidateDataAnnotations() // Runs the DataAnnotations validations
                .ValidateOnStart(); // Fail startup if invalid

// Configure<T> registers the options but doesn't attach validation in one call.
// builder.Services.Configure<FileConfiguration>(builder.Configuration.GetSection("FileConfiguration"));

var app = builder.Build();

// Seed DB with Data
// using (var scope = app.Services.CreateScope())
// {
//     SeedData.Initialize(scope.ServiceProvider);
// }

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
