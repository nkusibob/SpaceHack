using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using app_HackSpace.Models;

namespace app_HackSpace
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit https://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configurer le contexte de base de données, le gestionnaire des utilisateurs et le gestionnaire des connexions pour utiliser une instance unique par demande
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Autoriser l’application à utiliser un cookie pour stocker des informations pour l’utilisateur connecté
            // et pour utiliser un cookie à des fins de stockage temporaire des informations sur la connexion utilisateur avec un fournisseur de connexion tiers
            // Configurer le cookie de connexion
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Permet à l'application de valider le timbre de sécurité quand l'utilisateur se connecte.
                    // Cette fonction de sécurité est utilisée quand vous changez un mot de passe ou ajoutez une connexion externe à votre compte.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });            
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Permet à l'application de stocker temporairement les informations utilisateur lors de la vérification du second facteur dans le processus d'authentification à 2 facteurs.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Permet à l'application de mémoriser le second facteur de vérification de la connexion, un numéro de téléphone ou un e-mail par exemple.
            // Lorsque vous activez cette option, votre seconde étape de vérification pendant le processus de connexion est mémorisée sur le poste à partir duquel vous vous êtes connecté.
            // Ceci est similaire à l'option RememberMe quand vous vous connectez.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Supprimer les commentaires des lignes suivantes pour autoriser la connexion avec des fournisseurs de connexions tiers
            app.UseMicrosoftAccountAuthentication(
                clientId: "aff7665d-cf62-4fbd-916d-26de6744b200",
                clientSecret: "NcnA5oB5NMafRPootwWzNe3");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            app.UseFacebookAuthentication(
               appId: "143034852924673",
               appSecret: "35f9e8a48a4d3a24baf5f8dad1fe0713");

            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "708142339190-3ovfks25en135bprihaa1d2b2plg8eo9.apps.googleusercontent.com",
                ClientSecret = "-6EjVGl3CtI0mZ_SzxlDPKBq"
            });
        }
    }
}