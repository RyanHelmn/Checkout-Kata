namespace CheckoutKata.Core.Extensions
{
    public static class CharExtensions
    {
        public static bool EqualsIgnoreCase(this char original, char compare)
        {
            return original.ToString().ToLower().Equals(compare.ToString().ToLower());
        }
    }
}
