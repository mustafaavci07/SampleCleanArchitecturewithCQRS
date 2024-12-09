using NUnit.Framework;

using Refit;

using SampleCleanArchitecture.Infrastructure.PaymentService;

using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

using Assert = NUnit.Framework.Assert;

namespace SampleCleanArchitecture.Test.Infrastructure.PaymentService
{
    public class PaymentServiceTest
    {

        private static IEnumerable<TestCaseData> GetInvalidPaymentRequests()
        {

            yield return new TestCaseData(new PaymentRequest(12.5,"TL","hatalý kart no","mustafa avcý","222",DateTime.Now.AddYears(1),null));
            yield return new TestCaseData(new PaymentRequest(0,"TL","1111222233334444", "mustafa avcý", "222", DateTime.Now.AddYears(1), null));
            yield return new TestCaseData(new PaymentRequest(12.5, "TL","1111222233334444",null,"222",DateTime.Now.AddYears(2),null));
            yield return new TestCaseData(new PaymentRequest(12.5, "TL", "1111222233334444", "mustafa avcý", "Hatalý cvv", DateTime.Now.AddYears(2), null));
            yield return new TestCaseData(new PaymentRequest(12.5, "TL", "1111222233334444", "mustafa avcý", "222", DateTime.Now.AddYears(-1), null));
        }

        private WireMockServer _wireMockServer;
        private IPaymentService _paymentService;

        [SetUp]
        public void Setup()
        {
            if (!_wireMockServer?.IsStarted ?? false)
                _wireMockServer = WireMockServer.Start();

            _paymentService = RestService.For<IPaymentService>(_wireMockServer.Urls[0]);
        }

        [TearDown]
        public void Teardown() {

            _wireMockServer.Stop();
            _wireMockServer.Dispose();
        }

        [Fact]        
        public async Task DoPayment_Valid_ReturnSuccess()
        {
            //arrange

            PaymentRequest validReq = new PaymentRequest() { Amount = 50.0, CardNumber = "1234123412341234", Currency = "TL", CVV = "123", OwnerName = "Mustafa Avcý", ValidTill = new DateTime(2027, 12, 31) };
            PaymentResponse validResp = new PaymentResponse() { CreatedAt = DateTime.Now, Status = "Accepted", Id = "591975d7-d972-4a8a-93c6-355c1279303e" };
            _wireMockServer.Given(
               Request.Create()
                   .WithPath("/payments")
                   .WithBodyAsJson(validReq)
                   .UsingPost()
                   .WithHeader("Bearer", "ey123123123123").WithHeader("Content-Type", "application/json")
           )
           .RespondWith(
               Response.Create()
                   .WithStatusCode(201)
                   .WithHeader("Content-Type", "application/json")
                   .WithBodyAsJson(validResp)
           );
            //act

            PaymentResponse result =await _paymentService.CreatePaymentAsync(validReq);

            //assert

            Assert.Equals(result.Id, validResp.Id);
            
        }

        [NUnit.Framework.Theory]
        [TestCaseSource(nameof(GetInvalidPaymentRequests))]
        public async Task DoPayment_InvalidRequests_ReturnBadRequests(PaymentRequest testCaseData)
        {
            //arrange
            PaymentResponse invalidResp = new PaymentResponse() { CreatedAt = DateTime.Now, Status = "Rejected"};
            _wireMockServer.Given(
               Request.Create()
                   .WithPath("/payments")
                   .WithBodyAsJson(testCaseData)
                   .UsingPost()
                   .WithHeader("Bearer", "ey123123123123").WithHeader("Content-Type", "application/json")
           )
           .RespondWith(
               Response.Create()
                   .WithStatusCode(400)
                   .WithHeader("Content-Type", "application/json")
                   .WithBodyAsJson(invalidResp)
           );
            //act

            PaymentResponse result = await _paymentService.CreatePaymentAsync(testCaseData);

            //assert

            Assert.Equals(result.Status , invalidResp.Status);
        }
    }
}