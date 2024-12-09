using AutoMapper;

using FluentAssertions;

using GraphQL;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using NUnit.Framework;

using SampleCleanArchitecture.Application.Passengers.Commands.CreatePassenger;
using SampleCleanArchitecture.Infrastructure.Persistence;
using SampleCleanArchitecture.Shared;

using System.Reflection;

using Assert = NUnit.Framework.Assert;
using TheoryAttribute = NUnit.Framework.TheoryAttribute;



namespace SampleCleanArchitecture.Test.Application.Passengers
{

    public class CreatePassengerCommandTest
    {
        private SampleContext _context;
        private DbContextOptions<SampleContext> _options;
        private CreatePassengerValidation _validator { get; set; }
        private CreatePassengerCommandHandler _handler { get; set; }
        private IMapper _mapper { get; set; }

        private static IEnumerable<TestCaseData> GetInvalidCommands()
        {
       
            yield return new TestCaseData(new CreatePassengerCommand( Address:null, MailAddress:"test@test.com",PhoneNumber:"12312312",Name:"test name",Surname:"test surname", BirthDate:DateTime.Now.AddYears(-20), Gender:Gender.Female, NationalIdentityNumber:"12345" ));
            yield return new TestCaseData(new CreatePassengerCommand(Address: "valid address", MailAddress: "invalidmail", PhoneNumber: "12312312", Name: "test name", Surname: "test surname", BirthDate: DateTime.Now.AddYears(-20), Gender: Gender.Female, NationalIdentityNumber: "12345"));
            yield return new TestCaseData(new CreatePassengerCommand(Address: "valid address", MailAddress: "test@test.com", PhoneNumber: "12312312", Name: "test name", Surname: "test surname", BirthDate: DateTime.Now.AddYears(20), Gender: Gender.Female, NationalIdentityNumber: "12345"));
            yield return new TestCaseData(new CreatePassengerCommand(Address: "valid address", MailAddress: "test@test.com", PhoneNumber: "12312312", Name: "test name", Surname: "test surname", BirthDate: DateTime.Now.AddYears(-20), Gender: Gender.Female, NationalIdentityNumber: ""));
            yield return new TestCaseData(new CreatePassengerCommand(Address: "valid address", MailAddress: "test@test.com", PhoneNumber: "", Name: "test name", Surname: "test surname", BirthDate: DateTime.Now.AddYears(-20), Gender: Gender.Female, NationalIdentityNumber: "12345"));
        }
    

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<SampleContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            MapperConfiguration _mapConfig = new(cfg => cfg.AddMaps(Assembly.GetExecutingAssembly()));

            _mapper = _mapConfig.CreateMapper();

            _context = new SampleContext(_options);
            _validator = new();
            _handler = new(_context, _mapper);

        }


        [TearDown]
        public async Task TearDown()
        {
            await _context.DisposeAsync();
        }

        [Fact]
        // metod adı Islem+BeklenenSonuc
        public async Task Handle_ValidCommand_ShouldCreate()
        {
            //arrange
            CreatePassengerCommand command = new(DateTime.Now.AddYears(-20).AddMonths(-1),
                                                "12345678912", "Mustafa", "Avcı", Shared.Gender.Male,
                                                "mavci@innova.com.tr", "5551112233", "Ankara");

            //act

            var result = await _handler.Handle(command, CancellationToken.None);

            //assert

            result.Should().NotBeNull();
            var user = await _context.Passengers.FindAsync(result);
            user.Should().NotBeNull();
            user.NationalIdentityNumber.Should().Be(command.NationalIdentityNumber);
        }

        [Theory]
        [TestCaseSource(nameof(GetInvalidCommands))]
        public void Handle_InValidCommands_ShouldThrowValidationException(CreatePassengerCommand incorrectCommand)
        {

            Assert.Throws<ValidationException>( () => _handler.Handle(incorrectCommand, CancellationToken.None) );
        }

        

    }

}
