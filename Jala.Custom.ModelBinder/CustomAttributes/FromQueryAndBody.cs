using Microsoft.AspNetCore.Mvc;

namespace Jala.Custom.ModelBinder.CustomAttributes;

[AttributeUsage(AttributeTargets.Parameter)]
//[AttributeUsage(AttributeTargets.All)]
public class FromQueryAndBody: ModelBinderAttribute
{
    public FromQueryAndBody():base(typeof(Jala.Custom.ModelBinder.ModelBinders.MultipleSourceModelBinder))
    {

    }
}