namespace CheckoutKata.Core.Interfaces
{
    public interface ICheckout
    {
        void Scan(string skus);
        int GetTotalPrice();
    }
}
