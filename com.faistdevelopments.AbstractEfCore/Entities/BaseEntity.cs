using Microsoft.EntityFrameworkCore;

namespace com.faistdevelopments.AbstractEfCore;

public abstract class BaseEntity<T> : AbstractEntity where T : AbstractEntity
{
    public void Save(AbstractDatabase db)
    {
        db.Add<T>((this as T)!);
    }

    private static DbSet<T> GetSet(AbstractDatabase db)
    {
        return db.GetSet<T>(typeof(T));
    }

    public static List<T> FetchAll(AbstractDatabase db)
    {
        List<T> baseEntities = GetSet(db).ToList();
        return baseEntities!;
    }

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
}

