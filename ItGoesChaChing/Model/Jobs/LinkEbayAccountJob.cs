using eBay.Service.Core.Soap;
using ItGoesChaChing.Model.Ebay;
using ItGoesChaChing.Model.Factories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItGoesChaChing.Model.Jobs
{
    /// <summary>
    /// This is a two step process to link an account with eBay.
    /// Step 1: Get the sign in URL from eBay and show it to the user.
    /// Step 2: Check to see that the user agreed to the sign in.
    /// </summary>
    public class LinkEbayAccountJob
    {
        private Context Context { get; set; }

        public string SessionId { get; set; }
        public string Result { get; set; }

        #region Constructors...

        public LinkEbayAccountJob() :
            this(DependencyFactory.Resolve<Context>())
        {

        }

        public LinkEbayAccountJob(Context context)
        {
            this.Context = context;
        }

        #endregion

        public void LinkToEbay()
        {
            EbayContext eBayContext = new EbayContext(null);
            GetSignInUrl command = new GetSignInUrl(eBayContext);
            command.Execute();

            this.SessionId = command.SessionId;
            string url = command.SignInUrl;

            System.Diagnostics.Process.Start(url);
        }

        public void Confirm()
        {
            string eBayToken = null;
            {
                EbayContext context = new EbayContext(null);
                FetchToken command = new FetchToken(context);
                command.SessionId = this.SessionId;
                command.Execute();

                eBayToken = command.EbayToken;
            }

            // Get the User's Details
            string userId = null;
            string siteCode = null;
            {
                EbayContext context = new EbayContext(eBayToken);
                GetUserCommand command = new GetUserCommand(context);
                command.Execute();

                userId = command.UserId;
                siteCode = command.SiteCode;
            }

            Account account = new Account()
            {
                EbayToken = eBayToken,
                UserId = userId,
                SiteCode = siteCode
            };

            // Get the Account's related Site Details
            {
                PopulateSitesJob job = new PopulateSitesJob();
                Site site = job.PopulateSite(this.Context.Sites, account);
                account.Site = site;
            }

            // Set the Accounts Alert Preferences
            // TODO: Need to try catch this.
            {
                UpdatePreferencesJob job = new UpdatePreferencesJob();
                job.Execute(account, this.Context.AlertPreferences);
            }

            // Save the account
            {
                this.Context.Accounts.Add(account);
                AccountsFactory factory = new AccountsFactory();
                factory.Save(this.Context.Accounts);

                ObservableCollection<Account> newAccounts = factory.Load();
            }

            this.Result = "Account Added";
        }

    }
}
