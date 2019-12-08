namespace WestWorld1
{
    public class Fly : BaseGameEntity
    {
        StateMachine<Fly> _stateMachine;

        LocationType _location;
        
        public bool Bothering { get; set; }


        public Fly(int id) : base(id)
        {
            _stateMachine = new StateMachine<Fly>(this)
            {
                CurrentState = FlyIdle.Instance
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

        public StateMachine<Fly> GetFsm()
        {
            return _stateMachine;
        }
    }
}