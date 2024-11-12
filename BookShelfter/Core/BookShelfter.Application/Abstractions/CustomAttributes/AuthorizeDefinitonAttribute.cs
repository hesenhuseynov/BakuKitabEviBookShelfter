using BookShelfter.Application.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace BookShelfter.Application.Abstractions.CustomAttributes;

public class AuthorizeDefinitonAttribute:Attribute
{
    public string Menu { get; set; }
    public string Definition { get; set; }
    public ActionType ActionType { get; set; }




}