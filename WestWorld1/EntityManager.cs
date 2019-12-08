using System.Collections.Generic;

class EntityManager
{
    Dictionary<int, BaseGameEntity> entityMap = new Dictionary<int, BaseGameEntity>();

    static readonly EntityManager instance = new EntityManager();

    // Explicit static constructor to tell C# compiler
    // not to mark type as beforefieldinit
    static EntityManager()
    {
    }

    EntityManager()
    {
    }

    //this is a singleton
    public static EntityManager Instance => instance;

    public void RegisterEntity(BaseGameEntity newEntity)
    {
        entityMap.Add(newEntity.ID, newEntity);
    }

    public BaseGameEntity GetEntityFromID(int id)
    {
        return entityMap[id];
    }

    public void RemoveEntity(BaseGameEntity entity)
    {
        entityMap.Remove(entity.ID);
    }
}