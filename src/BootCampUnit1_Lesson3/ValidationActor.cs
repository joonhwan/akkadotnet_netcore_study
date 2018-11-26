using Akka.Actor;

namespace WinTail
{
    internal class ValidationActor : UntypedActor
    {
        private IActorRef _writeActor;

        public ValidationActor(IActorRef writeActor)
        {
            _writeActor = writeActor;
        }

        protected override void OnReceive(object message)
        {
            if (message is Messages.ConsoleTextInput)
            {
                var input = (message as Messages.ConsoleTextInput).Input;

                var messageToWriter = CreateMessageToSend(input);
                _writeActor.Tell(messageToWriter);

                Context.Sender.Tell(new Messages.ContinueProcessing());
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