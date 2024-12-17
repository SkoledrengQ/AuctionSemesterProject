namespace WebApp.Models
{
    public class BidResult
    {
        public bool IsSuccessful { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public object? AdditionalInfo { get; set; }

        public static BidResult Success()
        {
            return new BidResult { IsSuccessful = true };
        }

        public static BidResult Failure(string errorMessage, object? additionalInfo = null)
        {
            return new BidResult
            {
                IsSuccessful = false,
                ErrorMessage = errorMessage,
                AdditionalInfo = additionalInfo
            };
        }
    }
}
