using Jala.Custom.ModelBinder.ModelBinders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Registering a custom model binder provider in the list of provider to be use with a specified model(Page).
// builder.Services.AddMvc(config =>
// {
//     //config.ModelBinderProviders.Insert(0,new CustomModelBinderProvider());
//     
//     //Adding the custom biding provider to the beginning of the list
//     //then if the default binder match the ModelType it will not run before our custom one
//     config.ModelBinderProviders.Insert(0,new MultipleSourceModelBinderProvider());
// });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();