using Jala.Custom.ModelBinder.CustomAttributes;
using Jala.Custom.ModelBinder.ModelBinders;
using Microsoft.AspNetCore.Mvc;

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
        //var valorDoAtributo = QualquerCoisa.Valor1
        //return Ok(valorDoAtributo);
        return Ok(id);
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

// public enum QualquerCoisa
// {
//     [Attributo("Texto")]
//     Valor1,
//     [Attributo("Texto")]
//     Valor2
// }