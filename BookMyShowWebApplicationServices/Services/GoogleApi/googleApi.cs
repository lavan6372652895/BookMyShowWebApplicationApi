//using BookMyShowWebApplicationServices.Interface.IgoogleApi;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace BookMyShowWebApplicationServices.Services.GoogleApi
//{
//    using Google.Apis.Auth.OAuth2;
//    using Google.Apis.Cloudchannel.v1;
//    using Google.Apis.Services;
//    using System.Threading.Tasks;

//    public class GoogleApi
//    {
//        private readonly CloudchannelService _cloudChannelService;

//        public GoogleApi(string applicationName, string credentialsJson)
//        {
//            GoogleCredential credential;

//            using (var stream = new FileStream(credentialsJson, FileMode.Open, FileAccess.Read))
//            {
//                credential = GoogleCredential.FromStream(stream)
//                    .CreateScoped(CloudchannelService.Scope.AppsOrder);
//            }

//            _cloudChannelService = new CloudchannelService(new BaseClientService.Initializer
//            {
//                HttpClientInitializer = credential,
//                ApplicationName = applicationName,
//            });
//        }

//        // Example method to list accounts
//        public async Task ListAccountsAsync()
//        {
//            var request = _cloudChannelService.Accounts.List();
//            var response = await request.ExecuteAsync();

//            foreach (var account in response.AccountsValue)
//            {
//                Console.WriteLine($"Account ID: {account.Name}, Account Display Name: {account.DisplayName}");
//            }
//        }
//    }

//}
