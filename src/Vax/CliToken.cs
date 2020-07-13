

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Vax
{
    public class CliToken
    {
        public async Task<List<TokenBlock>> GetTokensAsync()
        {
            var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), 
                @".azure\accessTokens.json");
            var tokens = JsonConvert.DeserializeObject<List<TokenBlock>>(await File.ReadAllTextAsync(filePath));

            return tokens;
        }

        public async Task<TokenBlock> GetAdGraphTokenAsync()
        {
            var tokenInfo = (await GetTokensAsync())
                .First(tc => tc.Resource.Equals("https://graph.windows.net/", 
                StringComparison.OrdinalIgnoreCase));
            return tokenInfo;
        }
    }

    public class TokenBlock
    {
        [JsonProperty("tokenType")]
        public string TokenType { get; set; }
        [JsonProperty("expiresIn")]
        public int ExpiresIn { get; set; }
        [JsonProperty("expiresOn")]
        public string ExpiresOn { get; set; }
        [JsonProperty("resource")]
        public string Resource { get; set; }
        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }
        [JsonProperty("refreshToken")]
        public string RefreshToken { get; set; }
        [JsonProperty("identityProvider")]
        public string IdentityProvider { get; set; }
        [JsonProperty("userId")]
        public string UserId { get; set; }
        [JsonProperty("isMRRT")]
        public bool IsMRRT { get; set; }
        [JsonProperty("_clientId")]
        public string ClientId { get; set; }
        [JsonProperty("_authority")]
        public string Authority { get; set; }

        public string TenantId
        {
            get
            {
                if(!string.IsNullOrWhiteSpace(this.Authority))
                {
                    return this.Authority.Replace("https://login.microsoftonline.com/", string.Empty);
                }
                return string.Empty;
            }
        }
    }

    public class AdManagerReference
    {

        [JsonProperty("odata.metadata")]
        public string OdataMetadata { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        public string ManagerId
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(this.Url))
                {
                    var segments = new Uri(this.Url).Segments;
                    return segments[segments.Length - 2].Replace("/", string.Empty);
                }
                return string.Empty;
            }
        }

    }


    public partial class AdUserCollection
    {
        [JsonProperty("odata.metadata")]
        public Uri OdataMetadata { get; set; }

        [JsonProperty("value")]
        public AdUser[] Users { get; set; }
    }

    public partial class AdUser
    {
        [JsonProperty("odata.type")]
        public string OdataType { get; set; }

        [JsonProperty("objectType")]
        public string ObjectType { get; set; }

        [JsonProperty("objectId")]
        public Guid ObjectId { get; set; }

        [JsonProperty("deletionTimestamp")]
        public object DeletionTimestamp { get; set; }

        [JsonProperty("accountEnabled")]
        public bool AccountEnabled { get; set; }

        [JsonProperty("ageGroup")]
        public object AgeGroup { get; set; }

        [JsonProperty("assignedLicenses")]
        public object[] AssignedLicenses { get; set; }

        [JsonProperty("assignedPlans")]
        public object[] AssignedPlans { get; set; }

        [JsonProperty("city")]
        public object City { get; set; }

        [JsonProperty("companyName")]
        public object CompanyName { get; set; }

        [JsonProperty("consentProvidedForMinor")]
        public object ConsentProvidedForMinor { get; set; }

        [JsonProperty("country")]
        public object Country { get; set; }

        [JsonProperty("createdDateTime")]
        public DateTimeOffset CreatedDateTime { get; set; }

        [JsonProperty("creationType")]
        public string CreationType { get; set; }

        [JsonProperty("department")]
        public string Department { get; set; }

        [JsonProperty("dirSyncEnabled")]
        public object DirSyncEnabled { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("employeeId")]
        public object EmployeeId { get; set; }

        [JsonProperty("facsimileTelephoneNumber")]
        public object FacsimileTelephoneNumber { get; set; }

        [JsonProperty("givenName")]
        public string GivenName { get; set; }

        [JsonProperty("immutableId")]
        public object ImmutableId { get; set; }

        [JsonProperty("isCompromised")]
        public object IsCompromised { get; set; }

        [JsonProperty("jobTitle")]
        public string JobTitle { get; set; }

        [JsonProperty("lastDirSyncTime")]
        public object LastDirSyncTime { get; set; }

        [JsonProperty("legalAgeGroupClassification")]
        public object LegalAgeGroupClassification { get; set; }

        [JsonProperty("mail")]
        public string Mail { get; set; }

        [JsonProperty("mailNickname")]
        public string MailNickname { get; set; }

        [JsonProperty("mobile")]
        public object Mobile { get; set; }

        [JsonProperty("onPremisesDistinguishedName")]
        public object OnPremisesDistinguishedName { get; set; }

        [JsonProperty("onPremisesSecurityIdentifier")]
        public object OnPremisesSecurityIdentifier { get; set; }

        [JsonProperty("otherMails")]
        public string[] OtherMails { get; set; }

        [JsonProperty("passwordPolicies")]
        public string PasswordPolicies { get; set; }

        [JsonProperty("passwordProfile")]
        public object PasswordProfile { get; set; }

        [JsonProperty("physicalDeliveryOfficeName")]
        public object PhysicalDeliveryOfficeName { get; set; }

        [JsonProperty("postalCode")]
        public object PostalCode { get; set; }

        [JsonProperty("preferredLanguage")]
        public string PreferredLanguage { get; set; }

        [JsonProperty("provisionedPlans")]
        public object[] ProvisionedPlans { get; set; }

        [JsonProperty("provisioningErrors")]
        public object[] ProvisioningErrors { get; set; }

        [JsonProperty("proxyAddresses")]
        public string[] ProxyAddresses { get; set; }

        [JsonProperty("refreshTokensValidFromDateTime")]
        public DateTimeOffset? RefreshTokensValidFromDateTime { get; set; }

        [JsonProperty("showInAddressList")]
        public bool? ShowInAddressList { get; set; }

        [JsonProperty("signInNames")]
        public object[] SignInNames { get; set; }

        [JsonProperty("sipProxyAddress")]
        public object SipProxyAddress { get; set; }

        [JsonProperty("state")]
        public object State { get; set; }

        [JsonProperty("streetAddress")]
        public object StreetAddress { get; set; }

        [JsonProperty("surname")]
        public string Surname { get; set; }

        [JsonProperty("telephoneNumber")]
        public object TelephoneNumber { get; set; }

        [JsonProperty("thumbnailPhoto@odata.mediaEditLink")]
        public string ThumbnailPhotoOdataMediaEditLink { get; set; }

        [JsonProperty("usageLocation")]
        public string UsageLocation { get; set; }

        [JsonProperty("userIdentities")]
        public object[] UserIdentities { get; set; }

        [JsonProperty("userPrincipalName")]
        public string UserPrincipalName { get; set; }

        [JsonProperty("userState")]
        public string UserState { get; set; }

        [JsonProperty("userStateChangedOn")]
        public DateTimeOffset? UserStateChangedOn { get; set; }

        [JsonProperty("userType")]
        public string UserType { get; set; }
    }
}
