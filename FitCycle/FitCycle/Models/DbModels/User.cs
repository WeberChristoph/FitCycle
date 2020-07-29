using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Diagnostics;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.MobileServices;

namespace FitCycle
{
    public class User
    {
       
		[JsonProperty(PropertyName = "id")]
		public string Id { get; set; }

		[JsonProperty(PropertyName = "UserName")]
		public string UserName { get; set; }

		[JsonProperty(PropertyName = "UserID")]
        public int UserID { get; set; }

        [JsonProperty(PropertyName = "Weight")]
        public int Weight { get; set; }
        [JsonProperty(PropertyName = "Age")]
        public int Age { get; set; }

        [JsonProperty(PropertyName = "MailAdress")]
        public string MailAdress { get; set; }
        [JsonProperty(PropertyName = "Password")]
        public string Password { get; set; }
        [JsonProperty(PropertyName = "Password_Salt")]
        public string Password_Salt { get; set; }

        [Version]
		public string Version { get; set; }
	}
}
