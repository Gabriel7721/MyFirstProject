public class Item
{
    public int Id { get; set; }
    public string ItemName { get; set; }
    public int Quantity { get; set; }

    public Item(int id, string itemName, int quantity)
    {
        Id = id;
        ItemName = itemName;
        Quantity = quantity;
    }
}
