namespace Samurai_V2_.Net_8.DTOs
{
    public class EmployeeDtos
    {
        public int EmployeeId { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? Mobile { get; set; }

        public DateTime? DateofBirth { get; set; }


        public DateTime? ActionDate { get; set; }

        public bool? IsActive { get; set; }
    }
    public class Allemp
    {
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public List<EmployeeDtos> Datas { get; set; }

    }
}
