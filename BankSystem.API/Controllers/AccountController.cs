using BankSystem.Models.DTO;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }


    [HttpGet("{accountId}")]
    public ActionResult<Account> GetAccount(Guid accountId)
    {
        try
        {
            var account = _accountService.GetAccount(accountId);
            if (account == null)
                return NotFound();
            return Ok(account);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public ActionResult<Guid> CreateAccount(CreateAccountParams? model)
    {
        try
        {
            if (model == null)
                return BadRequest();
            var accountId = _accountService.CreateAccount(model.UserId, model.InitialBalance);
            return Ok(accountId);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{accountId}")]
    public IActionResult DeleteAccount(Guid accountId)
    {
        try
        {
            _accountService.DeleteAccount(accountId);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{accountId}/deposit")]
    public IActionResult Deposit(Guid accountId, DepositParams model)
    {
        try
        {
            _accountService.Deposit(accountId, model.Amount);
            return Ok();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (AccountNotFoundException ex)
        {
            return NotFound();
        }
    }

    [HttpPost("{accountId}/withdraw")]
    public IActionResult Withdraw(Guid accountId, DepositParams model)
    {
        try
        {
            _accountService.Withdraw(accountId, model.Amount);
            return Ok();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (AccountNotFoundException ex)
        {
            return NotFound();
        }
    }
}