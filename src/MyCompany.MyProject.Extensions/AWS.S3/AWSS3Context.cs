using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using MyCompany.MyProject.Extensions.AwsS3.Models;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace MyCompany.MyProject.Extensions.AwsS3
{
    public partial class AWSS3Context : IDisposable
    {
        private static readonly object LockObject = new object();
        private readonly string _bucketName;
        private readonly IAmazonS3 client;
        static readonly string[] SizeSuffixes =
                  { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

        public AWSS3Context(string keyId, string keySecret, string bucketName, EnumAWSRegion region)
        {
            _bucketName = bucketName;
            client = new AmazonS3Client(keyId, keySecret, SetRegion(region));
        }

        public async Task<S3DownloadResult> GetDataAsync(string keyName)
        {
            S3DownloadResult result = new S3DownloadResult() { UniID = keyName, IsSuccess = true };
            try
            {
                GetObjectRequest request = new GetObjectRequest
                {
                    BucketName = _bucketName,
                    Key = keyName
                };
                using (GetObjectResponse response = await client.GetObjectAsync(request))
                using (Stream responseStream = response.ResponseStream)
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    responseStream.CopyTo(memoryStream);
                    byte[] bytes = memoryStream.ToArray();
                    result.Base64 = Convert.ToBase64String(bytes, 0, bytes.Length);
                    //Encoding.UTF8.GetString(bytes);
                    if (string.IsNullOrWhiteSpace(result.Base64))
                    {
                        result.IsSuccess = false;
                        result.ErrorMessage = "empty base64";
                    }
                }
            }
            catch (AmazonS3Exception e)
            {
                result.IsSuccess = false;
                result.ErrorMessage += e.Message;
            }
            catch (Exception e)
            {
                result.IsSuccess = false;
                result.ErrorMessage += e.Message;
            }
            return result;
        }
        public async Task<S3UploadResult> PostDataAsync(string base64)
        {
            PutObjectResponse response = null;
            S3UploadResult result = new S3UploadResult()
            {
                IsSuccess = true
            };
            lock (LockObject)
            {
                result.UniID = (DateTime.Now.Ticks - new DateTime(2016, 1, 1).Ticks).ToString("x");
            }

            if (string.IsNullOrWhiteSpace(base64))
            {
                result.IsSuccess = false;
                result.ErrorMessage += "Base 64 is empty";
                return result;
            }
            try
            {
                byte[] base64EncodedBytes = Convert.FromBase64String(base64);
                result.Size = SizeSuffix(base64EncodedBytes.Length);

                PutObjectRequest request = new PutObjectRequest
                {
                    BucketName = _bucketName,
                    CannedACL = S3CannedACL.BucketOwnerFullControl,
                    Key = result.UniID,
                };
                using (var ms = new MemoryStream(base64EncodedBytes))
                {
                    request.InputStream = ms;
                    response = await client.PutObjectAsync(request);
                }
                if (response.HttpStatusCode != HttpStatusCode.OK)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = response.HttpStatusCode.ToString();
                }
            }
            catch (AmazonS3Exception e)
            {
                result.IsSuccess = false;
                result.ErrorMessage += e.Message;
            }
            catch (Exception e)
            {
                result.IsSuccess = false;
                result.ErrorMessage += e.Message;
            }
            return result;
        }

        private string SizeSuffix(long value)
        {
            int i = 0;
            decimal dValue = value;
            while (Math.Round(dValue, 1) >= 1000)
            {
                dValue /= 1024;
                i++;
            }

            return string.Format("{0} {1}", dValue.ToString("00.00"), SizeSuffixes[i]);
        }

        private RegionEndpoint SetRegion(EnumAWSRegion region)
        {
            switch (region)
            {
                case EnumAWSRegion.HongKong:
                    return RegionEndpoint.APEast1;
                case EnumAWSRegion.Singapore:
                    return RegionEndpoint.APSoutheast1;
                default:
                    return RegionEndpoint.APSoutheast1;
            }
        }
        public void Dispose()
        {
            client.Dispose();
        }
    }

}
