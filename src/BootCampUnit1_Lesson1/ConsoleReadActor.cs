using System;
using Akka.Actor;

namespace WinTail
{
    internal class ConsoleReadActor : UntypedActor
    {
        private IActorRef _writerActor;
        const string ExitCommand = "exit";

        public ConsoleReadActor(IActorRef writerActor)
        {
            _writerActor = writerActor;
        }

        protected override void OnReceive(object message)
        {
            var input = Console.ReadLine();
            if(!string.IsNullOrEmpty(input) && string.Equals(input, ExitCommand, StringComparison.InvariantCultureIgnoreCase))
            {
                Context.System.Terminate();
                return;
            }
            _writerActor.Tell(input);

            Context.Self.Tell("blabla");
        }
    }
}