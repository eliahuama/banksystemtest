public interface IUserService
{
    Guid CreateUser(string name);
    void DeleteUser(Guid userId);
    User? GetUser(Guid userId);
}