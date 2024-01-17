

using DataAcess_SharedLibary.Entities;

namespace DataAcess_SharedLibary.Repositories;

public class RoleRepository(string connectionString) : Repository<RoleEntity>(connectionString)
{
}
