//A class defining a goldminer.
public class Miner : BaseGameEntity
{
    //the amount of gold a miner must have before he feels comfortable
    public const int ComfortLevel = 5;

    //the amount of nuggets a miner can carry
    public const int MaxNuggets = 3;

    //above this value a miner is thirsty
    public const int ThirstLevel = 5;

    //above this value a miner is sleepy
    public const int TirednessThreshold = 5;

    //an instance of the state machine class
    StateMachine<Miner> _stateMachine;

    LocationType _location;

    public LocationType Location => _location;

    public void ChangeLocation(LocationType loc)
    {
        _location = loc;
    }

    //how many nuggets the miner has in his pockets
    int _goldCarried;

    int _moneyInBank;

    //the higher the value, the thirstier the miner
    int _thirst;

    //the higher the value, the more tired the miner
    int _fatigue;

    public Miner(int id) : base(id)
    {
        _stateMachine = new StateMachine<Miner>(this);
        _stateMachine.CurrentState = GoHomeAndSleepTilRested.Instance;
    }

    public int GoldCarried
    {
        get => _goldCarried;
        set => _goldCarried = value;
    }

    public void AddToGoldCarried(int val)
    {
        _goldCarried += val;
        if (_goldCarried < 0) _goldCarried = 0;
    }

    public bool PocketsFull()
    {
        return _goldCarried >= MaxNuggets;
    }

    public void AddToWealth(int val)
    {
        _moneyInBank += val;
        if (_moneyInBank < 0) _moneyInBank = 0;
    }

    public int Wealth
    {
        get => _moneyInBank;
        set => _moneyInBank = value;
    }

    public bool Thirsty()
    {
        if (_thirst >= ThirstLevel)
        {
            return true;
        }

        return false;
    }

    public void BuyAndDrinkAWhiskey()
    {
        _thirst = 0;
        _moneyInBank -= 2;
    }

    public override bool HandleMessage(Telegram telegram)
    {
        return _stateMachine.HandleMessage(telegram);
    }

    //this must be implemented
    public override void Update()
    {
        _thirst += 1;

        _stateMachine.Update();
    }

    public StateMachine<Miner> GetFsm()
    {
        return _stateMachine;
    }

    public bool Fatigued()
    {
        if (_fatigue > TirednessThreshold)
        {
            return true;
        }

        return false;
    }

    public void DecreaseFatigue()
    {
        _fatigue -= 1;
    }

    public void IncreaseFatigue()
    {
        _fatigue += 1;
    }
}