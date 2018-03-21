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

            //membermodel test
            var member = await MemberModel.GetUserWithLoginName("shawhu@qq.com");

            Common.LogDTO<MemberModel>(base.Request,member);
            return member;
        }
        
    }
}