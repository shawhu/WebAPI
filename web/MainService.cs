using ServiceStack;
using ServiceStack.Configuration;

namespace web
{
    public class MainService:Service
    {
        public IAppSettings AppSettings { get; set; }

        public TestResponse Any(TestRequest req)
        {
            string str = "";
            var token = base.Request.GetHeader("Authorization");
            str+=$"Token:{token} ";
            var awsappid = AppSettings.GetString("awsappid");
            str+=$"AwsAppId:{awsappid} ";

            var result = new TestResponse(){
                Output = str+$"Input:{req.Input}"
            };
            return result;
        }
    }
}