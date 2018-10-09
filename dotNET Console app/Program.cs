using System;
using System.Collections.Generic;

namespace dotNET_Console_app
{
    class Program
    {
        public class JoinProcess : CommandHandler
        {
            public JoinProcess()
            {
                Add(new CheckFromPoliceCommand());
                Add(new CreateUserCommand());
                Add(new SendNotificationCommand());
            }
        }
        static void Main(string[] args)
        {
            var commandHandler = new CommandHandler();

            commandHandler.Add(new CheckFromPoliceCommand());
            commandHandler.Add(new CreateUserCommand());
            commandHandler.Add(new SendNotificationCommand());

            commandHandler.Run();

            Console.WriteLine("\r\nPress any key to continue ...");
            Console.ReadKey();

        }
    }

    public interface ICommand
    {
        IList<string> Errors { get; }
        bool Execute();
        bool Rollback();
    }
    public abstract class Command : ICommand
    {
        public IList<string> Errors { get; } = new List<string>();
        public abstract bool Execute();
        public virtual bool Rollback()
        {
            return true;
        }
    }
    public class CreateUserCommand : Command
    {
        public override bool Execute()
        {
            Console.WriteLine("Creating user account.");

            return true;
        }
        public override bool Rollback()
        {
            Console.WriteLine("Rollback user account.");
            Errors.Add("ROLLBACK: user account");
            return true;
        }
    }
    public class SendNotificationCommand : Command
    {
        public override bool Execute()
        {
            Console.WriteLine("Send notification called.");
            return false;
        }
        public override bool Rollback()
        {
            Console.WriteLine("Rollback notification");
            Errors.Add("ROLLBACK: notification");
            return true;
        }
    }
    public class CheckFromPoliceCommand : Command
    {
        public override bool Execute()
        {
            Console.WriteLine("Call the police API.");
            return true;
        }
        public override bool Rollback()
        {
            Console.WriteLine("Rollback police.");
            return true;
        }
    }
}
