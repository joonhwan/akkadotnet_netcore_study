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

            var tailCoordActorProps = Props.Create<TailCoordActor>(writeActor);
            var tailCoordActor = system.ActorOf(tailCoordActorProps, "tail-coordinator");

            var fileValidationActorProps = Props.Create<FileValidationActor>(writeActor, tailCoordActor);
            var fileValidationActor = system.ActorOf(fileValidationActorProps, "validator");

            var readActorProps = Props.Create<ConsoleReadActor>(fileValidationActor);
            var readActor = system.ActorOf(readActorProps, "reader");

            readActor.Tell(new Messages.ContinueProcessing());

            Console.WriteLine("WinTail Services started!");
            Console.WriteLine("Enter path like './SampleLogFile.Log' or 'exit'");

            system.WhenTerminated.Wait();
        }
    }
}
