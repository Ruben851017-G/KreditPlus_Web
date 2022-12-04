namespace KreditPlus_Web.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }

    public class ErrorBody
    {
        public object? message { get; set; }
        public object? errors { get; set; }
        public object? status_code { get; set; }
    }
}