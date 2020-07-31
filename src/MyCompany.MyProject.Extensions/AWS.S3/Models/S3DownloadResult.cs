namespace MyCompany.MyProject.Extensions.AwsS3.Models
{
    public class S3DownloadResult
    {
        public bool IsSuccess { get; set; }
        public string UniID { get; set; }
        public string ErrorMessage { get; set; }
        public string Base64 { get; set; }
    }
}
