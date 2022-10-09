namespace InviteService
{
    public interface ISettings
    {
        int MaxCountInviteSend { get; }
        int MaxCountInviteInDay { get; }
        string PhoneTemplate { get; }

    }
}