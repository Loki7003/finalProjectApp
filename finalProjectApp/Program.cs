var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Adds a default in-memory implementation of IDistributedCache.
builder.Services.AddDistributedMemoryCache();

builder.Services.AddHttpContextAccessor();

builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromMinutes(30);
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
});

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

// Use session
app.UseSession();

app.Use(async (context, next) =>
{
	if (context.Session.GetString("username") != null && context.Session.GetString("userid") != null)
	{
		// If the user is logged in (i.e., "username" and "userid" exist in the session), redirect to the User Index page
		context.Response.Redirect("/User/Index");
	}
	else
	{
		// If the user is not logged in, just call the next middleware in the pipeline
		await next.Invoke();
	}
});

app.UseStatusCodePages(async context =>
{

	var response = context.HttpContext.Response;

	if (response.StatusCode == 404)
	{
		response.Redirect("/Home/NotFound");
	} else if (response.StatusCode == 401)
	{
		response.Redirect("/Home/NotAuthorized");
	}


});

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();