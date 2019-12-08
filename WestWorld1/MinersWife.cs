public class MinersWife : BaseGameEntity
{
    //an instance of the state machine class
    StateMachine<MinersWife> _stateMachine;

    LocationType _location;

    public bool Cooking { get; set; }

    public MinersWife(int id) : base(id)
    {
        _stateMachine = new StateMachine<MinersWife>(this)
        {
            CurrentState = DoHouseWork.Instance,
            GlobalState = WifesGlobalState.Instance
        };
    }

    public LocationType Location => _location;

    public void ChangeLocation(LocationType loc)
    {
        _location = loc;
    }

    public override bool HandleMessage(Telegram telegram)
    {
        return _stateMachine.HandleMessage(telegram);
    }

    public override void Update()
    {
        _stateMachine.Update();
    }

    public StateMachine<MinersWife> GetFsm()
    {
        return _stateMachine;
    }
}