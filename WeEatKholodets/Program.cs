using Microsoft.AspNetCore.Mvc.Razor;
using WeEatKholodets.Data;
using WeEatKholodets.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbConfig(builder.Configuration);

builder.Services.AddTransient<IMealRepository, EFMealRepository>();

builder.Services.AddIdentityAndAuthenticationConfig(builder.Configuration);

builder.Services.AddSessionConfig();

builder.Services.AddLocalizationConfig();

builder.Services.AddRazorPages()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddRazorRuntimeCompilation();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRequestLocalization();

app.UseSession();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
