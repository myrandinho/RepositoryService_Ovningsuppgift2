

using DataAcess_SharedLibary.Entities;
using DataAcess_SharedLibary.Repositories;
using System.Diagnostics;

namespace DataAcess_SharedLibary.Services;

public class RoleService(RoleRepository roleRepository)
{
    private readonly RoleRepository _roleRepository = roleRepository;



    public RoleEntity GetOrCreateRoleName(string roleName)
    {
        try
        {
            var roleEntity = _roleRepository.GetOne("SELECT Id FROM Roles WHERE RoleName = @RoleName", new RoleEntity { RoleName = roleName });
            if (roleEntity == null)
            {
                _roleRepository.Execute("INSERT INTO Roles VALUES (@RoleName)", new RoleEntity { RoleName = roleName });
                roleEntity = _roleRepository.GetOne("SELECT Id FROM Roles WHERE RoleName = @RoleName", new RoleEntity { RoleName = roleName });
            }
            return roleEntity;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }
}
