using System;
using Akka.Actor;

namespace WinTail
{
    class Program
    {
        static void Main(string[] args)
        {
            var system = ActorSystem.Create("wintail");

            var writeActorProps = Props.Create<ConsoleWriterActor>();
            var writeActor = system.ActorOf(writeActorProps, "writer");

            var validationActorProps = Props.Create<ValidationActor>(writeActor);
            var validationActor = system.ActorOf(validationActorProps, "validator");

            var readActorProps = Props.Create<ConsoleReadActor>(validationActor);
            var readActor = system.ActorOf(readActorProps, "reader");

            readActor.Tell(new Messages.ContinueProcessing());

            System.Console.WriteLine("WinTail Services started!");

            system.WhenTerminated.Wait();
        }
    }
}
