using System;
using System.Collections.Generic;
using System.Text;

namespace MessageEncrypting.Model.Database
{
    public class UserItem : BaseItem
    {
        
        public string UserName { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }
    }
}
