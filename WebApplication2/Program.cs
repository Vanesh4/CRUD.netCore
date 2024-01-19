
using Rotativa.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


var app = builder.Build();

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


IWebHostEnvironment env = app.Environment;
//Rotativa.AspNetCore.RotativaConfiguration.Setup(env.WebRootPath, "../Rotativa/Windows");
string wkhtmltopdfPath = System.IO.Path.Combine(env.WebRootPath, "Rotativa", "wkhtmltopdf.exe");
string rutaRelativa = "Rotativa/Windows/wkhtmltopdf.exe";

// Utiliza Path.Combine para construir la ruta completa
string rutaCompleta = Path.Combine(env.WebRootPath, rutaRelativa);

//Rotativa.AspNetCore.RotativaConfiguration.Setup(env.WebRootPath, rutaCompleta);
RotativaConfiguration.Setup(env.WebRootPath, "Rotativa", wkhtmltopdfPath);





app.Run();
