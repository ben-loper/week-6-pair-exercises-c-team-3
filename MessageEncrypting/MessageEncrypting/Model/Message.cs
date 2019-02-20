using System;
using System.Collections.Generic;
using System.Text;

namespace MessageEncrypting
{
    public class Message
    {
        public int MessageId { get; set;}
        public int ToUserName { get; set; }
        public int FromUserName { get; set; }
        public string UserMessage { get; set; }
    }
}
