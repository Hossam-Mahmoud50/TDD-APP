using FormualApp.Api.Domains;

namespace formulaApp.UnitTests.Fixtures;

public class FansFixture
{
    public static List<Fan> GetFans() =>
    [
        new Fan() { Id = 1, Email = "hossam@gmail.com", Name = "hossam" },
        new Fan() { Id = 2, Email = "Ahmed@gmail.com", Name = "Ahmed" },
    ];
}