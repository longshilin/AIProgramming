public class Telegram
{
    public long Time { get; set; }
    public int SenderId { get; set; }
    public int ReceiverId { get; set; }
    public int MessageId { get; set; }
    
    public Telegram()
    {
        Time = -1;
        SenderId = -1;
        ReceiverId = -1;
        MessageId = -1;
    }

    public Telegram(long time, int senderId, int receiverId, int messageId)
    {
        this.Time = time;
        this.SenderId = senderId;
        this.ReceiverId = receiverId;
        this.MessageId = messageId;
    }
}