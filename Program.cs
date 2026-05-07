using Magazyn;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
// rejestrujemy serwis do obslugi pliku JSON
builder.Services.AddScoped<Db>();

// logowanie przez cookie, bez Identity
builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", o =>
    {
        o.LoginPath = "/Account/Login";
        o.LogoutPath = "/Account/Logout";
    });
builder.Services.AddAuthorization();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
    app.UseExceptionHandler("/Error");

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.Run();
