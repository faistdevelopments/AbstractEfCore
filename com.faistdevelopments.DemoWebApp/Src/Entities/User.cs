using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using com.faistdevelopments.AbstractEfCore;

namespace com.faistdevelopments.DemoWebApp;

public class User : BaseEntity<User>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Username { get; set; }

    public virtual List<Hobby> Hobbies { get; set; } = new List<Hobby>();

    public User() { }

    public User(string username)
    {
        Username = username;
    }
}
