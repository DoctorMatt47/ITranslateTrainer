using Microsoft.AspNetCore.Mvc;

namespace ITranslateTrainer.WebUI.Controllers;

[Route("api/[controller]")]
public class TranslationsController : Controller
{
    [HttpPost("sheet")]
    public async Task CreateTranslationSheet([FromBody] IFormFile sheet)
    {
    }
}
