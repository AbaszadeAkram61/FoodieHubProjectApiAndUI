using Restaurant.WebApi.Models.Entities;

namespace Resturant.WebUI.Models.Dto
{
    public class UpdateProductDtos
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
