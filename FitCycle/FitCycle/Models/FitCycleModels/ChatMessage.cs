using System;
using System.Collections.Generic;
using System.Text;

namespace FitCycle.Models
{
    public class ChatMessage
    {
        public int ID { get; private set; }
        public string Message { get; private set; }
        public DateTime ReadTime { get; set; }
        public DateTime SendTime { get; private set; }
        public User User { get; private set; }
        public bool IsfromOtherUser { get; private set; }
        public bool IsfromCurrentUser { get; private set; }

        public ChatMessage(int id,string message,DateTime sendtime,bool isfromcurrentuser,User user)
        {
            ID = id;
            Message = message;
            SendTime = sendtime;
            IsfromOtherUser = !isfromcurrentuser;
            IsfromCurrentUser = isfromcurrentuser;
            User = user;
        }
    }
}
