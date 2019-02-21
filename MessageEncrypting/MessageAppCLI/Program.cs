using MessageEncrypting.BusinessLogic;
using MessageEncrypting.DAL;
using MessageEncrypting.DAL.Interfaces;
using System;

namespace MessageAppCLI
{
    class Program
    {
        static void Main(string[] args)
        {

            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=MessageEncryptionDB;Integrated Security = True";

            var db = new UserDBService(connectionString);

            MessageApp ma = new MessageApp(db);
            MessageAppCLI messageCli = new MessageAppCLI(ma);
            messageCli.Run();
        }
    }
}
