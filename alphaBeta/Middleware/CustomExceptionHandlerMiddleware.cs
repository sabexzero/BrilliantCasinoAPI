using Newtonsoft.Json;
using System.Net;

using BrilliantCasinoAPI.Helpers.Exceptions;
using BrilliantCasinoAPI.Helpers.Exceptions.Player;
using BrilliantCasinoAPI.Helpers.Exceptions.Bet;

namespace BrilliantCasinoAPI.Middleware;
public class CustomExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
	public CustomExceptionHandlerMiddleware(RequestDelegate next) =>
        _next = next;
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception) 
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        var result = string.Empty;
        switch(exception)
        {
            case PlayerNotFoundException ex:
                code = HttpStatusCode.NotFound;
                result = ex.ErrorMessage;
                break;

            case SomethingWrongWithAddClaimOperationException ex:
                code = HttpStatusCode.BadRequest;
                result = ex.ErrorMessage;
                break;

            case PasswordBadException ex:
                code = HttpStatusCode.BadRequest;
                result = ex.ErrorMessage;
                break;

            case PlayerNameAlreadyExistException ex:
                code = HttpStatusCode.BadRequest;
                result = ex.ErrorMessage;
                break;

            case SomethingWrongWithDeletingProcessException ex:
                code = HttpStatusCode.InternalServerError;
                result = ex.ErrorMessage;
                break;

            case SomethingWrongWithUpdatingProcessException ex:
                code = HttpStatusCode.InternalServerError;
                result = ex.ErrorMessage;
                break;

            case ClaimNotFoundException ex:
                code = HttpStatusCode.BadRequest;
                result = ex.ErrorMessage;
                break;

            case SomethingWrongWithCreatingProcessException ex:
                code = HttpStatusCode.BadRequest;
                result = ex.ErrorMessage;
                break;

            case BetAlreadyExistException ex:
                code = HttpStatusCode.BadRequest;
                result = ex.ErrorMessage;
                break;

            case BetNotFoundException ex:
                code = HttpStatusCode.NotFound;
                result = ex.ErrorMessage;
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        var errorResponse = new ErrorResponse(result);
        var json = JsonConvert.SerializeObject(errorResponse);
        await context.Response.WriteAsync(json);
    }

}