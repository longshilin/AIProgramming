namespace WestWorld1
{
    class Program
    {
        public static void Main()
        {
            //create a miner
            Miner bob = new Miner(0);

            //create his wife
            MinersWife elsa = new MinersWife(1);

            // create Bar's Fly
            Fly fly = new Fly(2);


            //register them with the entity manager
            EntityManager.Instance.RegisterEntity(bob);
            EntityManager.Instance.RegisterEntity(elsa);
            EntityManager.Instance.RegisterEntity(fly);

            //run Bob and Elsa through a few Update calls
            for (int i = 0; i < 50; i++)
            {
                bob.Update();
                elsa.Update();
                fly.Update();
                MessageDispatcher.Instance.DispatchDelayedMessages();
                System.Threading.Thread.Sleep(500);
            }
        }
    }
}