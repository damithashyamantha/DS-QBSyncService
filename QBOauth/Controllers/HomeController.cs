using Newtonsoft.Json;
using QBAuthManager;
using QBAuthManager.Models;
using QBBusinessService.Interfaces;
using QBBusinessService.System;
using QBEntity.System;
using QBOauth.Models;
using RavenHandler;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace QBOauth.Controllers
{
    public class HomeController : Controller
    {
        //private static AuthManager am;
        private static string realm;

        private static readonly string tokenIdKey = "TokenIdKey";

        static string refreshToken;
        static string accessToken;
        OAuthSettings settings;

        public async Task<ActionResult> Index()
        {
            await DoAuth();
            return View();
        }


        public async Task<ActionResult> Home(string state, string code, string realmId)
        {
            QBServiceCreator creator = new QBServiceCreator();
            using (var authManager = creator.GetService<IAuthService>())
            {
                TokenBaerer token = await authManager.GetNewTokenAsync(state, code, realmId);

                realm = realmId;
                token.RealmId = realmId;
                accessToken = token.AccessToken;
                return
                    View(new AuthModel { Tokens = token });
            }


        }

        private async Task DoAuth()
        {
            string tokenId = Session[tokenIdKey] as string;
            if (string.IsNullOrEmpty(tokenId))
            {
                QBServiceCreator creator = new QBServiceCreator();
                var authService = creator.GetService<IAuthService>();
                var rs = await authService.GetAuthRequestAsync();
                Response.Redirect(rs);
            }


        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public async Task<ActionResult> GetCompany()
        {
            ServiceSettings ss = new ServiceSettings();
            using (var manager = RavenManager.Instance)
            {
                TokenBaerer token = await manager.GetLatestTokenAsync();
                ss.Token = token;
            }

            ss.BaseUrl = "https://sandbox-quickbooks.api.intuit.com/";

            using (var sm = new ServiceManager(ss))
            {
                //var res = await sm.GetCompany();

                return View();
            }
        }

        public void CreatePurchaseOrder(AuthModel model)
        {

            try
            {
                //PurchaseOrder order = JsonConvert.DeserializeObject<PurchaseOrder>(model.Json);
            }
            catch (Exception exception)
            {

                throw;
            }
        }

        public async Task<ActionResult> CreateCustomer(AuthModel model)
        {
            try
            {
                //ServiceSettings ss = new ServiceSettings();
                //using (var manager = RavenManager.Instance)
                //{
                //    ss.Token = await manager.GetLatestToken();
                //}

                //ss.BaseUrl = "https://sandbox-quickbooks.api.intuit.com/";

                //using (ServiceManager sm = new ServiceManager(ss))
                //{
                //    model.Status = await sm.CreateCustomer(model.Customer);

                return View(model);
                //}


            }
            catch (Exception exception)
            {

                throw;
            }
        }
    }

}