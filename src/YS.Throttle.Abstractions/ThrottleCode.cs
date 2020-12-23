namespace YS.Throttle
{
    public sealed class ThrottleCode
    {
        public string FunctionCode { get; set; } = "*";

        public string ContextKind { get; set; }
        
        public string ContextValue { get; set; }

        public override string ToString()
        {
            return $"{ContextKind}:{ContextValue}@{FunctionCode}";
        }
    }
}
