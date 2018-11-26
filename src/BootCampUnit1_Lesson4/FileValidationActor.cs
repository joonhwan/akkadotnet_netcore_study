using System;
using System.IO;
using Akka.Actor;

namespace WinTail
{
    internal class FileValidationActor : UntypedActor
    {
        private IActorRef _writeActor;
        private IActorRef _tailCoordActor;

        public FileValidationActor(IActorRef writeActor, IActorRef tailCoordActor)
        {
            _writeActor = writeActor;
            _tailCoordActor = tailCoordActor;
        }

        protected override void OnReceive(object message)
        {
            if (message is Messages.ConsoleTextInput)
            {
                var input = (message as Messages.ConsoleTextInput).Input;

                var messageToSend = CreateMessageToSend(input);
                
                if(messageToSend is Messages.FileUriInput)
                {
                    _tailCoordActor.Tell(messageToSend);
                }
                else
                {
                    _writeActor.Tell(messageToSend);
                }

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
            else if (IsFileUri(input))
            {
                return new Messages.FileUriInput(input);
            }
            else
            {
                return new Messages.ErrorTextInput($"{input} is not File Uri!");
            }
        }

        private bool IsFileUri(string input)
        {
            return File.Exists(input);
        }
    }
}