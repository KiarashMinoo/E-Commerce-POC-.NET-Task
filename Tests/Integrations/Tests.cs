using Api.Dto.Receipts;
using Api.Dto.Users;
using Application.CQRS.Products.Queries.ListAll;
using Application.CQRS.Products.Queries.Retrieve;
using Application.CQRS.Users.Commands.Login;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace Integrations
{
    public class Tests : IClassFixture<WebFactory>
    {
        private readonly HttpClient client;

        public Tests(WebFactory factory) => client = factory.CreateClient();

        [Fact]
        public async void Authorize_With_InValidUser()
        {
            var model = new LoginUserDto()
            {
                UserName = "InvalidUserName",
                Password = "InvalidPassword"
            };

            var response = await client.PostAsync("/api/Credential/login", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));

            Assert.Equal(System.Net.HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async void Authorize_With_ValidUser()
        {
            var model = new LoginUserDto()
            {
                UserName = "aminoo",
                Password = "aminoo123"
            };

            var response = await client.PostAsync("/api/Credential/login", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var result = JsonConvert.DeserializeObject<LoginUserCommandResultDto>(await response.Content.ReadAsStringAsync())!;
            Assert.NotNull(result);
            Assert.NotNull(result.Token);
        }

        [Fact]
        public async void Buy_Invalid_Product()
        {
            var loginModel = new LoginUserDto() { UserName = "aminoo", Password = "aminoo123" };
            var loginResponse = await client.PostAsync("/api/Credential/login", new StringContent(JsonConvert.SerializeObject(loginModel), Encoding.UTF8, "application/json"));
            var loginResult = JsonConvert.DeserializeObject<LoginUserCommandResultDto>(await loginResponse.Content.ReadAsStringAsync())!;

            var buyModel = new ReceiptDto() { ProductId = Guid.NewGuid(), Quantity = 1000 };
            using var buyRequestMessage = new HttpRequestMessage(HttpMethod.Post, "/api/Receipt");
            buyRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", loginResult.Token);
            buyRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(buyModel), Encoding.UTF8, "application/json");
            var buyResponse = await client.SendAsync(buyRequestMessage);

            Assert.Equal(HttpStatusCode.NotFound, buyResponse.StatusCode);
        }

        [Fact]
        public async void Buy_Valid_Product_Invalid_Quantity()
        {
            var loginModel = new LoginUserDto() { UserName = "aminoo", Password = "aminoo123" };
            var loginResponse = await client.PostAsync("/api/Credential/login", new StringContent(JsonConvert.SerializeObject(loginModel), Encoding.UTF8, "application/json"));
            var loginResult = JsonConvert.DeserializeObject<LoginUserCommandResultDto>(await loginResponse.Content.ReadAsStringAsync())!;

            using var productsRequestMessage = new HttpRequestMessage(HttpMethod.Get, "/api/Product");
            productsRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", loginResult.Token);
            var productsResponse = await client.SendAsync(productsRequestMessage);
            var products = JsonConvert.DeserializeObject<IEnumerable<ListAllProductQueryResultDto>>(await productsResponse.Content.ReadAsStringAsync())!;

            var random = new Random();
            var product = products.ElementAt(random.Next(products.Count()));

            var buyModel = new ReceiptDto() { ProductId = product.Id, Quantity = product.Quantity + 1 };
            using var buyRequestMessage = new HttpRequestMessage(HttpMethod.Post, "/api/Receipt");
            buyRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", loginResult.Token);
            buyRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(buyModel), Encoding.UTF8, "application/json");
            var buyResponse = await client.SendAsync(buyRequestMessage);

            Assert.Equal(HttpStatusCode.UnprocessableEntity, buyResponse.StatusCode);
        }

        [Fact]
        public async void Buy_Valid_Product_Valid_Quantity_The_Quantity_Must_Decrease()
        {
            var loginModel = new LoginUserDto() { UserName = "aminoo", Password = "aminoo123" };
            var loginResponse = await client.PostAsync("/api/Credential/login", new StringContent(JsonConvert.SerializeObject(loginModel), Encoding.UTF8, "application/json"));
            var loginResult = JsonConvert.DeserializeObject<LoginUserCommandResultDto>(await loginResponse.Content.ReadAsStringAsync())!;

            using var productsRequestMessage = new HttpRequestMessage(HttpMethod.Get, "/api/Product");
            productsRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", loginResult.Token);
            var productsResponse = await client.SendAsync(productsRequestMessage);
            var products = JsonConvert.DeserializeObject<IEnumerable<ListAllProductQueryResultDto>>(await productsResponse.Content.ReadAsStringAsync())!;

            var random = new Random();
            var selectedProduct = products.ElementAt(random.Next(products.Count()));

            var selectedQuantity = 1;// random.Next(selectedProduct.Quantity);
            var expectedQuantity = selectedProduct.Quantity - selectedQuantity;

            var buyModel = new ReceiptDto() { ProductId = selectedProduct.Id, Quantity = selectedQuantity };
            using var buyRequestMessage = new HttpRequestMessage(HttpMethod.Post, "/api/Receipt");
            buyRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", loginResult.Token);
            buyRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(buyModel), Encoding.UTF8, "application/json");
            var buyResponse = await client.SendAsync(buyRequestMessage);

            using var productRequestMessage = new HttpRequestMessage(HttpMethod.Get, $"/api/Product/{selectedProduct.Id}");
            productRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", loginResult.Token);
            var productResponse = await client.SendAsync(productRequestMessage);
            var product = JsonConvert.DeserializeObject<RetrieveProductQueryResultDto>(await productResponse.Content.ReadAsStringAsync())!;

            Assert.Equal(expectedQuantity, product.Quantity);
        }
    }
}
