namespace SmsGateway.Domain.CoreContext.SMSModule.Services
{
    public interface IDatabases : System.IDisposable
    {

        bool Start();
    }
}
