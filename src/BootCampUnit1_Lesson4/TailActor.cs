using System;
using System.IO;
using System.Text;
using Akka.Actor;

namespace WinTail {

    public class TailActor : UntypedActor
    {
        private IActorRef _reportActor;
        private string _fileUri;
        private FileObserver _fileObserver;
        private StreamReader _streamReader;

        class InitialRead {
            public InitialRead(string text) {
                Text = text;
            }
            public string Text { get;}
        };
        class FileErrored {
            public FileErrored(string reason) {
                Reason = reason;
            }
            public string Reason { get;}
        };
        class FileChanged {};

        public TailActor(IActorRef reportActor, string fileUri)
        {
            _reportActor = reportActor;
            _fileUri = fileUri;

            _fileObserver = new FileObserver(fileUri);

            var self = Self;
            _fileObserver.OnError += e => {
                self.Tell(new FileErrored(e.Message));
            };
            _fileObserver.OnChanged += () => {
                self.Tell(new FileChanged());
            };
            _fileObserver.Start();

            var fileStream = new FileStream(fileUri, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            _streamReader = new StreamReader(fileStream, Encoding.UTF8);
            
            var initialText = _streamReader.ReadToEnd();
            Self.Tell(new InitialRead(initialText));
        }

        protected override void OnReceive(object message)
        {
            if(message is InitialRead)
            {
                _reportActor.Tell((message as InitialRead).Text);
            }
            else if(message is FileChanged)
            {
                var text = _streamReader.ReadToEnd();
                if(!string.IsNullOrEmpty(text))
                {
                    _reportActor.Tell(text);
                }
            }
            else if(message is FileErrored)
            {
                var reason = (message as FileErrored).Reason;
                _reportActor.Tell($"File Error : {reason}");
            }
            
        }
    }

}