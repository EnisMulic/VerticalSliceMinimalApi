using Microsoft.Identity.Web;

namespace Api.Options;

public class MicrosoftEntraOptions : MicrosoftIdentityOptions
{
    public const string SectionName = "Entra";

    public string BaseUrl => $"{Instance}/{TenantId}/oauth2/v2.0";
    public string AuthorizationUrl => $"{BaseUrl}/authorize";
    public string TokenUrl => $"{BaseUrl}/token";
}