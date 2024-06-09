using Polly;
using ShippingService.DAL;
using ShippingService.DAL.Interfaces;
using ShippingService.DTO;
using ShippingService.Models;
using ShippingService.Services;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IShipping, ShippingDAL>();
builder.Services.AddScoped<ICurrier, CurrierDAL>();

//register HttpClient
builder.Services.AddHttpClient<IOrderHeaderService, OrderHeaderService>().AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(6000)));

builder.Services.AddHttpClient<IUserService, UserService>().AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(6000)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/currier", (ICurrier currier) =>
{
    return Results.Ok(currier.GetAll());
})
.WithName("GetCurrier")
.WithOpenApi();

app.MapPost("/currier", (ICurrier currier, InsertCurrierDTO obj) =>
{
    try
    {
        Currier data = new Currier
        {
            CurrierName = obj.CurrierName,
            CurrierPhone = obj.CurrierPhone,
            CurrierAddress = obj.CurrierAddress,
        };
        currier.Insert(data);
        return Results.Created($"/currier/{obj.CurrierName}", data);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
})
.WithName("PostCurrier")
.WithOpenApi();

app.MapPut("/currier", (ICurrier currier, UpdateCurrierDTO obj) =>
{
    try
    {
        Currier data = new Currier
        {
            CurrierId = obj.CurrierId,
            CurrierName = obj.CurrierName,
            CurrierPhone = obj.CurrierPhone,
            CurrierAddress = obj.CurrierAddress,
        };
        currier.Update(data);
        return Results.Ok(new { success = true, message = "request update successful", data = data });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
})
.WithName("PutCurrier")
.WithOpenApi();

app.MapGet("/shipping", (IShipping shipping) =>
{
    return Results.Ok(shipping.GetAll());
})
.WithName("GetShipping")
.WithOpenApi();

app.MapPost("/shipping", async (IShipping shipping, IOrderHeaderService orderHeaderService, InsertShippingDTO obj, IUserService userService) =>
{
    try
    {
        // return Results.Ok(new{obj.OrderHeaderID, order});
        Shipping data = new Shipping
        {
            OrderHeaderId = obj.OrderHeaderId,
            CurrierId = obj.CurrierId,
            ShippingAddress = obj.ShippingAddress,
            ShippingVendor = obj.ShippingVendor,
            ShippingDate = obj.ShippingDate,
            ShippingStatus = obj.ShippingStatus,
            ShippingInformation = obj.ShippingInformation,
            EstimatedShipping = obj.EstimatedShipping,
            ItemWeight = obj.ItemWeight,
            ShippingCosts = 10000 * obj.ItemWeight
        };
        shipping.Insert(data);
        var order = await orderHeaderService.GetUserById(data.OrderHeaderId);
        if (order == null)
        {
            return Results.BadRequest("Order Header not found");
        }
        var userUpdateBalance = new UserUpdateBalance
        {
            UserName = order.userName,
            Balance = data.ShippingCosts
        };

        await userService.UpdateUserBalance(userUpdateBalance);
        return Results.Created($"/shipping/{data.ShippingId}", data);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
})
.WithName("PostShipping")
.WithOpenApi();

app.MapPut("/shipping", async (IShipping shipping, IOrderHeaderService orderHeaderService, UpdateOrderHeader obj, IUserService userService) =>
{
    try
    {
        Shipping data = new Shipping
        {
            ShippingId = obj.ShippingId,
            OrderHeaderId = obj.OrderHeaderId,
            CurrierId = obj.CurrierId,
            ShippingAddress = obj.ShippingAddress,
            ShippingVendor = obj.ShippingVendor,
            ShippingDate = obj.ShippingDate,
            ShippingStatus = obj.ShippingStatus,
            ShippingInformation = obj.ShippingInformation,
            EstimatedShipping = obj.EstimatedShipping,
            ItemWeight = obj.ItemWeight,
            ShippingCosts = obj.ShippingCosts
        };
        shipping.Insert(data);
        var order = await orderHeaderService.GetUserById(data.OrderHeaderId);
        if (order == null)
        {
            return Results.BadRequest("Order Header not found");
        }
        return Results.Ok(data);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
})
.WithName("PutShipping")
.WithOpenApi();

app.Run();
