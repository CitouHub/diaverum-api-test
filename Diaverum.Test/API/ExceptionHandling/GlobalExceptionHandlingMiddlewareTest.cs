//using Diaverum.API.ExceptionHandling;
//using Diaverum.Common;
//using Diaverum.Service.CustomeException;
//using Diaverum.Test.Helper;
//using NSubstitute;
//using NSubstitute.ExceptionExtensions;
//using System.Net;
//using System.Net.Http.Json;

//namespace Diaverum.Test.API.ExceptionHandling
//{
//    public class GlobalExceptionHandlingMiddlewareTest
//    {
//        [Theory]
//        [InlineData(ExceptionType.InvalidRequest, HttpStatusCode.BadRequest)]
//        [InlineData(ExceptionType.ItemNotFound, HttpStatusCode.NotFound)]
//        public async Task ServiceException(ExceptionType exceptionType, HttpStatusCode expectedHttpStatusCode)
//        {
//            // Arrange
//            var app = new DiaverumAPIMock();
//            var client = app.CreateClient();
//            app.MockDiaverumItemService.GetDiaverumItemListAsync()
//                .Throws(new ServiceException(exceptionType));

//            // Act
//            var response = await client.GetAsync(@$"{Constants.API_VERSION}/invoice/
//                {SupportedExternalSource.GSuite}/
//                {DateTime.Now:yyyy-MM-dd}/
//                {DateTime.Now:yyyy-MM-dd}");
//            var result = await response.Content.ReadFromJsonAsync<ErrorDTO>();

//            // Assert
//            Assert.Equal((int)expectedHttpStatusCode, result?.Status);
//            Assert.Equal(expectedHttpStatusCode, response.StatusCode);
//        }

//        [Fact]
//        public async Task ServiceException_WithDetails()
//        {
//            // Arrange
//            var app = new DiaverumAPIMock();
//            app.SetAuthorizationActive(false);
//            var client = app.CreateClient();
//            var details = "ExceptionDetails";
//            app.MockExternalSourceService.GetInvoicesAsync(
//                Arg.Any<SupportedExternalSource>(),
//                Arg.Any<DateTime>(),
//                Arg.Any<DateTime>(),
//                Arg.Any<string?>())
//                .Throws(new ServiceException(ExceptionType.InvalidRequest, details: details));

//            // Act
//            var response = await client.GetAsync($"{Constants.API_VERSION}/invoice/{SupportedExternalSource.GSuite}/{DateTime.Now:yyyy-MM-dd}/{DateTime.Now:yyyy-MM-dd}");
//            var result = await response.Content.ReadFromJsonAsync<ErrorDTO>();

//            // Assert
//            Assert.Contains(details, result!.Details.First());
//        }

//        [Fact]
//        public async Task ServiceException_InnerException()
//        {
//            // Arrange
//            var app = new DiaverumAPIMock();
//            app.SetAuthorizationActive(false);
//            var client = app.CreateClient();
//            var exceptionMessage = "InnerTestException";
//            app.MockExternalSourceService.GetInvoicesAsync(
//                Arg.Any<SupportedExternalSource>(),
//                Arg.Any<DateTime>(),
//                Arg.Any<DateTime>(),
//                Arg.Any<string?>())
//                .Throws(new ServiceException(ExceptionType.InvalidRequest, innerException: new Exception(exceptionMessage)));

//            // Act
//            var response = await client.GetAsync($"{Constants.API_VERSION}/invoice/{SupportedExternalSource.GSuite}/{DateTime.Now:yyyy-MM-dd}/{DateTime.Now:yyyy-MM-dd}");
//            var result = await response.Content.ReadFromJsonAsync<ErrorDTO>();

//            // Assert
//            Assert.Equal(exceptionMessage, result!.Details[1]);
//        }

//        [Fact]
//        public async Task UnknownException()
//        {
//            // Arrange
//            var app = new DiaverumAPIMock();
//            app.SetAuthorizationActive(false);
//            var client = app.CreateClient();
//            app.MockExternalSourceService.GetInvoicesAsync(
//                Arg.Any<SupportedExternalSource>(),
//                Arg.Any<DateTime>(),
//                Arg.Any<DateTime>(),
//                Arg.Any<string?>())
//                .Throws(new Exception());

//            // Act
//            var response = await client.GetAsync($"{Constants.API_VERSION}/invoice/{SupportedExternalSource.GSuite}/{DateTime.Now:yyyy-MM-dd}/{DateTime.Now:yyyy-MM-dd}");
//            var result = await response.Content.ReadFromJsonAsync<ErrorDTO>();

//            // Assert
//            Assert.Equal((int)HttpStatusCode.InternalServerError, result?.Status);
//            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
//        }
//    }
//}
