using System;
using Akka.Actor;

namespace WinTail
{
    internal class ConsoleWriterActor : UntypedActor
    {
        public ConsoleWriterActor()
        {
        }

        protected override void OnReceive(object message)
        {
            var stringMessage = message as string;
            if(stringMessage == null) return;
            
            var length = stringMessage.Length;
            if(length == 0)
            {
                Console.WriteLine("(Hey! Empty String!)");
            }
            else if(length % 2 == 0)
            {
                Console.WriteLine("[BLUE] {0}", stringMessage);
            }
            else
            {
                Console.WriteLine("[RED ] {0}", stringMessage);
            }
        }
    }
}