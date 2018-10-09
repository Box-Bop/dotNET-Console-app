using System;
using System.Collections.Generic;

namespace dotNET_Console_app
{
    public class JoinProcess : CompositeCommand
    {
        public JoinProcess()
        {
            Add(new CheckFromPoliceCommand());
            Add(new CreateUserCommand());
            Add(new SendNotificationCommand());
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var process = new JoinProcess();
            if (!process.Execute())
            {
                process.Rollback();
            }
            foreach (var error in process.Errors)
            {
                Console.WriteLine(error);
            }
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
        public virtual IList<string> Errors { get; } = new List<string>();
        public abstract bool Execute();
        public virtual bool Rollback()
        {
            return true;
        }
    }
    public class CreateUserCommand : CompositeCommand
    {
        public CreateUserCommand()
        {
            Add(new CreateDbUserCommand());
            Add(new CreateUserCommand());
        }
    }
    public class CreateDbUserCommand : Command
    {
        public override bool Execute()
        {
            Console.WriteLine("Creating user to database");
            return true;
        }
    }
    public class CreateAzureBloblContainerForUser : Command
    {
        public override bool Execute()
        {
            Console.WriteLine("");
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
