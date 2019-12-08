using System;

class WifesGlobalState : State<MinersWife>
{
    static readonly WifesGlobalState instance = new WifesGlobalState();

    // Explicit static constructor to tell C# compiler
    // not to mark type as beforefieldinit
    static WifesGlobalState() { }

    WifesGlobalState() { }

    //this is a singleton
    public static WifesGlobalState Instance => instance;

    Random _random = new Random();

    public override void Enter(MinersWife wife)
    {
    }

    public override void Execute(MinersWife wife)
    {
        if (_random.NextDouble() < 0.1)
        {
            wife.GetFsm().ChangeState(VisitBathroom.Instance);
        }
    }

    public override void Exit(MinersWife wife)
    {
    }

    public override bool OnMessage(MinersWife wife, Telegram telegram)
    {
        switch (telegram.MessageId)
        {
            case 0:
                Console.WriteLine("Elsa: Hi honey. Let me make you some of mah fine country stew");
                wife.GetFsm().ChangeState(CookStew.Instance);
                return true;
        }
        return false;
    }
}

class DoHouseWork : State<MinersWife>
{
    static readonly DoHouseWork instance = new DoHouseWork();

    // Explicit static constructor to tell C# compiler
    // not to mark type as beforefieldinit
    static DoHouseWork() { }

    DoHouseWork() { }

    //this is a singleton
    public static DoHouseWork Instance => instance;

    Random _random = new Random();

    public override void Enter(MinersWife wife)
    {
    }

    public override void Execute(MinersWife wife)
    {
        switch (_random.Next(3))
        {
            case 0:
                Console.WriteLine("Elsa: Moppin' the floor");
                break;
            case 1:
                Console.WriteLine("Elsa: Washin' the dishes");
                break;
            case 2:
                Console.WriteLine("Elsa: Makin' the bed");
                break;
        }
    }

    public override void Exit(MinersWife wife)
    {
    }

    public override bool OnMessage(MinersWife wife, Telegram telegram)
    {
        return false;
    }
}   

class VisitBathroom : State<MinersWife>
{
    static readonly VisitBathroom instance = new VisitBathroom();

    // Explicit static constructor to tell C# compiler
    // not to mark type as beforefieldinit
    static VisitBathroom() { }

    VisitBathroom() { }

    //this is a singleton
    public static VisitBathroom Instance => instance;

    public override void Enter(MinersWife wife)
    {
        Console.WriteLine("Elsa: Walking to the can. Need to powda mah pretty li'lle nose");
    }

    public override void Execute(MinersWife wife)
    {
        Console.WriteLine("Elsa: Ahhhhhh! Sweet relief!");
        wife.GetFsm().RevertToPreviousState();
    }

    public override void Exit(MinersWife wife)
    {
        Console.WriteLine("Elsa: Leaving the john");
    }

    public override bool OnMessage(MinersWife wife, Telegram telegram)
    {
        return false;
    }
}

class CookStew : State<MinersWife>
{
    static readonly CookStew instance = new CookStew();

    // Explicit static constructor to tell C# compiler
    // not to mark type as beforefieldinit
    static CookStew() { }

    CookStew() { }

    //this is a singleton
    public static CookStew Instance => instance;

    public override void Enter(MinersWife wife)
    {
        if (!wife.Cooking)
        {
            Console.WriteLine("Elsa: Putting the stew in the oven");
            MessageDispatcher.Instance.DispatchMessage(1500000, wife.Id, 1, 1);
            wife.Cooking = true;
        }
    }

    public override void Execute(MinersWife wife)
    {
        Console.WriteLine("Elsa: Fussin' over food");
    }

    public override void Exit(MinersWife wife)
    {
        Console.WriteLine("Elsa: Puttin' the stew on the table");
    }

    public override bool OnMessage(MinersWife wife, Telegram telegram)
    {
        switch (telegram.MessageId)
        {
            case 1:
                Console.WriteLine("Elsa: Stew ready! Let's eat");
                MessageDispatcher.Instance.DispatchMessage(0, wife.Id, 0, 1);
                wife.Cooking = false;
                wife.GetFsm().ChangeState(DoHouseWork.Instance);
                return true;
        }
        return false;
    }
}