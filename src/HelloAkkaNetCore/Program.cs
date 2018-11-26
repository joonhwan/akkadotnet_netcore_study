using System;
using System.Runtime.Loader;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;

namespace HelloAkkaNetCore
{
    class Program
    {
        static void Main(string[] args)
        {
            var system = ActorSystem.Create("test-actor-system");
            var actor = system.ActorOf(Props.Create<FirstActor>(), "first-actor");

            new AppRunContext()
            .WhenRun(() => actor.Tell("Test"))
            .WhenExit(() => {
                Console.WriteLine("Stopping actors...");
                actor.GracefulStop(TimeSpan.FromSeconds(5));
                Console.WriteLine("Terminating System...");
                system.Terminate().Wait();
            })
            .RunAndWait()
            ;
            actor.Tell("test");
        }
    }

    public class AppRunContext
    {
        private readonly AutoResetEvent _waitHandle = new AutoResetEvent(false);
        private Action _runAction;
        private Action _exitAction;
        
        public AppRunContext WhenRun(Action  runAction)
        {
            _runAction = runAction;
            return this;
        }

        public AppRunContext WhenExit(Action exitAction) 
        {
            _exitAction = exitAction;
            return this;
        }

        public void RunAndWait()
        {
            Task.Run(() => _runAction?.Invoke());

            Console.CancelKeyPress += (o, e) => {
                _exitAction?.Invoke();
                _waitHandle.Set();
            };
            AssemblyLoadContext.Default.Unloading += context => {
                Console.WriteLine("Unloading..");
                _exitAction?.Invoke();
                _waitHandle.Set();
            };

            _waitHandle.WaitOne();
        }
    }
}
