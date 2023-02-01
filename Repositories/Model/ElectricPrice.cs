namespace Repositories.Model;

public partial class ElectricPrice
{
    public Guid Id { get; set; }
    public int Level { get; set; }
    public int From { get; set; }
    public int To { get; set; }
    public long StandardPrice { get; set; }
    public string Description { get; set; }
}