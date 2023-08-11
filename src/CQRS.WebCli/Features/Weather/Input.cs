using CQRS.WebCli.Models;
using Oakton;

namespace CQRS.WebCli.Features.Weather;

public class Input : BaseInput
{
    [FlagAlias("city", 'c')]
    [Description("City")]
    public string CityFlag { get; set; }

    // public bool? Flex { get; set; }
}
