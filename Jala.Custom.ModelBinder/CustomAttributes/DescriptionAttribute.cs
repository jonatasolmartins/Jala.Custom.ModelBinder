namespace Jala.Custom.ModelBinder.CustomAttributes;

[AttributeUsage(AttributeTargets.Field)]
public class DescriptionAttribute: Attribute
{
    public Type Page { get; }
    public string Description { get; }
    public DescriptionAttribute(string description)
    {
        Description = description;
    }

    public DescriptionAttribute(Type page)
    {
        Page = page;
    }
}