using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TheFoodHood.Web.Components;
using TheFoodHood.Web.Components.Account;
using TheFoodHood.Web.Data;
using TheFoodHood.Web.Data.Services;

var builder = WebApplication.CreateBuilder(args);

// --------------------- RAZOR + CIRCUITO ---------------------
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddCircuitOptions(options => { options.DetailedErrors = true; });


builder.Services.AddServerSideBlazor()
    .AddHubOptions(options =>
    {
        options.MaximumReceiveMessageSize = 10 * 1024 * 1024; // 10 MB
    });


// --------------------- IDENTITY AUTH + STATE ---------------------
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();

// --------------------- BASE DE DATOS ---------------------
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// --------------------- IDENTITY (con roles) ---------------------
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Permitir login sin confirmar email
    options.SignIn.RequireConfirmedEmail = false;
});
builder.Services.AddHttpContextAccessor();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromDays(14); // o el tiempo que desees
    options.SlidingExpiration = true;
    options.LoginPath = "/Account/Login";
});




// --------------------- SERVICIOS PERSONALIZADOS ---------------------
builder.Services.AddScoped<IArticuloService, ArticuloService>();
builder.Services.AddScoped<IPedidoService, PedidoService>();
builder.Services.AddScoped<IDetallePedidoService, DetallePedidoService>();
builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

// --------------------- MIDDLEWARE ---------------------
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();              // <--- IMPORTANTE
app.UseAuthentication();       // <--- CRÍTICO PARA LOGIN
app.UseAuthorization();        // <--- NECESARIO SI USAS ROLES

app.UseAntiforgery();

// --------------------- MAPEO DE COMPONENTES ---------------------
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapAdditionalIdentityEndpoints();

app.Run();
