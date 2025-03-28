class Shop
{
    private Inventory stock;
    private Player customer;

    public Shop(Player newPlayer)
    {
        stock = new Inventory(999999);
        customer = newPlayer;
    }

    public Inventory Stock
    {
        get { return stock; }
    }

    public void purchase(string product)
    {
        if (product != null && product != "")
        {
            Item pProduct = stock.Get(product);
            if (customer.Currency >= pProduct.Price)
            {
                if (stock.Items.ContainsKey(product))
                {
                    customer.Backpack.Put(product, pProduct);
                    customer.Currency -= pProduct.Price;
                }
                else
                {
                    Console.WriteLine($"there is no {product} for sale");
                    stock.Put(product, pProduct);
                }
            }
            else
            {
                Console.WriteLine("you are too broke to buy this");
            }
        }
        else
        {
            Console.WriteLine("purchase what?");
        }
    }

    public void Sell(string product)
    {

    }
}