using MessageEncrypting.DAL.Interfaces;
using MessageEncrypting.Model.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessageEncrypting.BusinessLogic
{
    public class MessageApp
    {
        /// <summary>
        /// The business logic for the message app
        /// </summary>
        private IUserDBService _db = null;

        private User _currentUser = null;

        public MessageApp(IUserDBService db)
        {
            _db = db;
        }
        

        /// <summary>
        /// Adds a new user to the vending machine system
        /// </summary>
        /// <param name="user">Model that contains all the user information</param>
        public void RegisterUser(User user)
        {
            UserItem userItem = null;
            try
            {
                userItem = _db.GetUserItem(user.UserName);
            }
            catch (Exception)
            {
            }

            if (userItem != null)
            {
                throw new Exception("The username is already taken.");
            }

            if (user.Password != user.ConfirmPassword)
            {
                throw new Exception("The password and confirm password do not match.");
            }

            PasswordManager passHelper = new PasswordManager(user.Password);
            UserItem newUser = new UserItem()
            {
                UserName = user.UserName,
                Salt = passHelper.Salt,
                Hash = passHelper.Hash,
                
            };

            _db.AddUserItem(newUser);
            LoginUser(newUser.UserName, user.Password);
        }

        /// <summary>
        /// Logs a user into the vending machine system and throws exceptions on any failures
        /// </summary>
        /// <param name="username">The username of the user to authenicate</param>
        /// <param name="password">The password of the user to authenicate</param>
        public void LoginUser(string username, string password)
        {
            UserItem user = null;

            try
            {
                user = _db.GetUserItem(username);
                _currentUser.UserName = user.UserName;
                _currentUser.UserId = user.Id;
            }
            catch (Exception)
            {
                throw new Exception("Either the username or the password is invalid.");
            }

            PasswordManager passHelper = new PasswordManager(password, user.Salt);
            if (!passHelper.Verify(user.Hash))
            {
                throw new Exception("Either the username or the password is invalid.");
            }

           
        }

        /// <summary>
        /// Logs the current user out of the vending machine system
        /// </summary>
        public void LogoutUser()
        {
            _currentUser = null;
        }

    }
}
