using Microsoft.EntityFrameworkCore;

namespace com.faistdevelopments.AbstractEfCore;

/// <summary>
/// Typed base class for entities
/// </summary>
/// <typeparam name="T">The type of entity</typeparam>
public abstract class BaseEntity<T> : AbstractEntity where T : AbstractEntity
{
    /// <summary>
    /// Add the entity to the context. Required if the entity is newly created.
    /// </summary>
    /// <param name="db"></param>
    public void Save(AbstractDatabase db)
    {
        db.Add<T>((this as T)!);
    }

    /// <summary>
    /// Gets the Set from the database context
    /// </summary>
    /// <param name="db"></param>
    /// <returns></returns>
    protected static DbSet<T> GetSet(AbstractDatabase db)
    {
        return db.GetSet<T>(typeof(T));
    }

    /// <summary>
    /// Fetch all entities of this type
    /// </summary>
    /// <param name="db"></param>
    /// <returns></returns>
    public static List<T> FetchAll(AbstractDatabase db)
    {
        List<T> baseEntities = GetSet(db).ToList();
        return baseEntities!;
    }

    /// <summary>
    /// Fetch the entity matching the primary keys
    /// </summary>
    /// <param name="db"></param>
    /// <param name="pks">Multiple PKs possible</param>
    /// <returns></returns>
    public static T? FetchByPrimaryKey(AbstractDatabase db, params object[] pks)
    {
        DbSet<T> set = GetSet(db);
        AbstractEntity? baseEntity = set.Find(pks);

        if (baseEntity == null)
        {
            return null;
        }

        return baseEntity as T;
    }

    /// <summary>
    /// Fetch entities by specific linq condition
    /// </summary>
    /// <param name="db"></param>
    /// <param name="linq"></param>
    /// <returns></returns>
    public static List<T> FetchByLinq(AbstractDatabase db, Func<T, bool> linq)
    {
        DbSet<T> set = GetSet(db);
        return set.Where(linq).ToList();
    }
}

