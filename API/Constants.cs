using IdentityServer4.Models;

namespace API
{
    public static class Constants
    {
        public const string ClientId = "oauthClient";
        public const string ClientSecret = "superSecretPassword";
        public const string Scope = "custom_scope";
        public const string ResourceId = "customAPI";
        public const string ResourceSecret = "anotherSecretPassword";
        public const string Flow = GrantType.ResourceOwnerPassword;
    }
}