namespace XLFezEditor.Files
{
    public class ContextGroup
    {

        public string source { get; set; }
        public string lineNumber { get; set; }

        public ContextGroup(string source, string lineNumber)
        {
            this.source = source;
            this.lineNumber = lineNumber;
        }
    }
}