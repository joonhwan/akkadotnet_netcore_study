using System;
using Akka.Actor;

namespace WinTail {
    public class TailCoordActor : UntypedActor
    {
        private IActorRef _reportActor;

        public TailCoordActor(IActorRef reportActor)
        {
            _reportActor = reportActor;
        }

        protected override void OnReceive(object message)
        {
            if(message is Messages.FileUriInput)
            {
                var fileUri = (message as Messages.FileUriInput).FileUri;
                Context.ActorOf(Props.Create<TailActor>(_reportActor, fileUri));
            }
        }
    }
}