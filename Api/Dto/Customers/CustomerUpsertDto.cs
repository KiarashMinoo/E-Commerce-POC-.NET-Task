namespace Api.Dto.Customers
{
    public class CustomerUpsertDto
    {
        public string FullName { get; set; } = null!;
        public string EMail { get; set; } = null!;
        public string Cell { get; set; } = null!;
    }
}
