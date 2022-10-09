namespace Api.Dto.Customers
{
    public class CustomerFetchDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = null!;
        public string EMail { get; set; } = null!;
        public string Cell { get; set; } = null!;
    }
}
