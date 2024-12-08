using EquipLease.WebApi.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace EquipLease.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[ServiceFilter(typeof(ApiKeyAuthFilter))]
public class BaseController : ControllerBase
{
}