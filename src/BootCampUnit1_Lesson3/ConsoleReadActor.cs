using System;
using Akka.Actor;

namespace WinTail
{
    internal class ConsoleReadActor : UntypedActor
    {
        private IActorRef _validationActor;
        const string ExitCommand = "exit";

        public ConsoleReadActor(IActorRef validationActor)
        {
            _validationActor = validationActor;
        }

        protected override void OnReceive(object message)
        {
            if (message is Messages.ContinueProcessing)
            {
                var input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input) && string.Equals(input, ExitCommand, StringComparison.InvariantCultureIgnoreCase))
                {
                    Context.Self.Tell(new Messages.StopProcessing());
                    return;
                }

                _validationActor.Tell(new Messages.ConsoleTextInput(input));
            }
            else if(message is Messages.StopProcessing)
            {
                Context.System.Terminate();
            }
        }

        private object CreateMessageToSend(string input)
        {
            if (input == null)
            {
                return new Messages.NullTextInput();
            }
            else if (input.Length == 0)
            {
                return new Messages.EmptyTextInput();
            }
            else if (input.Length % 2 == 0)
            {
                return new Messages.EvenTextInput(input);
            }
            else
            {
                return new Messages.OddTextInput(input);
            }
        }
    }
}