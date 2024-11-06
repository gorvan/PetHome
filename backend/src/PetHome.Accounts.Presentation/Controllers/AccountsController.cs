using Microsoft.AspNetCore.Mvc;
using PetHome.Accounts.Application.AccountsMenagement.Commands.Login;
using PetHome.Accounts.Application.AccountsMenagement.Commands.RefreshTokens;
using PetHome.Accounts.Application.AccountsMenagement.Commands.Register;
using PetHome.Accounts.Application.Contracts;
using PetHome.Accounts.Contracts.Responses;
using PetHome.Shared.Core.Extensions;
using PetHome.Shared.Framework.Controllers;

namespace PetHome.Accounts.Presentation.Controllers
{
    public class AccountsController : ApplicationContoller
    {
        [HttpPost("registration")]
        public async Task<IActionResult> Register(
            [FromBody] RegisterUserRequest request,
            [FromServices] RegisterUserHandler handler,
            CancellationToken token)
        {
            var command = request.ToCommand();
            var response = await handler.Execute(command, token);
            if (response.IsFailure)
            {
                return response.ToResponse();
            }
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login(
            [FromBody] LoginUserRequest request,
            [FromServices] LoginUserHandler handler,
            CancellationToken token)
        {
            var command = request.ToCommand();
            var result = await handler.Execute(command, token);
            if (result.IsFailure)
            {
                return result.ToResponse();
            }
            return Ok(result.Value);
        }

        [HttpPost("refresh")]
        public async Task<ActionResult<LoginResponse>> RefreshTokens(
            [FromBody] RefreshTokensRequest request,
            [FromServices] RefreshTokensHandler handler,
            CancellationToken token)
        {
            var command = request.ToCommand();
            var result = await handler.Execute(command, token);
            if (result.IsFailure)
            {
                return result.ToResponse();
            }
            return Ok(result.Value);
        }
    }
}
