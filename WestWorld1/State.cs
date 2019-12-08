//abstract base class to define an interface for a state
abstract public class State<TEntityType>
{
    //this will execute when the state is entered
    abstract public void Enter(TEntityType entity);

    //this is the state's normal update function
    abstract public void Execute(TEntityType entity);

    //this will execute when the state is exited
    abstract public void Exit(TEntityType entity);

    abstract public bool OnMessage(TEntityType entity, Telegram telegram);
}