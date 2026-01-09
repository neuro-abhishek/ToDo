namespace ToDo.Tests;

using MongoDB.Driver;
using Xunit;
using Moq;
using System.Linq.Expressions;

public class UnitTest1
{
    [Fact]
    public async void Test1()
    {
        var mockMongo = new Mock<IMongoOperationDal<UserDto>>();

        mockMongo.Setup(s => s.FindOneAsync(It.IsAny<Expression<Func<UserDto,bool>>>())).Returns(Task.FromResult(new UserDto { Id = "1", Email = "abhi@gmail.com", Password = "1234" }));

        var service = new UserDal(mockMongo.Object);

        UserDto data = await service.findUserByEmail("abhi@gmail.com");

        Assert.NotNull(data);
        Assert.Equal("1", data.Id);
        
    }
}
