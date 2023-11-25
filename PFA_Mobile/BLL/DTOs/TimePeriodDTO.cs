using DAL.Entities;

namespace BLL.DTOs
{
    public class TimePeriodDTO
    {
        public TimePeriodDTO() { }

        public TimePeriodDTO(TimePeriod timePeriod)
        {
            Id   = timePeriod.Id;
            Name = timePeriod.Name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
