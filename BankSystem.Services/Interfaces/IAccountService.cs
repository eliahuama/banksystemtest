public interface IAccountService
{
    Account? GetAccount(Guid accountId);
    IEnumerable<Account> GetAccounts(Guid? userId);
    Guid CreateAccount(Guid userId, double initialBalance);
    void DeleteAccount(Guid accountId);
    void Deposit(Guid accountId, double amount);
    void Withdraw(Guid accountId, double amount);
}