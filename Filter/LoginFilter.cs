using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace market.Filter;

public class LoginFilter : ActionFilterAttribute
{
    private readonly ILogger<LoginFilter> _logger;

    public LoginFilter(ILogger<LoginFilter> logger)
    {
        _logger = logger;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var request = context.HttpContext.Request;
        var url = $"{request.Scheme}://{request.Host}{request.Path}{request.QueryString}";
        _logger.LogInformation(url);
        
        string? memberId = context.HttpContext.Session.GetString("LoginSession");
        if (string.IsNullOrEmpty(memberId))
        {
            context.Result = new UnauthorizedResult();
            _logger.LogError("request rejected");
        }
        base.OnActionExecuting(context);
    }
}