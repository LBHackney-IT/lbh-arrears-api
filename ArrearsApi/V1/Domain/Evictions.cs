using System;

namespace ArrearsApi.V1.Domain
{   
    public class Evictions
    {
        public int Id { get; set; }       
        public DateTime Date { get; set; }      
        public string TenancyRef { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
