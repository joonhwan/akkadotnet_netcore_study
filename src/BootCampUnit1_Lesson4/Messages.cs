namespace WinTail
{
    public class Messages
    {
        public class ContinueProcessing {}
        public class StopProcessing {}

        public class OddTextInput 
        {
            public OddTextInput(string text)
            {
                Text = text;
            }

            public string Text { get; }
        }
        
        public class EvenTextInput
        {
            public EvenTextInput(string text)
            {
                Text = text;
            }

            public string Text { get; }
        }

        public class NullTextInput {}

        public class EmptyTextInput
        {
            public EmptyTextInput()
            {
            }
        }

        internal class ConsoleTextInput
        {
            public ConsoleTextInput(string input)
            {
                Input = input;
            }

            public string Input { get; }
        }

        internal class FileUriInput
        {
            public FileUriInput(string fileUri)
            {
                FileUri = fileUri;
            }

            public string FileUri { get; }
        }

        internal class ErrorTextInput
        {
            public ErrorTextInput(string reason)
            {
                Reason = reason;
            }

            public string Reason { get; }
        }
    }
}
