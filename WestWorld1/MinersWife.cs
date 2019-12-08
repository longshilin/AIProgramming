public class MinersWife : BaseGameEntity
{
    //an instance of the state machine class
    StateMachine<MinersWife> stateMachine;

    location_type location;

    public bool Cooking { get; set; }

    public MinersWife(int id) : base(id)
    {
        stateMachine = new StateMachine<MinersWife>(this)
        {
            CurrentState = DoHouseWork.Instance,
            GlobalState = WifesGlobalState.Instance
        };
    }

    public location_type Location => location;

    public void ChangeLocation(location_type loc)
    {
        location = loc;
    }

    public override bool HandleMessage(Telegram telegram)
    {
        return stateMachine.HandleMessage(telegram);
    }

    public override void Update()
    {
        stateMachine.Update();
    }

    public StateMachine<MinersWife> GetFSM()
    {
        return stateMachine;
    }
}