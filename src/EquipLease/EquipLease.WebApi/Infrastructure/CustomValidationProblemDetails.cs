using Microsoft.AspNetCore.Mvc;

namespace EquipLease.WebApi.Infrastructure;

public class CustomValidationProblemDetails : ProblemDetails
{
    public Dictionary<string, string[]> Errors { get; set; }
}