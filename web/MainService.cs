using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ServiceStack;
using ServiceStack.Configuration;
using ServiceStack.Logging;

namespace web
{
    public class MainService:Service
    {
        public IAppSettings Settings { get; set; }
        private static NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();
        public async Task<object> Any(TestRequest req)
        {
            string str = "";
            Dictionary<string,string> dict = new Dictionary<string,string>();
            //testing Common
            var token = base.Request.GetHeader("Authorization");
            dict.Add("Token",$"{token}");

            var awsappid = Settings.GetString("awsappid");//case sensitive
            dict.Add("AwsAppID",$"{awsappid}");

            str+=$" Input:{req.Input}";
            dict.Add("Input",$"{req.Input}");

            var result = new TestResponse(){
                Output = "",
                dictOutput = dict
            };
            Common.LogDTO(base.Request,result);

            //membermodel test
            var member = await MemberModel.GetUserWithLoginName("shawhu@qq.com");
            log.Info($"member:{member.ToJson()}");

            log.Info($"test is done at {DateTime.Now.ToString()}");
            return result;
        }
        
    }
}