using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using NUnit.Framework;

using SampleCleanArchitecture.Infrastructure.Persistence;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SampleCleanArchitecture.Test.Application.Passengers
{
    [Test]
    public class CreatePassengerCommandTest
    {
        private SampleContext _context;
        private DbContextOptions<SampleContext> _options;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<SampleContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new SampleContext(_options);
            
        }

        [TearDown]
        public async Task TearDown()
        {
            await _context.DisposeAsync();
        }
    }
}
