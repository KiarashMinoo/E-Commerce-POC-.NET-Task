namespace Application.CQRS.Customers.Queries.Retrieve
{
    public class RetrieveCustomerQueryResultDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = null!;
        public string EMail { get; set; } = null!;
        public string Cell { get; set; } = null!;
    }
}
