public class AccountService : IAccountService
{

    private readonly BankContext _context;

    public AccountService(BankContext context)
    {
        _context = context;
    }

    public Account GetAccount(Guid accountId)
    {
        var account = _context.Accounts.FirstOrDefault(a => a.Id == accountId);
        return account;
    }

    public Guid CreateAccount(Guid userId, double balance)
    {
        var account = new Account { Balance = balance > 100 ? balance : 100, UserId = userId};
        _context.Accounts.Add(account);
        _context.SaveChanges();

        return account.Id;
    }

    public void DeleteAccount(Guid accountId)
    {
        var account = _context.Accounts.FirstOrDefault(x => x.Id == accountId);
        if (account == null) return;    

        _context.Accounts.Remove(account);
        _context.SaveChanges();
    }

    public void Deposit(Guid accountId, double amount)
    {
        var account = _context.Accounts.FirstOrDefault(x => x.Id == accountId);
        if (account == null)
            throw new AccountNotFoundException();

        if (amount > 10000)
            throw new InvalidOperationException("Cannot deposit more than $10,000 in a single transaction.");

        account.Balance += amount;
        _context.Update(account);
        _context.SaveChanges();
    }

    public void Withdraw(Guid accountId, double amount)
    {
        var account = _context.Accounts.FirstOrDefault(x => x.Id == accountId);
        if (account == null)
            throw new AccountNotFoundException();

        if (account.Balance - amount < 100)
            throw new InvalidOperationException("An account cannot have less than $100 at any time.");

        if (amount > account.Balance * 0.9)
        {
            throw new InvalidOperationException("A user cannot withdraw more than 90% of their total balance from an account in a single transaction.");
        }

        account.Balance -= amount;

        _context.Update(account);
        _context.SaveChanges();
    }
}