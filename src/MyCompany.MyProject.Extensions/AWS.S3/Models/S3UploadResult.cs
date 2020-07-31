namespace MyCompany.MyProject.Extensions.AwsS3.Models
{
    public class S3UploadResult
    {
        public bool IsSuccess { get; set; }
        public string UniID { get; set; }
        public string Size { get; set; }
        public string ErrorMessage { get; set; }
    }
}
