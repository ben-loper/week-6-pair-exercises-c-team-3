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
<<<<<<< HEAD
            string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=MessageEncryptionDB;Integrated Security = True";

            var db = new UserDBService(connectionString);

            MessageApp ma = new MessageApp(db);
            MessageAppCLI messageCli = new MessageAppCLI(ma);
=======
            MessageAppCLI messageCli = new MessageAppCLI();
>>>>>>> 6cc0262a320885b10bed05bb22d874a06313b014

            messageCli.Run();
        }
    }
}
