using Microsoft.AspNetCore.Mvc;

namespace Jala.Custom.ModelBinder.CustomAttributes;

[AttributeUsage(AttributeTargets.Parameter)]
//[AttributeUsage(AttributeTargets.All)]
public class FromQueryAndBody: ModelBinderAttribute
{
    //public readonly Type? ParamType;
    public FromQueryAndBody(/*Type? paramType*/):base(typeof(Jala.Custom.ModelBinder.ModelBinders.MultipleSourceModelBinder))
    {
        //ParamType = paramType;
    }
}