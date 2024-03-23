using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace com.faistdevelopments.AbstractEfCore;

public abstract class AbstractDatabase : DbContext
{
    protected string ConnectionString { get; set; }

    private List<Type> EntityTypes = new List<Type>();

    protected Assembly? AssemblyOfProject;

    public AbstractDatabase(string connectionString, Assembly assemblyOfProject)
    {
        this.ConnectionString = connectionString;
        this.AssemblyOfProject = assemblyOfProject;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        OnSpecificConfiguring(optionsBuilder);
    }

    protected abstract void OnSpecificConfiguring(DbContextOptionsBuilder optionsBuilder);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Register also the entities from the Lib
        this.RegisterEntites();

        foreach (Type type in this.EntityTypes)
        {
            modelBuilder.Entity(type);
        }

        // Dispose of EntityTypes to clear memory
        this.EntityTypes.Clear();
    }

    /// <summary>
    /// Needs to be called from constructor of project
    /// </summary>
    protected void RegisterEntites()
    {
        Assembly assemblyOfLib = Assembly.GetExecutingAssembly();

        PrepareEntityTypes(assemblyOfLib);
        if (AssemblyOfProject != null)
        {
            PrepareEntityTypes(AssemblyOfProject);
        }
    }

    private void PrepareEntityTypes(Assembly assembly)
    {
        foreach (Type type in assembly.GetTypes()
                                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(AbstractEntity))))
        {
            this.EntityTypes.Add(type);
        }
    }

    public new void Add<T>(T entity) where T : AbstractEntity
    {
        this.GetSet<T>(entity.GetType()).Add(entity);
    }

    public DbSet<T> GetSet<T>(Type type) where T : AbstractEntity
    {
        return base.Set<T>(type.ToString());
    }

    public override int SaveChanges()
    {
        int entitesWritten = base.SaveChanges();
        return entitesWritten;
    }
}