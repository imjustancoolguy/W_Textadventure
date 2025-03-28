class Shop
{
    private Inventory stock;
    private Player customer;

    public Shop(Player newPlayer)
    {
        stock = new Inventory(999999999);
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
            if (pProduct != null)
            {
                if (customer.Currency >= pProduct.Price)
                {
                    customer.Backpack.Put(product, pProduct);
                    customer.Currency -= pProduct.Price;
                }
                else
                {
                    Console.WriteLine("you are too broke to buy this");
                    stock.Put(product, pProduct);
                }
            }
            else
            {
                Console.WriteLine($"there is no {product} for sale");
            }
        }
        else
        {
            Console.WriteLine("purchase what?");
        }
    }

    public void Sell(string product)
    {
        if (product != null && product != "")
        {
            Item sProduct = customer.Backpack.Get(product);
            if (sProduct != null)
            {

                stock.Put(product, sProduct);
                customer.Currency += Math.Floor(sProduct.Price * 0.95);

            }
            else
            {
                Console.WriteLine($"there is no {product} to sell");
            }
        }
        else
        {
            Console.WriteLine("purchase what?");
        }
    }
}