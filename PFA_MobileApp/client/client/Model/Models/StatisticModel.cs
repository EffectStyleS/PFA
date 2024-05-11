namespace client.Model.Models;

public class StatisticModel<T>
{
    public string TypeName { get; set; }
    
    public T Value { get; set; }
}