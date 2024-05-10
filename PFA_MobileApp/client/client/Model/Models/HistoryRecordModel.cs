namespace client.Model.Models;

public class HistoryRecordModel
{
    public string Name { get; set; } = string.Empty;
    
    public decimal Value { get; set; }
    
    public DateTime Date { get; set; }
}