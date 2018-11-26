
using System;
using Akka.Actor;

namespace HelloAkkaNetCore
{
    public class FirstActor : UntypedActor
    {
        protected override void PreStart() => System.Console.WriteLine("PreStart() is called");
        protected override void PostStop() => System.Console.WriteLine("PostStop() is called");
        
        protected override void OnReceive(object message)
        {
            Console.WriteLine($"Received message : {message}");
        }
    }
}