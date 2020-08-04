namespace ConsoleEShopMultilayered.DAL.Models
{
    public enum OrderState
    {
        New,
        Paid,
        Sent,
        Received,
        Completed,
        CanceledByAdmin,
        CanceledByUser
    }
}
