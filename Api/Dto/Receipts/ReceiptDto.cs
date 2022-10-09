using System.ComponentModel.DataAnnotations;

namespace Api.Dto.Receipts
{
    public class ReceiptDto
    {
        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
