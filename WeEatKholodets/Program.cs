using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.Razor;
using WeEatKholodets.Data;
using WeEatKholodets.Hubs;
using WeEatKholodets.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbConfig(builder.Configuration);

builder.Services.AddTransient<IMealRepository, EFMealRepository>();
builder.Services.AddTransient<IRecipeRepository, EFRecipeRepository>();
builder.Services.AddTransient<IUserRepository, EFUserRepository>();
builder.Services.AddScoped<IEmailSender, EmailService>();

builder.Services.AddSignalR();

builder.Services.AddIdentityAndAuthenticationConfig(builder.Configuration);

builder.Services.AddSessionConfig();

builder.Services.AddLocalizationConfig();

builder.Services.AddRazorPages()
    .AddDataAnnotationsLocalization()
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
app.UseStaticFiles(new StaticFileOptions()
{
    OnPrepareResponse = ctx =>
    {
        ctx.Context.Response.Headers.Add("Cache-Control", "public,max-age=600");
    }
});

app.UseSession();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseRequestLocalization();

app.MapRazorPages();
app.MapHub<MealsHub>("/mealHub");

app.Run();
