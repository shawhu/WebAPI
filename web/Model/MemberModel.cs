using System.Threading.Tasks;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;


namespace web
{
    [DynamoDBTable("wt_member")]
    public class MemberModel
    {
        //tags
        [DynamoDBProperty(AttributeName = "tags")]
        public string tags { get; set; }
        //added just to keep mailchimp join date
        [DynamoDBProperty(AttributeName = "newsletter_joindate")]
        public string newsletter_joindate { get; set; }
        //added for mailchimp newsletters
        [DynamoDBProperty(AttributeName = "newsletter_requested")]
        public string newsletter_requested { get; set; }
        [DynamoDBProperty(AttributeName = "newsletter_subscribed")]
        public string newsletter_subscribed { get; set; }

        [DynamoDBProperty(AttributeName = "applogincount")]
        public int applogincount { get; set; }
        [DynamoDBProperty(AttributeName = "weblogincount")]
        public int weblogincount { get; set; }
        [DynamoDBHashKey]
        public string LoginName { get; set; }
        [DynamoDBProperty(AttributeName = "platform")]
        public string platform { get; set; }
        [DynamoDBProperty(AttributeName = "is_from_website")]//aka registration-source
        public string is_from_website { get; set; }

        [DynamoDBProperty(AttributeName = "fb_uid")]
        public string FBUID { get; set; }

        [DynamoDBProperty(AttributeName = "first_name")]
        public string FirstName { get; set; }

        [DynamoDBProperty(AttributeName = "last_name")]
        public string LastName { get; set; }

        [DynamoDBProperty(AttributeName = "password")]
        public string EncryptedPassword { get; set; }

        [DynamoDBProperty(AttributeName = "gender")]
        public int Gender { get; set; }

        [DynamoDBProperty(AttributeName = "birthday")]
        public double Birthday { get; set; }

        [DynamoDBProperty(AttributeName = "nationality")]
        public string Nationality { get; set; }

        [DynamoDBProperty(AttributeName = "instrument")]
        public string Instrument { get; set; }

        [DynamoDBProperty(AttributeName = "music_status")]
        public string MusicStatus { get; set; }

        [DynamoDBProperty(AttributeName = "profile_icon")]
        public int ProfileIcon { get; set; }

        [DynamoDBProperty(AttributeName = "xp_level")]
        public double XPLevel { get; set; }

        [DynamoDBProperty(AttributeName = "xp_points")]
        public double XPPoints { get; set; }

        [DynamoDBProperty(AttributeName = "practise_stars")]
        public double PractiseStars { get; set; }

        [DynamoDBProperty(AttributeName = "member_guid")]
        public string MemberGuid { get; set; }

        [DynamoDBProperty(AttributeName = "create_date")]
        public double CreateDate { get; set; }
        //added by harry 9/13/2014
        [DynamoDBProperty(AttributeName = "activatekey")]
        public string ActivateKey { get; set; }
        //added by harry 12/15/14
        [DynamoDBProperty(AttributeName = "disabled")]
        public string Disabled { get; set; }
        //added by Harry 9/25/15
        [DynamoDBProperty(AttributeName = "disabled_date")]
        public double disabled_date { get; set; }
        //added by Harry 10/26/15
        [DynamoDBProperty(AttributeName = "last_active_date")]
        public double last_active_date { get; set; }
        [DynamoDBProperty(AttributeName = "last_purchase_date")]
        public double last_purchase_date { get; set; }
        [DynamoDBProperty(AttributeName = "other_instruments")]
        public string other_instruments { get; set; }
        [DynamoDBProperty(AttributeName = "user_type")]
        public string user_type { get; set; }
        [DynamoDBProperty(AttributeName = "school_name")]
        public string school_name { get; set; }

        [DynamoDBProperty(AttributeName = "longest_streak_days")]
        public int longest_streak_days { get; set; }
        [DynamoDBProperty(AttributeName = "first_name_key")]
        public string FirstNameKey { get; set; }

        [DynamoDBProperty(AttributeName = "last_name_key")]
        public string LastNameKey { get; set; }
        //wp270 added for organization
        [DynamoDBProperty]
        public int org_id { get; set; }
        [DynamoDBProperty]
        public double vat { get; set; }
        [DynamoDBProperty]
        public double minimum_order { get; set; }
        [DynamoDBProperty]
        public string payment_method { get; set; }
        //wp321
        [DynamoDBProperty]
        public int active_subscription_id { get; set; }
        [DynamoDBProperty]
        public double subscription_expire_date { get; set; }
        [DynamoDBProperty]
        public string sub_sent_from { get; set; }
        //wp341
        [DynamoDBProperty]
        public double subscription_start_date { get; set; }
        [DynamoDBProperty]
        public string subscription_provided_by_org { get; set; }

        private static AmazonDynamoDBClient _awsdb = new AmazonDynamoDBClient("AKIAPEDOSXBVQYIP5CQA","jqo1c2fsqt2B945e3ZPJep/ZLqAAZGHKXvxNKW3k",RegionEndpoint.CNNorth1);
        private static DynamoDBContext _ctxt = Common.GetDDBContextWithPrefix(_awsdb);
        public static async Task<MemberModel> GetUserWithLoginName(string loginname)
        {
            //DynamoDBContext context = Common.GetDDBContextWithPrefix(_awsdb);
            var context = _ctxt;
            var member = await context.LoadAsync<MemberModel>(loginname);
            if (member!=null)
            {
                member.EncryptedPassword = null;
            }
            //context.Dispose();
            return member;
        }
    }

}