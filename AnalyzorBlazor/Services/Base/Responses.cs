namespace AnalyzerBlasor.Services.Base
{
    public class Responses<T>
    {
        public string Message { get; set; }
        public string ValidationError { get; set; }
        public bool Success { get; set; }
        public T Data { get; set; }
    }
}
