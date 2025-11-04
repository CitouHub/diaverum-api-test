using Diaverum.API.ExceptionHandling;
using Diaverum.Domain;
using Diaverum.Test.Helper.Domain;
using Newtonsoft.Json;
using NSubstitute;
using System.Net;
using System.Net.Http.Json;
using System.Text;

namespace Diaverum.Test.API.Controller
{
    public class DiaverumItemControllerTest
    {
        public class AddDiaverumItemAsync
        {
            [Theory]
            [InlineData(null, 0, "dateValue")]
            [InlineData("requredStringValue", null, "dateValue")]
            [InlineData("requredStringValue", 0, null)]
            public async Task InvalidRequest(string? requredStringValue, int? evenNumber, string? dateValue)
            {
                // Arrange
                var app = new DiaverumAPIMock();
                var client = app.CreateClient();

                var request = new { requredStringValue, evenNumber, dateValue };
                var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

                // Act
                var response = await client.PostAsync($"v1/diaverumitem", content);
                var result = await response.Content.ReadFromJsonAsync<ErrorDTO>();

                // Assert
                Assert.Equal((int)HttpStatusCode.BadRequest, result?.Status);
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
                app.MockDiaverumItemService.DidNotReceiveWithAnyArgs();
            }

            [Fact]
            public async Task Success()
            {
                // Arrange
                var app = new DiaverumAPIMock();
                var client = app.CreateClient();

                var diaverumitemDto = DiaverumItemHelper.New();
                var content = new StringContent(JsonConvert.SerializeObject(diaverumitemDto), Encoding.UTF8, "application/json");

                app.MockDiaverumItemService.AddDiaverumItemAsync(Arg.Any<DiaverumItemDTO>()).Returns(new DiaverumItemDTO());

                // Act
                var response = await client.PostAsync($"v1/diaverumitem", content);
                var result = await response.Content.ReadFromJsonAsync<DiaverumItemDTO>();

                // Assert
                Assert.NotNull(result);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                await app.MockDiaverumItemService.Received(1).AddDiaverumItemAsync(Arg.Any<DiaverumItemDTO>());
            }
        }

        public class GetDiaverumItem
        {
            [Fact]
            public async Task InvalidRequest()
            {
                // Arrange
                var app = new DiaverumAPIMock();
                var client = app.CreateClient();

                // Act
                var response = await client.GetAsync($"v1/diaverumitem/invalid");
                var result = await response.Content.ReadFromJsonAsync<ErrorDTO>();

                // Assert
                Assert.Equal((int)HttpStatusCode.BadRequest, result?.Status);
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
                app.MockDiaverumItemService.DidNotReceiveWithAnyArgs();
            }

            [Fact]
            public async Task Success()
            {
                // Arrange
                var app = new DiaverumAPIMock();
                var client = app.CreateClient();

                var diaverumitemId = (short)1;
                var diaverumitemDto = DiaverumItemHelper.New(id: diaverumitemId);
                app.MockDiaverumItemService.GetDiaverumItemAsync(diaverumitemId).Returns(diaverumitemDto);

                // Act
                var response = await client.GetAsync($"v1/diaverumitem/{diaverumitemId}");
                var result = await response.Content.ReadFromJsonAsync<DiaverumItemDTO>();

                // Assert
                Assert.NotNull(result);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                await app.MockDiaverumItemService.Received(1).GetDiaverumItemAsync(diaverumitemId);
            }
        }

        public class GetDiaverumItems
        {
            [Fact]
            public async Task EmptyResult()
            {
                // Arrange
                var app = new DiaverumAPIMock();
                var client = app.CreateClient();
                app.MockDiaverumItemService.GetDiaverumItemListAsync().Returns((List<DiaverumItemDTO>?)null);

                // Act
                var response = await client.GetAsync($"v1/diaverumitem/list");
                var result = await response.Content.ReadAsStringAsync();

                // Assert
                Assert.True(string.IsNullOrEmpty(result));
                Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
                await app.MockDiaverumItemService.Received(1).GetDiaverumItemListAsync();
            }

            [Fact]
            public async Task Success()
            {
                // Arrange
                var app = new DiaverumAPIMock();
                var client = app.CreateClient();
                app.MockDiaverumItemService.GetDiaverumItemListAsync().Returns([new(), new()]);

                // Act
                var response = await client.GetAsync($"v1/diaverumitem/list");
                var result = await response.Content.ReadFromJsonAsync<List<DiaverumItemDTO>>();

                // Assert
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.Equal(2, result?.Count);
                await app.MockDiaverumItemService.Received(1).GetDiaverumItemListAsync();
            }
        }

        public class UpdateDiaverumItem
        {
            [Theory]
            [InlineData(null, 0, "dateValue")]
            [InlineData("requredStringValue", null, "dateValue")]
            [InlineData("requredStringValue", 0, null)]
            public async Task InvalidRequest(string? requredStringValue, int? evenNumber, string? dateValue)
            {
                // Arrange
                var app = new DiaverumAPIMock();
                var client = app.CreateClient();

                var request = new { requredStringValue, evenNumber, dateValue };
                var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

                // Act
                var response = await client.PutAsync($"v1/diaverumitem", content);
                var result = await response.Content.ReadFromJsonAsync<ErrorDTO>();

                // Assert
                Assert.Equal((int)HttpStatusCode.BadRequest, result?.Status);
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
                app.MockDiaverumItemService.DidNotReceiveWithAnyArgs();
            }

            [Fact]
            public async Task Success()
            {
                // Arrange
                var app = new DiaverumAPIMock();
                var client = app.CreateClient();

                var diaverumitemDto = DiaverumItemHelper.New();
                var content = new StringContent(JsonConvert.SerializeObject(diaverumitemDto), Encoding.UTF8, "application/json");

                app.MockDiaverumItemService.UpdateDiaverumItemAsync(Arg.Any<DiaverumItemDTO>()).Returns(new DiaverumItemDTO());

                // Act
                var response = await client.PutAsync($"v1/diaverumitem", content);
                var result = await response.Content.ReadFromJsonAsync<DiaverumItemDTO>();

                // Assert
                Assert.NotNull(result);
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                await app.MockDiaverumItemService.Received(1).UpdateDiaverumItemAsync(Arg.Any<DiaverumItemDTO>());
            }
        }

        public class DeleteDiaverumItem
        {
            [Fact]
            public async Task InvalidRequest()
            {
                // Arrange
                var app = new DiaverumAPIMock();
                var client = app.CreateClient();

                // Act
                var response = await client.DeleteAsync($"v1/diaverumitem/invalid");
                var result = await response.Content.ReadFromJsonAsync<ErrorDTO>();

                // Assert
                Assert.Equal((int)HttpStatusCode.BadRequest, result?.Status);
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
                app.MockDiaverumItemService.DidNotReceiveWithAnyArgs();
            }

            [Fact]
            public async Task Success()
            {
                // Arrange
                var app = new DiaverumAPIMock();
                var client = app.CreateClient();
                var diaverumitemId = (short)1;

                // Act
                var response = await client.DeleteAsync($"v1/diaverumitem/{diaverumitemId}");

                // Assert
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                await app.MockDiaverumItemService.Received(1).DeleteDiaverumItemAsync(diaverumitemId);
            }
        }
    }
}
