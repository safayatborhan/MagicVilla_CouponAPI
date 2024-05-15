using AutoMapper;
using FluentValidation;
using MagicVilla_CouponAPI;
using MagicVilla_CouponAPI.Data;
using MagicVilla_CouponAPI.Models;
using MagicVilla_CouponAPI.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/api/coupons", (ILogger<Program> logger) =>
{
    logger.LogInformation("Getting all coupons");

    var response = new APIResponse
    {
        Result = CouponStore.coupons,
        IsSuccess = true,
        StatusCode = HttpStatusCode.OK,
    };
    return Results.Ok(response);
})
    .Produces<APIResponse>(200);



app.MapGet("/api/coupons/{id:int}", (int id) =>
{
    return Results.Ok(CouponStore.coupons.FirstOrDefault(u => u.Id == id));
})
    .Produces<IEnumerable<Coupon>>(200);



app.MapPost("/api/coupons", async (IMapper mapper, IValidator<CouponCreateDTO> validator, [FromBody] CouponCreateDTO couponCreateDto) =>
{
    var validationResult = await validator.ValidateAsync(couponCreateDto);
    var response = new APIResponse
    {
        IsSuccess = false,
        StatusCode = HttpStatusCode.BadRequest,
    };

    if (!validationResult.IsValid)
    {
        response.ErrorMessages.Add(validationResult.Errors.FirstOrDefault().ToString());
        return Results.BadRequest(response);
    }

    if (CouponStore.coupons.Any(x => string.Compare(x.Name, couponCreateDto.Name, StringComparison.OrdinalIgnoreCase) == 0))
    {
        response.ErrorMessages.Add("Coupon Name already exists!");
        return Results.BadRequest(response);
    }

    var coupon = mapper.Map<Coupon>(couponCreateDto);

    coupon.Id = CouponStore.coupons.OrderByDescending(x => x.Id).FirstOrDefault().Id + 1;
    CouponStore.coupons.Add(coupon);

    var couponDto = mapper.Map<CouponDTO>(coupon);

    response.Result = couponDto;
    response.IsSuccess = true;
    response.StatusCode = HttpStatusCode.Created;
    
    return Results.Ok(response);
    //return Results.Created($"/api/coupon/{couponDto.Id}", couponDto);
})
    .Accepts<CouponCreateDTO>("application/json")
    .Produces<APIResponse>(201)
    .Produces(400);

app.MapPut("/api/coupons", () =>
{

});

app.MapDelete("/api/coupons/{id:int}", () =>
{

});

//app.UseHttpsRedirection();

app.Run();
