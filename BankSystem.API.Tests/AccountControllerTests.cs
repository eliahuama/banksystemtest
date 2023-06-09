using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using System.Net;

[TestFixture]
public class AccountControllerE2ETests
{
    private WebApplicationFactory<Program> _factory;
    private HttpClient _client;

    [SetUp]
    public void SetUp()
    {
        // Arrange
        _factory = new WebApplicationFactory<Program>();
        _client = _factory.CreateClient();
    }

    [TearDown]
    public void TearDown()
    {
        _factory.Dispose();
        _client.Dispose();
    }

    [Test]
    public async Task CreateAccount_ShouldReturn_NewAccountId()
    {
        var response = await _client.PostAsJsonAsync("api/account", new { userId = Guid.NewGuid(), initialBalance = 200 });

        response.EnsureSuccessStatusCode();

        var accountId = await response.Content.ReadFromJsonAsync<Guid>();

        Assert.AreNotEqual(Guid.Empty, accountId);
    }

    [Test]
    public async Task GetAccount_ShouldReturn_AccountDetails()
    {
        var userId = Guid.NewGuid();
        var createResponse = await _client.PostAsJsonAsync("api/account", new { userId = userId, initialBalance = 200 });

        createResponse.EnsureSuccessStatusCode();

        var accountId = await createResponse.Content.ReadFromJsonAsync<Guid>();

        var getResponse = await _client.GetAsync($"api/account/{accountId}");

        getResponse.EnsureSuccessStatusCode();

        var account = await getResponse.Content.ReadFromJsonAsync<Account>();

        Assert.AreEqual(accountId, account.Id);
        Assert.AreEqual(200, account.Balance);
    }

    [Test]
    public async Task Deposit_ShouldIncrease_AccountBalance()
    {
        var userId = Guid.NewGuid();
        var createResponse = await _client.PostAsJsonAsync("api/account", new { userId = userId, initialBalance = 200 });

        createResponse.EnsureSuccessStatusCode();

        var accountId = await createResponse.Content.ReadFromJsonAsync<Guid>();

        var depositResponse = await _client.PostAsJsonAsync($"api/account/{accountId}/deposit", new { amount = 1000 });

        depositResponse.EnsureSuccessStatusCode();

        var getResponse = await _client.GetAsync($"api/account/{accountId}");

        getResponse.EnsureSuccessStatusCode();

        var account = await getResponse.Content.ReadFromJsonAsync<Account>();

        Assert.AreEqual(1200, account.Balance);
    }

    [Test]
    public async Task Withdraw_ShouldDecrease_AccountBalance()
    {
        var userId = Guid.NewGuid();
        var createResponse = await _client.PostAsJsonAsync("api/account", new { userId = userId, initialBalance = 200 });

        createResponse.EnsureSuccessStatusCode();

        var accountId = await createResponse.Content.ReadFromJsonAsync<Guid>();

        var withdrawResponse = await _client.PostAsJsonAsync($"api/account/{accountId}/withdraw", new { amount = 50 });

        withdrawResponse.EnsureSuccessStatusCode();

        var getResponse = await _client.GetAsync($"api/account/{accountId}");

        getResponse.EnsureSuccessStatusCode();

        var account = await getResponse.Content.ReadFromJsonAsync<Account>();

        Assert.AreEqual(150, account.Balance);
    }

    [Test]
    public async Task DeleteAccount_ShouldRemove_Account()
    {
        var userId = Guid.NewGuid();
        var createResponse = await _client.PostAsJsonAsync("api/account", new { userId = userId, initialBalance = 200 });

        createResponse.EnsureSuccessStatusCode();

        var accountId = await createResponse.Content.ReadFromJsonAsync<Guid>();

        var deleteResponse = await _client.DeleteAsync($"api/account/{accountId}");

        deleteResponse.EnsureSuccessStatusCode();

        var getResponse = await _client.GetAsync($"api/account/{accountId}");

        Assert.AreEqual(HttpStatusCode.NotFound, getResponse.StatusCode);
    }

    [Test]
    public async Task Withdraw_Over90Percent_ShouldFail()
    {
        var userId = Guid.NewGuid();
        var createResponse = await _client.PostAsJsonAsync("api/account", new { userId = userId, initialBalance = 200 });

        createResponse.EnsureSuccessStatusCode();

        var accountId = await createResponse.Content.ReadFromJsonAsync<Guid>();

        var withdrawResponse = await _client.PostAsJsonAsync($"api/account/{accountId}/withdraw", new { amount = 180 });

        Assert.AreEqual(HttpStatusCode.BadRequest, withdrawResponse.StatusCode);
    }

    [Test]
    public async Task Deposit_MoreThan10000_ShouldFail()
    {
        var userId = Guid.NewGuid();
        var createResponse = await _client.PostAsJsonAsync("api/account", new { userId = userId, initialBalance = 200 });

        createResponse.EnsureSuccessStatusCode();

        var accountId = await createResponse.Content.ReadFromJsonAsync<Guid>();

        var depositResponse = await _client.PostAsJsonAsync($"api/account/{accountId}/deposit", new { amount = 20000 });

        Assert.AreEqual(HttpStatusCode.BadRequest, depositResponse.StatusCode);
    }


}