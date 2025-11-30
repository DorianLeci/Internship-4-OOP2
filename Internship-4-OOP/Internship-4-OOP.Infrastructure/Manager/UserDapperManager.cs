using Internship_4_OOP.Domain.Entities.Users;

namespace Internship_4_OOP.Infrastructure.Manager;

public class UserDapperManager(string connectionString) :DapperManager<User>(connectionString){}
