using Jala.Custom.ModelBinder.CustomAttributes;
using Jala.Custom.ModelBinder.ModelBinders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;

namespace Jala.Custom.ModelBinder.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ModelBinderController : ControllerBase
{
    [HttpGet]
    public IActionResult Index([FromHeader(Name = "User-Agent")]string useragent)
    {
        return Ok(useragent);
    }

    [HttpGet]
    public IActionResult GetId([FromQuery] int id)
    {
        return Ok(id);
    }

    [HttpGet]
    public IActionResult GetEnumValue(int number)
    {
        var enumerator = (QualquerCoisa)number;
        var attribute = enumerator.GetAttributeOfType<DescriptionAttribute>();
        
        var valorDoAtributo = attribute?.Description;
        return Ok(valorDoAtributo ?? attribute.Page.Name);
    }
    
    [HttpPost]
    public IActionResult Create([FromQueryAndBody]Page page)
    {
        return Ok(page);
    }

    [HttpPost]
    public IActionResult Create2([FromQueryAndBody]Page2 page)
    {
        return Ok(page);
    }
}

public class Page
{
    public string? Name { get; set; }
    public int Id { get; set; }
}

public class Page2
{
    public string? Name { get; set; }
    public int Id { get; set; }
    public int Age { get; set; }
}

public enum QualquerCoisa: int
{
    [DescriptionAttribute("Texto1")]
    Valor1 = 1,
    [DescriptionAttribute(typeof(Page2))]
    Valor2 = 2
}