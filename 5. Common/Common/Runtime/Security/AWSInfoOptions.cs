namespace Common.Runtime.Security
{
    public class AWSInfoOptions
    {
        public string AccessKey { get; set; }

        public string SecretKey { get; set; }

        public string Region { get; set; }

        public string UserPoolID { get; set; }

        public string ClientAppID { get; set; }

        public string S3BucketName { get; set; }

        public string S3UserImagePath { get; set; }

        public string VPNPublicIP1 { get; set; }

        public string VPNPublicIP2 { get; set; }

        public int TokenExpirationInHours { get; set; }
    }
}
