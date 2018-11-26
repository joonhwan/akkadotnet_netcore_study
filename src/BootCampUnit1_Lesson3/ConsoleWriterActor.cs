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
            if(message is Messages.OddTextInput)
            {
                Console.WriteLine("[ODD ] {0}", (message as Messages.OddTextInput).Text);
            }
            else if(message is Messages.EvenTextInput)
            {
                Console.WriteLine("[EVEN] {0}", (message as Messages.EvenTextInput).Text);
            }
            else if(message is Messages.NullTextInput)
            {
                System.Console.WriteLine("[NULL] You input Null String!");
            }
            else if(message is Messages.EmptyTextInput)
            {
                System.Console.WriteLine("[EMPT] You input Empty String!");
            }
        }
    }
}