using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;
using System.IO;

using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using Amazon.SimpleDB;
using Amazon.SimpleDB.Model;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;


namespace MyCloudClassroom.Classes
{
    public class AWSClass
    {
        protected IAmazonEC2 ec2;
        protected IAmazonS3 s3;
        protected IAmazonSimpleDB sdb;
        static IAmazonS3 clients3;
        static AmazonSimpleNotificationServiceClient clientsns;
        


        public AWSClass()
        {
            if (clients3 == null)
                clients3 = new AmazonS3Client(Amazon.RegionEndpoint.USWest2);
            if(clientsns == null)
                clientsns = new AmazonSimpleNotificationServiceClient();
        }

        
            //string filePath = "C:\\Users\\Priyank\\Documents\\IISExpress\\input\\" + FileUpload1.FileName;

            //FileUpload1.SaveAs(filePath);
            
            //string bucketName = "cloud272";
            //string keyName = Path.GetFileName(FileUpload1.FileName);
            
                        
            //string urlString;

            //S3FileUpload.WritingAnObject(bucketName, keyName, filePath,ContentType);

            //ReadObjectData(bucketName, keyName);
            
        //}

        public static void UploadToS3(string _bucketname, string _keyname, string _FilePath, 
                                string _ContentType, string _title)
        {
            if (clients3 == null)
                clients3 = new AmazonS3Client(Amazon.RegionEndpoint.USWest2);
            try
            {
                // 2. Put object-set ContentType and add metadata.
                PutObjectRequest putRequest2 = new PutObjectRequest
                {
                    BucketName = _bucketname,
                    Key = _keyname,
                    FilePath = _FilePath,
                    ContentType = _ContentType,
                    CannedACL = S3CannedACL.PublicRead
                };
                putRequest2.Metadata.Add("x-amz-meta-title", _title);

                PutObjectResponse response2 = clients3.PutObject(putRequest2);

            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null &&
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId")
                    ||
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    Console.WriteLine("Check the provided AWS Credentials.");
                    Console.WriteLine(
                        "For service sign up go to http://aws.amazon.com/s3");
                }
                else
                {
                    Console.WriteLine(
                        "Error occurred. Message:'{0}' when writing an object"
                        , amazonS3Exception.Message);
                }
            }
        }

        public static void PutBucketToS3(string _bucketname)
        {
            if (clients3 == null)
                clients3 = new AmazonS3Client(Amazon.RegionEndpoint.USWest2);
            try
            {
                PutBucketRequest request = new PutBucketRequest()
                {
                    BucketName = _bucketname,
                    BucketRegion = S3Region.USW2,
                    CannedACL = S3CannedACL.PublicRead
                };
                PutBucketResponse response = clients3.PutBucket(request);

            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null &&
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId")
                    ||
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    Console.WriteLine("Check the provided AWS Credentials.");
                    Console.WriteLine(
                        "For service sign up go to http://aws.amazon.com/s3");
                }
                else
                {
                    Console.WriteLine(
                        "Error occurred. Message:'{0}' when writing an object"
                        , amazonS3Exception.Message);
                }
            }
        }

        static string GeneratePreSignedURL(string _bucketname, string _keyname)
        {
            if (clients3 == null)
                clients3 = new AmazonS3Client(Amazon.RegionEndpoint.USWest2);

            string urlString = "";
            GetPreSignedUrlRequest request1 = new GetPreSignedUrlRequest
            {
                BucketName = _bucketname,
                Key = _keyname,
                Expires = DateTime.Now.AddMinutes(5)

            };

            try
            {
                urlString = clients3.GetPreSignedURL(request1);
                //string url = s3Client.GetPreSignedURL(request1);
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null &&
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId")
                    ||
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    Console.WriteLine("Check the provided AWS Credentials.");
                    Console.WriteLine(
                    "To sign up for service, go to http://aws.amazon.com/s3");
                }
                else
                {
                    Console.WriteLine(
                     "Error occurred. Message:'{0}' when listing objects",
                     amazonS3Exception.Message);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return urlString;

        }

        public static void ReadObjectData(string _bucketname, string _keyname, string _outputpath)
        {
            if (clients3 == null)
                clients3 = new AmazonS3Client(Amazon.RegionEndpoint.USWest2);

            using (clients3)
            {
                GetObjectRequest request = new GetObjectRequest
                {
                    BucketName = _bucketname,
                    Key = _keyname
                };
                GetObjectResponse response = clients3.GetObject(request);
                using (response)
                {
                        string title = response.Metadata["x-amz-meta-title"];
                        string dest = Path.Combine(_outputpath, title);
                        if (!File.Exists(dest))
                        {
                            response.WriteResponseStreamToFile(dest);
                        }
                };
            };

             
        }
    
        public static string createSNSTopic(string _topicname)
        {
            if (clientsns == null)
                clientsns = new AmazonSimpleNotificationServiceClient();
            var topicArn =  clientsns.CreateTopic(new CreateTopicRequest
                {
                    Name = _topicname
                }).TopicArn;


          return topicArn.ToString();

        }

        public static void subscribeToTopic(string _topicarn,string _email)
        {
            if (clientsns == null)
                clientsns = new AmazonSimpleNotificationServiceClient();

            clientsns.Subscribe(new SubscribeRequest
            {
                TopicArn = _topicarn,
                Protocol = "email",
                Endpoint = _email
            });


        }

        public static void publishToTopic(string _topicarn, string _subject, string _message)
        {
            if (clientsns == null)
                clientsns = new AmazonSimpleNotificationServiceClient();

            clientsns.Publish(new PublishRequest
            {
                Subject = _subject,
                Message = _message,
                TopicArn = _topicarn
            });

        }

    }
}