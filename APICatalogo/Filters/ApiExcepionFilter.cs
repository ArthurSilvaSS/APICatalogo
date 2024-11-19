using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APICatalogo.Filters;

public class ApiExcepionFilter : IExceptionFilter
{
    private readonly ILogger<ApiExcepionFilter> _logger;
    public ApiExcepionFilter(ILogger<ApiExcepionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        _logger.LogError(context.Exception, "Ocoreu uma execucao nao tratada: status code 500");

        context.Result = new ObjectResult("Ocorreu um problema ao tratar a sua solicitacao: Status code 500")
        {
            StatusCode = StatusCodes.Status500InternalServerError,
        };

    }
}
