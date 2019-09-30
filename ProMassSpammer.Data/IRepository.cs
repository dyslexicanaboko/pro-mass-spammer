using ProMassSpammer.Data.Interfaces;

namespace ProMassSpammer.Data
{
    public interface IRepository
    {
        IMassCommunicationRepository MassCommunicationRepository { get; set; }

        ILogRepository LogRepository { get; set; }

        IRecipientRepository RecipientRepository { get; set; }
    }
}
