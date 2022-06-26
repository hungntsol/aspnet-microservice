namespace Basket.API.Entities;

public class ShoppingCart
{
    public string Username { get; set; }
    public IList<ShoppingCartItem> CartItems { get; set; }

    public ShoppingCart(string username)
    {
        Username = username;
    }

    public decimal TotalPrice
    {
        get
        {
            decimal total = 0;
            foreach (var item in CartItems)
            {
                total += item.Price * item.Quantity;
            }

            return total;
        }
    }
}