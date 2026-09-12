using EquipLease.Api.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace EquipLease.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[ServiceFilter(typeof(ApiKeyAuthFilter))]
public class BaseController : ControllerBase
{
}
