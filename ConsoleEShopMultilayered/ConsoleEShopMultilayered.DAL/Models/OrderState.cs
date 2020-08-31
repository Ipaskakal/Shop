namespace ConsoleEShopMultilayered.DAL.Models
{
    /// <summary>
    /// States of order
    /// </summary>  
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
