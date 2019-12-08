using System.Collections.Generic;

class EntityManager
{
    Dictionary<int, BaseGameEntity> _entityMap = new Dictionary<int, BaseGameEntity>();

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
        _entityMap.Add(newEntity.Id, newEntity);
    }

    public BaseGameEntity GetEntityFromId(int id)
    {
        return _entityMap[id];
    }

    public void RemoveEntity(BaseGameEntity entity)
    {
        _entityMap.Remove(entity.Id);
    }
}