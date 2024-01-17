

using DataAcess_SharedLibary.Entities;

namespace DataAcess_SharedLibary.Repositories;

public class UserRepository(string connectionString) : Repository<UserEntity>(connectionString)
{
}
