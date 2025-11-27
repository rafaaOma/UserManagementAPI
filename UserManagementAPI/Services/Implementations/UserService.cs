using UserManagement.Models;

public class UserService : IUserService
{
    private static List<User> users = new()
    {
        new User { Id = 1, name = "Alice", Email = "alice@example.com", Age = 30 },
        new User { Id = 2, name = "Bob", Email = "bob@example.com", Age = 25 }
    };
    public IEnumerable<User> GetAllUsers()
    {
        return users.AsReadOnly();
    }
     public User? GetUserById(int id)
    {
        if (id <= 0) return null;
        return users.FirstOrDefault(u => u.Id == id);
    }
    public void AddUser(User user)
    {
       var existingUser = GetUserById(user.Id);
       if (existingUser != null)
        {
            throw new ArgumentException("User with the same Id already exists.");
        }
        else{
        users.Add(user);
        }
    }
    public void UpdateUser(User user)
    {
        var existingUser = GetUserById(user.Id);
        if (existingUser == null)
        {
            throw new ArgumentException("User not found.");
        }
        else {
        existingUser.name = user.name;
        existingUser.Email = user.Email;
        existingUser.Age = user.Age;
        }
    }
    public void DeleteUser(int id)
    {
        var user = GetUserById(id);
        if (user == null)
        {
            throw new ArgumentException("User not found.");
        }
        users.Remove(user);
    }
}