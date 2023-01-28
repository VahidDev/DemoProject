namespace Project.Core.Utilities.Results
{
    public class Result
    {
        public bool Success { get; set; }
        public string Error { get; set; }
        public object Data { get; set; }
        public int StatusCode { get; set; }

        public Result()
        {
            Success = true;
        }
    }
}
