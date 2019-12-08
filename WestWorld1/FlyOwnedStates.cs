using System;

namespace WestWorld1
{
    public class FlyIdle : State<Fly>
    {
        static readonly FlyIdle instance = new FlyIdle();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static FlyIdle()
        {
        }

        FlyIdle()
        {
        }

        //this is a singleton
        public static FlyIdle Instance => instance;

        Random _random = new Random();

        public override void Enter(Fly fly)
        {
        }

        public override void Execute(Fly fly)
        {
            switch (_random.Next(3))
            {
                case 0:
                    Console.WriteLine("Fly: Flying 1111");
                    break;
                case 1:
                    Console.WriteLine("Fly: Flying 2222");
                    break;
                case 2:
                    Console.WriteLine("Fly: Flying 3333");
                    break;
            }
        }

        public override void Exit(Fly fly)
        {
        }

        public override bool OnMessage(Fly fly, Telegram telegram)
        {
            switch (telegram.MessageId)
            {
                case 3:
                    Console.WriteLine("Fly: Hi honey miner. I finally waited for you");
                    fly.GetFsm().ChangeState(FlyAttack.Instance);
                    return true;
            }

            return false;
        }
    }

    public class FlyAttack : State<Fly>
    {
        static readonly FlyAttack instance = new FlyAttack();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static FlyAttack()
        {
        }

        FlyAttack()
        {
        }

        //this is a singleton
        public static FlyAttack Instance => instance;

        Random _random = new Random();

        public override void Enter(Fly fly)
        {
            if (!fly.Bothering)
            {
                Console.WriteLine("Fly: I'm going to bother the miners.");
                MessageDispatcher.Instance.DispatchMessage(0, fly.Id, 0, 4);
                fly.Bothering = true;
            }
        }

        public override void Execute(Fly fly)
        {
            Console.WriteLine("Fly: I'm bothering miner now");
        }

        public override void Exit(Fly fly)
        {
            if (fly.Bothering && _random.NextDouble() < 0.5)
            {
                Console.WriteLine("Fly: I don't want to bother miners anymore.");
                fly.Bothering = false;
                fly.GetFsm().ChangeState(FlyIdle.Instance);
            }
        }

        public override bool OnMessage(Fly fly, Telegram telegram)
        {
            switch (telegram.MessageId)
            {
                // Fly受到Miner的驱赶，有一半的几率离开，不再继续干扰Miner
                case 5:
                    if (fly.Bothering && _random.NextDouble() < 0.5)
                    {
                        Console.WriteLine("Fly: driving away by miner.");
                        fly.Bothering = false;
                        fly.GetFsm().ChangeState(FlyIdle.Instance);
                    }

                    return true;
                
                case 6:
                    if (fly.Bothering && _random.NextDouble() < 0.5)
                    {
                        Console.WriteLine("Fly: miner leave saloon. Fly going to idle state.");
                        fly.Bothering = false;
                        fly.GetFsm().ChangeState(FlyIdle.Instance);
                    }

                    return true;
            }

            return false;
        }
    }
}