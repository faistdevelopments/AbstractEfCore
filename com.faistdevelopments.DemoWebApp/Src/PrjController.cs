using Microsoft.AspNetCore.Mvc;

namespace com.faistdevelopments.DemoWebApp;

[ApiController]
public class PrjController : ControllerBase
{
    [HttpGet]
    [Route("GetUsers")]
    public List<User> GetUsers()
    {
        using (PrjDatabase db = new PrjDatabase())
        {
            List<User> users = com.faistdevelopments.DemoWebApp.User.FetchAll(db);
            return users;
        }
    }

    [HttpPost]
    [Route("AddDemoUser")]
    public void AddDemoUser()
    {
        using (PrjDatabase db = new PrjDatabase())
        {
            Hobby hobby = new Hobby("Motorbiking");
            hobby.Save(db); // When entity is created the first time

            User user = new User("Demo_" + DateTime.Now.ToShortTimeString());
            user.Hobbies.Add(hobby);
            user.Save(db); // When entity is created the first time

            db.SaveChanges(); // Commit the changes
        }
    }
}
