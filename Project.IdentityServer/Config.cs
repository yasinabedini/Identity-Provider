using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using IdentityModel;

namespace Project.IdentityServer;

public static class Config
{
    #region Identity Resources
    public static IEnumerable<IdentityResource> IdentityResources =>
   new IdentityResource[]
   {
            new IdentityResources.Profile(),
            new IdentityResources.OpenId()
   }; 
    #endregion

    #region Api Scopes
    public static IEnumerable<ApiScope> ApiScopes =>
   new ApiScope[]
       {
            new ApiScope{Name="Project.Api",DisplayName="My Api"}
           }; 
    #endregion

    #region Client
    public static IEnumerable<Client> Clients =>
   new Client[]
       {
                new Client
                {
                    ClientName = "Project.Client",
                    ClientId  = "Project.Client",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("Project.Client".ToSha256())
                    },
                    AllowedGrantTypes=GrantTypes.Code,
                     RedirectUris={ "https://localhost:7242/signin-oidc" },
                   PostLogoutRedirectUris={ "https://localhost:7242/signout-callback-oidc" },
                   AllowedScopes = new List<string>
                   {
                       IdentityServerConstants.StandardScopes.OpenId,
                       IdentityServerConstants.StandardScopes.Profile,
                       "Project.Api",
                   },
                   AllowOfflineAccess = true,
                }
       }; 
    #endregion
}