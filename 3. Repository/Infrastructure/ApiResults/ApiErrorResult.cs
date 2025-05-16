namespace Infrastructure.ApiResults
{
    public class ApiErrorResult : ApiResult
    {
        public string ErrorCode { get; set; }

        public string ErrorMessage { get; set; }

        public string TechnicalLog { get; internal set; }

        public override string ToString()
        {
            return string.Format(
                "ERROR: ErrorCode: {0} - ErrorMessage: {1} - Detail: {2}",
                ErrorCode,
                ErrorMessage,
                TechnicalLog
            );
        }
    }
}
