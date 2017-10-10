namespace ViagogoEventIntegration.Abstractions
{
    public interface ICredentialProvider
    {
        string ClientId { get; }
        string ClientSecret { get; }
    }
}