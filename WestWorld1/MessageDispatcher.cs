using System;
using System.Collections.Generic;
using System.Diagnostics;

class MessageDispatcher
{
    List<Telegram> _queue = new List<Telegram>();

    void Discharge(BaseGameEntity receiver, Telegram telegram)
    {
        if (!receiver.HandleMessage(telegram))
        {
            Console.WriteLine("Message not handled");
        }
    }

    static readonly MessageDispatcher instance = new MessageDispatcher();

    // Explicit static constructor to tell C# compiler
    // not to mark type as beforefieldinit
    static MessageDispatcher() { }

    MessageDispatcher() { }

    //this is a singleton
    public static MessageDispatcher Instance => instance;

    public void DispatchMessage(long time, int senderId, int receiverId, int messageId)
    {
        BaseGameEntity receiver = EntityManager.Instance.GetEntityFromId(receiverId);
        Telegram telegram = new Telegram(time, senderId, receiverId, messageId);
        if (time <= 0.0)
        {
            Discharge(receiver, telegram);
        }
        else
        {
            long currentTime = Stopwatch.GetTimestamp();
            telegram.Time = currentTime + time;
            _queue.Add(telegram);
            _queue.Sort(CompareByTime);
        }
    }

    static int CompareByTime(Telegram a, Telegram b)
    {
        return a.Time.CompareTo(b.Time);
    }

    public void DispatchDelayedMessages()
    {
        long currentTime = Stopwatch.GetTimestamp();
        
        while (_queue.Count > 0 && _queue[0].Time < currentTime && _queue[0].Time > 0)
        {
            Telegram telegram = _queue[0];
            BaseGameEntity receiver = EntityManager.Instance.GetEntityFromId(telegram.ReceiverId);
            Discharge(receiver, telegram);
            _queue.Remove(_queue[0]);
        }
    }
}