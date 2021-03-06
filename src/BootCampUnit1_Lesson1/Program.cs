﻿using System;
using Akka.Actor;

namespace WinTail
{
    class Program
    {
        static void Main(string[] args)
        {
            var system = ActorSystem.Create("wintail");

            var writerActor = system.ActorOf(Props.Create(() => new ConsoleWriterActor()));
            var readActor = system.ActorOf(Props.Create(() => new ConsoleReadActor(writerActor)));

            readActor.Tell("blabla");

            system.WhenTerminated.Wait();
        }
    }
}
