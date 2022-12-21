using System.Web;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace Jala.Custom.ModelBinder.ModelBinders;

public class MultipleSourceModelBinder: IModelBinder
{
    public async Task BindModelAsync(ModelBindingContext bindingContext)
    {
        //modelType is the type of the param in the controller action
        var modelType = bindingContext.ModelMetadata.UnderlyingOrModelType;
        object modelInstance = null;
        
        if (bindingContext.ActionContext.HttpContext.Request.QueryString.HasValue)
        {
            //Value gona be something like ?name=peter&id=22
            var value = bindingContext.ActionContext.HttpContext.Request.QueryString.Value;
            var jsonString = ExtractDataFromQuery(value);
            try
            {
               modelInstance = JsonConvert.DeserializeObject(jsonString, modelType);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                bindingContext.Result = ModelBindingResult.Failed();
                return;
            }
            
            bindingContext.Result = ModelBindingResult.Success(modelInstance);
            return;

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
            modelInstance = JsonConvert.DeserializeObject(
                valueFromBody, modelType, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            bindingContext.Result = ModelBindingResult.Failed();
            return;
        }
        
        bindingContext.Result = ModelBindingResult.Success(modelInstance);
    }

    private static string ExtractDataFromQuery(string value)
    {
        //Converts ?name=peter&id=22 to a key value pair object
        var nameValueCollection = HttpUtility.ParseQueryString(value);
        //Then converts it to a dictionary 
        var dictionary = nameValueCollection.OfType<string>()
            .ToDictionary(s => s, s => nameValueCollection[s]);
        //Now we are able to serialize to a json format which is better fot us to deserialize back to the model type
        return JsonConvert.SerializeObject(dictionary);
    }

}

// public class MultipleSourceModelBinderProvider : IModelBinderProvider
// {
//     public IModelBinder? GetBinder(ModelBinderProviderContext context)
//     {
//         var attributes = context.Metadata.ModelType.GetCustomAttributes(typeof(FromQueryAndBody), false);
//         if (attributes.Length > 0)
//         {
//             return new MultipleSourceModelBinder();
//         }
//
//         return null;
//     }
// }