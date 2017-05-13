using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;

namespace MstdnAPI {
    [Table("USERDATA")]
    public class UserData {
        public string UserId { get; set; }
        public string Password { get; set; }
        public string MstdnHost { get; set; }
    }
    [Table("APPKEY")]
    public class AppKey {
        public string ClientId { get; set; }
        public string ClientSec { get; set; }
    }
    [Table("TOKEN")]
    public class AccessToken {
        public string Host { get; set; }
        public string Token { get; set; }
    }
}
