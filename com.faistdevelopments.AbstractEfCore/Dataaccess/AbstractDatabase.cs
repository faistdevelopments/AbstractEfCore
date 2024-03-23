using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace com.faistdevelopments.AbstractEfCore;

/// <summary>
/// Base class for the Database Logic
/// </summary>
public abstract class AbstractDatabase : DbContext
{
    /// <summary>
    /// The connection string to the database
    /// </summary>
    protected string ConnectionString { get; set; }

    /// <summary>
    /// All classes which are an Entity
    /// </summary>
    protected List<Type> EntityTypes = new List<Type>();

    /// <summary>
    /// The Assembly of the executing project using this lib
    /// </summary>
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

    /// <summary>
    /// Special configuration to be done in here like database specifics
    /// </summary>
    /// <param name="optionsBuilder"></param>
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
    /// Loading all entity types into the context
    /// </summary>
    protected virtual void RegisterEntites()
    {
        Assembly assemblyOfLib = Assembly.GetExecutingAssembly();

        PrepareEntityTypes(assemblyOfLib);
        if (AssemblyOfProject != null)
        {
            PrepareEntityTypes(AssemblyOfProject);
        }
    }

    /// <summary>
    /// Preparing all the entity types for loading into the context
    /// </summary>
    /// <param name="assembly"></param>
    protected virtual void PrepareEntityTypes(Assembly assembly)
    {
        foreach (Type type in assembly.GetTypes()
                                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(AbstractEntity))))
        {
            this.EntityTypes.Add(type);
        }
    }

    /// <summary>
    /// Add a new entity to the context
    /// </summary>
    /// <typeparam name="T">Type of the entity</typeparam>
    /// <param name="entity">The object of the entity</param>
    public new void Add<T>(T entity) where T : AbstractEntity
    {
        this.GetSet<T>(entity.GetType()).Add(entity);
    }

    /// <summary>
    /// Get the Set from the context based on the entity type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="type"></param>
    /// <returns></returns>
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