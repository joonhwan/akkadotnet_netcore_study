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
    }
}
