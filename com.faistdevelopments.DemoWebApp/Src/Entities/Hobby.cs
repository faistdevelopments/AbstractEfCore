using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using com.faistdevelopments.AbstractEfCore;

namespace com.faistdevelopments.DemoWebApp;

public class Hobby : BaseEntity<Hobby>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Name { get; set; }

    public Hobby() { }

    public Hobby(string name)
    {
        Name = name;
    }
}
