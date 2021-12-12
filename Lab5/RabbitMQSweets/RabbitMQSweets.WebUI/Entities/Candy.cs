namespace RabbitMQSweets.WebUI.Entities;

public class Candy
{
    public string Id { get; set; }
    
    public string Name { get; set; }
    public string Production { get; set; }
    public string[] Ingredients { get; set; }
    
    public int Energy { get; set; }
    public CandyValue CandyValue { get; set; }
    
    public CandyType[] CandyTypes { get; set; }
}

public class CandyComparer : IComparer<Candy>
{
    public int Compare(Candy? x, Candy? y)
    {
        if (ReferenceEquals(x, y)) return 0;
        if (ReferenceEquals(null, y)) return 1;
        if (ReferenceEquals(null, x)) return -1;
        return string.Compare(x.Id, y.Id, StringComparison.Ordinal);
    }
}