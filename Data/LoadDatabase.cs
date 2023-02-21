using Microsoft.AspNetCore.Identity;
using NetKubernetes.Models;

namespace NetKubernetes.Data;

public class LoadDatabase {

    public static async Task InsertData(AppDbContext context, UserManager<UserApp>userManager)
    {
        if(!userManager.Users.Any())
        {
            var user = new UserApp {
                Name = "Oscar",
                LastName = "Vivcom",
                Email = "oscar.vivcom@outlook.com",
                UserName = "oscar.vivcom",
                PhoneNumber = "540316100656"
            };
            //await userManager.CreateAsync(user, "P4ssw0rdV1vc0m");
            var result = await userManager.CreateAsync(user, "VivCom2022$");
            if (!result.Succeeded)
            {
                Console.WriteLine($"result {result} {result.Errors.FirstOrDefault()}");
            }
        }

        if(!context.Inmuebles!.Any())
        {
            context.Inmuebles!.AddRange(
                new Inmueble {
                    Name = "Capital House",
                    Address = "Avenue Central",
                    Price = 3500M,
                    CreationDate = DateTime.Now
                },
                new Inmueble {
                    Name = "Small Ville",
                    Address = "Avenue 5",
                    Price = 3500M,
                    CreationDate = DateTime.Now
                }
            );
        }

        context.SaveChanges();
    }

}