using FluentAssertions;

namespace SampleStack.Repository.Repositories.Tests
{
    public abstract class RepositoryTests
    {
        protected IRepository<TestRecord> _repository;

        protected RepositoryTests(IRepository<TestRecord> repository)
        {
            _repository = repository;
        }

        [Fact]
        public void MemoryRepositoryTest()
        {
            _repository.Should().NotBeNull();
        }

        [Fact]
        public void AddTest()
        {
            // Arrange
            var entity = new TestRecord { Id = 1 };

            // Act
            _repository.Add(entity);

            // Assert
            _repository.GetAll().Should().Contain(entity);
            _repository.GetAll().Should().HaveCount(1);
        }

        [Fact]
        public void AddTest_ShouldNotAddDuplicates()
        {
            // Arrange
            var entity1 = new TestRecord { Id = 1 };
            var entity2 = new TestRecord { Id = 1 };

            // Act
            _repository.Add(entity1);
            _repository.Add(entity2);

            // Assert
            _repository.GetAll().Should().Contain(entity1);
            _repository.GetAll().Should().HaveCount(1);
        }

        [Fact]
        public void DeleteTest()
        {
            // Arrange
            var entity = new TestRecord { Id = 1 };

            // Act
            _repository.Add(entity);
            _repository.Delete(entity);

            // Assert
            _repository.GetAll().Should().HaveCount(0);
        }

        [Fact]
        public void DeleteAllTest()
        {
            // Arrange
            var entity1 = new TestRecord { Id = 1 };
            var entity2 = new TestRecord { Id = 2 };

            // Act
            _repository.Add(entity1);
            _repository.Add(entity2);
            _repository.DeleteAll();

            // Assert
            _repository.GetAll().Should().BeEmpty();
        }

        [Fact]
        public void GetAllTest()
        {
            // Arrange
            var entity1 = new TestRecord { Id = 1 };
            var entity2 = new TestRecord { Id = 2 };

            // Act
            _repository.Add(entity1);
            _repository.Add(entity2);

            // Assert
            var allEntities = _repository.GetAll();
            allEntities.Should().HaveCount(2);
            allEntities.Should().Contain(entity1);
            allEntities.Should().Contain(entity2);
        }

        [Fact]
        public void GetByIdTest()
        {
            // Arrange
            var id = 99;
            var entity1 = new TestRecord { Id = id };
            var entity2 = new TestRecord { Id = 5 };

            // Act
            _repository.Add(entity1);
            _repository.Add(entity2);

            // Assert
            var retrievedEntity = _repository.GetById(id);
            entity1.Should().Be(retrievedEntity);

            _repository.GetAll().Should().Contain(retrievedEntity);
            _repository.GetAll().Should().HaveCount(2);
        }


        [Fact]
        public void UpdateTest()
        {
            // Arrange
            var entity = new TestRecord { Id = 1 };
            _repository.Add(entity);

            // Act
            entity.Name = "Updated";
            _repository.Update(entity);

            // Assert
            var retrievedEntity = _repository.GetById(1);
            retrievedEntity.Name.Should().Be("Updated");
        }
    }
}
