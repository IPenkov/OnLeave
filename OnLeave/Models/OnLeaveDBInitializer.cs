
namespace OnLeave.Models
{
    #region [Using Directives]

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity;

    #endregion

    public class UserDBInitializer : DropCreateDatabaseAlways<UserDbContext>
      {
          protected override void Seed(UserDbContext context)
          {
              var UserManager = new UserManager<User>(new UserStore<User>(context));
              var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
              
              string name = "Admin";
              string password = "p@ssword";
              string test = "test";

              //Create Role Test and User Test
              RoleManager.Create(new IdentityRole(test));
              UserManager.Create(new User()
              {
                  UserName = test,
                  FirstName = test,
                  LastName = test
              });

              //Create Role Admin if it does not exist
              if (!RoleManager.RoleExists(name))
              {
                  var roleresult = RoleManager.Create(new IdentityRole(name));
              }

              //Create User=Admin with password=123456
              var user = new User();
              user.UserName = name;
              user.FirstName = name;
              user.LastName = name;
              
              var adminresult = UserManager.Create(user, password);

              //Add User Admin to Role Admin
              if (adminresult.Succeeded)
              {
                  var result = UserManager.AddToRole(user.Id, name);
              }
          }
      }
}