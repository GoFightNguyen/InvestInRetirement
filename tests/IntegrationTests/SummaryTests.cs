using AngleSharp;
using AngleSharp.Html.Dom;
using AngleSharp.Io;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace IntegrationTests
{
    [TestClass]
    public class SummaryTests
    {
        private readonly WebApplicationFactory<UI.Startup> _factory;

        public SummaryTests()
        {
            _factory = new WebApplicationFactory<UI.Startup>();
        }

        [TestMethod]
        public async Task Get()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/Summary");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual("text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        //TODO: research how to do this 
        [TestMethod]
        public async Task Post()
        {
            /*
             * This information comes from https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-2.1#basic-tests-with-the-default-webapplicationfactory
             * 
             * Any POST request to the SUT must satisfy the antiforgery check that's automatically made by the app's data protection antiforgery system. 
             * In order to arrange for a test's POST request, the test app must:
                    Make a request for the page.
                    Parse the antiforgery cookie and request validation token from the response.
                    Make the POST request with the antiforgery cookie and request validation token in place.

            The SendAsync helper extension methods (Helpers/HttpClientExtensions.cs) and the GetDocumentAsync helper method (Helpers/HtmlHelpers.cs) 
            in the sample app use the AngleSharp parser to handle the antiforgery check with the following methods:
                GetDocumentAsync – Receives the HttpResponseMessage and returns an IHtmlDocument. 
                    GetDocumentAsync uses a factory that prepares a virtual response based on the original HttpResponseMessage
                SendAsync extension methods for the HttpClient compose an HttpRequestMessage and call SendAsync(HttpRequestMessage) to submit requests to the SUT. 
                    Overloads for SendAsync accept the HTML form (IHtmlFormElement) and the following: 
                        Submit button of the form (IHtmlElement)
                        Form values collection (IEnumerable<KeyValuePair<string, string>>)
                        Submit button (IHtmlElement) and form values (IEnumerable<KeyValuePair<string, string>>)
             */

            // Arrange
            var client = _factory.CreateClient();
            var summaryPage = await client.GetAsync("/Summary");
            var content = await HtmlHelpers.GetDocumentAsync(summaryPage);

            content.Forms[0].Elements["InvestmentPercentage"].TextContent = "15";
            content.Forms[0].Elements["AnnualSalary"].TextContent = "106,156";

            // Act
            //var response = await client.PostAsync("/Summary", 
                //(IHtmlFormElement)content.QuerySelector("form[id='messages']"),
                //(IHtmlButtonElement)content.QuerySelector("button[id='deleteAllBtn']"));

            //// Assert
            //Assert.Equal(HttpStatusCode.OK, defaultPage.StatusCode);
            //Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            //Assert.Equal("/", response.Headers.Location.OriginalString);
        }

        private class HtmlHelpers
        {
            // This came from https://github.com/aspnet/Docs/blob/master/aspnetcore/test/integration-tests/samples/2.x/IntegrationTestsSample/tests/RazorPagesProject.Tests/Helpers/HtmlHelpers.cs
            public static async Task<IHtmlDocument> GetDocumentAsync(HttpResponseMessage response)
            {
                var content = await response.Content.ReadAsStringAsync();
                var document = await BrowsingContext.New()
                    .OpenAsync(ResponseFactory, CancellationToken.None);

                return (IHtmlDocument)document;

                void ResponseFactory(VirtualResponse htmlResponse)
                {
                    htmlResponse
                        .Address(response.RequestMessage.RequestUri)
                        .Status(response.StatusCode);

                    MapHeaders(response.Headers);
                    MapHeaders(response.Content.Headers);

                    htmlResponse.Content(content);

                    void MapHeaders(HttpHeaders headers)
                    {
                        foreach (var header in headers)
                        {
                            foreach (var value in header.Value)
                            {
                                htmlResponse.Header(header.Key, value);
                            }
                        }
                    }
                }
            }
        }
    }
}
