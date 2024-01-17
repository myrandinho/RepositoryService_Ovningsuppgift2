

using DataAcess_SharedLibary.Entities;
using DataAcess_SharedLibary.Models;
using DataAcess_SharedLibary.Repositories;
using System.Diagnostics;

namespace DataAcess_SharedLibary.Services;

public class UserService(UserRepository userRepository, RoleService roleService)
{
    private readonly UserRepository _UserRepository = userRepository;
    private readonly RoleService _roleService = roleService;
    


    public bool CreateUser(User user)
    {

        try
        {
            var roleEntity = _roleService.GetOrCreateRoleName(user.RoleName);

            if (roleEntity != null)
            {
                _UserRepository.Execute("INSERT INTO Users VALUES (@FirstName, @LastName, @Email, @RoleId)", new UserEntity
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    RoleId = roleEntity.Id,
                });
            }
            return true;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;

    }

    public bool GetAllUsers()
    {
        var users = _UserRepository.GetAllFromUserModel("SELECT u.FirstName, u.LastName, u.Email, r.RoleName FROM Users u JOIN Roles r ON u.RoleId = r.Id");

        foreach (var u in users)
        {
            Console.WriteLine($"First Name: {u.FirstName}");
            Console.WriteLine($"Last Name: {u.LastName}");
            Console.WriteLine($"Email: {u.Email}");
            Console.WriteLine($"RoleName: {u.RoleName}");
            Console.WriteLine();

        }
        return true;
    }

    public bool SelectUser(string email)
    {
        var userEntity = _UserRepository.Select("SELECT u.FirstName, u.LastName, u.Email, r.RoleName FROM Users u JOIN Roles r ON u.RoleId = r.Id WHERE u.Email = @Email", email).SingleOrDefault();
        if (userEntity != null)
        {
            Console.WriteLine($"First Name: {userEntity.FirstName}");
            Console.WriteLine($"Last Name: {userEntity.LastName}");
            Console.WriteLine($"Email: {userEntity.Email}");
            Console.WriteLine($"RoleName: {userEntity.RoleName}");
            return true;
        }
        else { return false; }
    }

    public bool DeleteUser(string email)
    {
        var userDelete = _UserRepository.Delete("DELETE FROM Users WHERE Email = @Email", email);
        if (userDelete == true)
        {
            return true;
        
        }
        else
            return false;

    }

    public bool UpdateUser(string query, string email)
    {
        bool userUpdate = _UserRepository.Update(query, email);
        if(userUpdate == true)
        {
            return true;
        }
        else
            return false;
    }

    public bool CheckIfUserExists(string query, string email, User user)
    {
        var checking_user = _UserRepository.ReadCheck(query, email, user);
        return checking_user.Any();
    }
     




    
    
}
