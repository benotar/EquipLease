using Microsoft.AspNetCore.Mvc;

namespace EquipLease.Api.Infrastructure;

public class CustomValidationProblemDetails : ProblemDetails
{
    public Dictionary<string, string[]> Errors { get; set; }
}
