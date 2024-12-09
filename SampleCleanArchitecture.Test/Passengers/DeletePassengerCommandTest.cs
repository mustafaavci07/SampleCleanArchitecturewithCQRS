
using Ardalis.GuardClauses;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using SampleCleanArchitecture.Application.Passengers.Commands.CreatePassenger;
using SampleCleanArchitecture.Application.Passengers.Commands.DeletePassenger;
using SampleCleanArchitecture.Infrastructure.Persistence;
using SampleCleanArchitecture.Shared;

using System.Reflection;

namespace SampleCleanArchitecture.Test.Application.Passengers
{
    public class DeletePassengerCommandTest
    {
        private SampleContext _context;
        private DbContextOptions<SampleContext> _options;
        private DeletePassengerCommandHandler _handler { get; set; }
        private IMapper _mapper { get; set; }

      


        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<SampleContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            MapperConfiguration _mapConfig = new(cfg => cfg.AddMaps(Assembly.GetExecutingAssembly()));

            _mapper = _mapConfig.CreateMapper();

            _context = new SampleContext(_options);
            
            _handler = new(_context);

        }


        [TearDown]
        public async Task TearDown()
        {
            await _context.DisposeAsync();
        }

        [Fact]
        public void Handle_NotFound_ShouldThrowGuardException()
        {
            //arrange
            DeletePassengerCommand cmd = new DeletePassengerCommand(new Ulid(Guid.NewGuid()));
            //act
            NUnit.Framework.Assert.Throws<NotFoundException>( ()=>_handler.Handle(cmd,CancellationToken.None));
            //assert
        }

    }
}
