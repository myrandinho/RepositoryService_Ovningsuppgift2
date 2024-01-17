

using DataAcess_SharedLibary.Entities;
using DataAcess_SharedLibary.Models;
using DataAcess_SharedLibary.Services;
using System.Diagnostics;

namespace UserApp_RepositoryService.LocalServices;

internal class MenuService(UserService userService)
{
    private readonly UserService _userService = userService;



    public void MainMenu()
    {
        var user = new User();


        while (true)
        {
            Console.Clear();
            Console.WriteLine("## User Menu ##");
            Console.WriteLine();
            Console.WriteLine("1. Create user");
            Console.WriteLine("2. Show all users");
            Console.WriteLine("3. Show one user");
            Console.WriteLine("4. Update user");
            Console.WriteLine("5. Delete user");
            Console.WriteLine("0. Exit application");
            Console.Write("Your option: ");
            string option = Console.ReadLine();
            Console.Clear();

            switch (option)
            {
                case "1":
                    Console.Write("First Name: ");
                    user.FirstName = Console.ReadLine();

                    Console.Write("Last Name: ");
                    user.LastName = Console.ReadLine();

                    Console.Write("Email: ");
                    user.Email = Console.ReadLine();

                    Console.Write("Job Position: ");
                    user.RoleName = Console.ReadLine();

                    var result = _userService.CreateUser(user);
                    if (result)
                    {
                        
                        Console.WriteLine("User was created.");
                        Console.ReadKey();
                    }
                        

                    else
                        Console.WriteLine("Something went wrong.");

                    Console.ReadKey();
                    Console.Clear();
                    break;



                case "2":
                    _userService.GetAllUsers();
                    Console.ReadKey();
                    Console.Clear();
                    break;

                case "3":
                    Console.Write("Email: ");
                    string email = Console.ReadLine();
                    Console.Clear();

                    _userService.SelectUser(email);

                    Console.ReadKey();
                    Console.Clear();


                    break;

                case "4":
                    Console.Write("Email: ");
                    string userMailUpdate = Console.ReadLine();
                    Console.Clear();

                    var checkingUser = _userService.CheckIfUserExists("SELECT * FROM Users WHERE Email = @Email", userMailUpdate, new User 
                    {
                        
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        RoleName = user.RoleName
                    });

                    if (checkingUser)
                    {
                        
                        Console.WriteLine($"## Selected Email: {user.Email} ##");
                        Console.WriteLine($"1. {user.FirstName}");
                        Console.WriteLine($"2. {user.LastName}");
                        Console.WriteLine($"3. {user.RoleName}");
                        Console.Write("Select what to update (1-3): ");
                        string updateOption = Console.ReadLine();
                        Console.Clear();

                        switch (updateOption)
                        {
                            case "1":
                                Console.Clear();
                                Console.Write("Enter new First Name: ");
                                string newFirstName = Console.ReadLine();

                                var updateResult = _userService.UpdateUser($"UPDATE users SET FirstName = '{newFirstName}' WHERE Email = @Email", userMailUpdate);
                                if (updateResult)
                                {
                                    user.FirstName = newFirstName;
                                    Console.WriteLine("First Name updated successfully.");
                                }
                                else
                                {
                                    Console.WriteLine("Failed to update First Name. Please try again.");
                                }

                                break;
                            case "2":
                                Console.Clear();
                                Console.Write("Enter new Last Name: ");
                                string newLastName = Console.ReadLine();

                                var updateLastnameResult = _userService.UpdateUser($"UPDATE users SET LastName = '{newLastName}' WHERE Email = @Email", userMailUpdate);
                                if (updateLastnameResult)
                                {
                                    user.LastName = newLastName;
                                    Console.WriteLine("Last Name updated successfully.");
                                }
                                else
                                {
                                    Console.WriteLine("Failed to update Last Name. Please try again.");
                                }
                                break;

                            case "3":
                                Console.Clear();
                                Console.Write("Enter new Role Name: ");
                                string newRoleName = Console.ReadLine();

                                var updateRoleResult = _userService.UpdateUser($"UPDATE roles SET RoleName = '{newRoleName}' FROM roles r INNER JOIN users u ON r.Id = u.RoleId WHERE u.Email = @Email", userMailUpdate);
                                if (updateRoleResult)
                                {
                                    user.RoleName = newRoleName;
                                    Console.WriteLine("Role Name updated successfully.");
                                }
                                else
                                {
                                    Console.WriteLine("Failed to update Role Name. Please try again.");
                                }
                                break;


                        }

                    }
                    else
                        Console.WriteLine("Something went wrong. Please try again.");

                    Console.ReadKey();
                    Console.Clear();
                    break;
                    
                    //--------------------------------------------------------------------------
                    //if (savedCustomer != null)
                    //{
                    //    Console.WriteLine($"1. Firstname: {savedCustomer.FirstName}");
                    //    Console.WriteLine($"2. LastName: {savedCustomer.LastName}");
                    //    Console.WriteLine($"3. Email: {savedCustomer.Email}");
                    //    Console.WriteLine($"4. Phonenumber: {savedCustomer.PhoneNumber}");
                    //    Console.Write("Select what to update (1-4): ");
                    //    string updateOption = Console.ReadLine();
                    //    Console.Clear();

                    //    switch (updateOption)
                    //    {
                    //        case "1":
                    //            Console.Write("Enter new First Name: ");
                    //            savedCustomer.FirstName = Console.ReadLine();
                    //            break;

                    //        case "2":
                    //            Console.Write("Enter new Last Name: ");
                    //            savedCustomer.LastName = Console.ReadLine();
                    //            break;

                    //        case "3":
                    //            Console.Write("Enter new Email: ");
                    //            savedCustomer.Email = Console.ReadLine();
                    //            break;

                    //        case "4":
                    //            Console.Write("Enter new Phone Number: ");
                    //            savedCustomer.PhoneNumber = Console.ReadLine();
                    //            break;
                    //    }

                    //    Console.Clear();
                    //    _customerService.UpdateCustomer(savedCustomer);
                    //    Console.WriteLine($"New data has been saved to customer: {savedCustomer.FirstName} {savedCustomer.LastName}");
                    //    Console.ReadKey();
                    //    Console.Clear();
                    //}
                    //else
                    //{
                    //    Console.Clear();
                    //    Console.WriteLine($"No customer with email {custEmail} exists in the database.");
                    //    Console.ReadKey();
                    //    Console.Clear();
                    //}
                    //break;

                case "5":
                    Console.Write("Email: ");
                    string email_delete = Console.ReadLine();
                    Console.Clear();

                    bool deleteResult = _userService.DeleteUser(email_delete);

                    if (deleteResult)
                        Console.WriteLine("User deleted.");
                    else
                        Console.WriteLine("Something went wrong. Please try again.");
                    
                    Console.ReadKey();
                    Console.Clear();

                    break;

                case "0":
                    Environment.Exit(0);
                    break;
            }
        }

    }
}
