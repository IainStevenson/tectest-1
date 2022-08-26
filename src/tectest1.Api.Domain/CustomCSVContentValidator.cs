namespace tectest1.Api.Domain
{
    public class CustomCSVContentValidator : IRawContentValidator
    {
        const int HT = 9;
        const int DEL = 127;
        const int EXPECTEDDELIMITERSPERLINE = 3;
        const int DELIMITER = 44;

        public bool Validate(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return false;
            }
            var chars = content.ToCharArray();
            if (ContentHasUnsuitableChars(chars)) return false;
            if (ContentLacksEnoughDelimiters(chars)) return false;
            return true;
        }

        private bool ContentHasUnsuitableChars(char[] content)
        {
            return content.Max() > DEL || content.Min() < HT; ;
        }

        private bool ContentLacksEnoughDelimiters(char[] content)
        {
            var delimiters = content.Count(x => x == DELIMITER);
            var lines = content.Count(c => c == '\n');
            return delimiters < EXPECTEDDELIMITERSPERLINE * lines;
        }
    }
}