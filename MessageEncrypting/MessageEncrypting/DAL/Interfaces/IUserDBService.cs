using MessageEncrypting.Model.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessageEncrypting.DAL.Interfaces
{
    public interface IUserDBService
    {       
        int AddUserItem(UserItem item);
        UserItem GetUserItem(int userId);
        void DeleteUserItem(int userId);
        List<UserItem> GetUserItems();
        UserItem GetUserItem(string username);
    }
}
