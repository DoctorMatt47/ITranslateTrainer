using Microsoft.AspNetCore.Mvc;

namespace ITranslateTrainer.WebApi.Controllers;

// [Authorize]
[ApiController]
[Route("api/[controller]")]
public abstract class ApiControllerBase : ControllerBase;
