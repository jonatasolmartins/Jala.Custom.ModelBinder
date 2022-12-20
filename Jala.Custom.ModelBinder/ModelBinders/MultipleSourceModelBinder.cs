using Jala.Custom.ModelBinder.Controllers;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace Jala.Custom.ModelBinder.ModelBinders;

public class MultipleSourceModelBinder: IModelBinder
{
    public async Task BindModelAsync(ModelBindingContext bindingContext)
    {
        Page modelInstance = new();
        
        if (bindingContext.ActionContext.HttpContext.Request.Query.Count > 0)
        {
            var name = bindingContext.ActionContext.HttpContext.Request.Query["name"];
            modelInstance = new Page()
            {
                Id = 0,
                Name = name
            };
            
        }

        string valueFromBody;
       
        using (var streamReader = new StreamReader(bindingContext.HttpContext.Request.Body))
        {
            valueFromBody = await streamReader.ReadToEndAsync();
        }

        if (string.IsNullOrWhiteSpace(valueFromBody) && string.IsNullOrEmpty(valueFromBody))
        {
            bindingContext.Result = ModelBindingResult.Success(modelInstance);
            return;
        }
  
        try
        {
            var model = JsonConvert.DeserializeObject<Page>(
                valueFromBody, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
            
            modelInstance.Id = model.Id;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            bindingContext.Result = ModelBindingResult.Failed();
            return;
        }
        
        bindingContext.Result = ModelBindingResult.Success(modelInstance);
    }

}

public class MultipleSourceModelBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        return context.Metadata.ModelType == typeof(Page) ? new CustomModelBinder() : null;
    }
}