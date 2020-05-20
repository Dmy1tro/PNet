namespace PNet_Lab_2.Models
{
    public class ExecuteResult
    {
        public static ExecuteResult OkResult()
        {
            return new ExecuteResult(true, string.Empty);
        }

        public static ExecuteResult ErrorResult(string error)
        {
            return new ExecuteResult(false, error);
        }

        private ExecuteResult(bool isValid, string error)
        {
            IsValid = isValid;
            Error = error;
        }

        public bool IsValid { get; }

        public string Error { get; }

    }
}
