public interface IAccountService
{
    Account? GetAccount(Guid accountId);
    Guid CreateAccount(Guid userId, double initialBalance);
    void DeleteAccount(Guid accountId);
    void Deposit(Guid accountId, double amount);
    void Withdraw(Guid accountId, double amount);
}