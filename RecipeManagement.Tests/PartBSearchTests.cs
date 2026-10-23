using RecipeManagement.Core;
using Xunit;

namespace RecipeManagement.Tests;

public class PartBSearchTests
{
    private static RecipeManager MakeManager()
    {
        return new RecipeManager(new[]
        {
            new Recipe { Id = 1, Title = "Pasta Carbonara",
                Ingredients = new() { "noodles", "egg", "bacon" } },
            new Recipe { Id = 2, Title = "Chicken Salad",
                Ingredients = new() { "chicken", "lettuce", "egg" } },
            new Recipe { Id = 3, Title = "Beef Stew",
                Ingredients = new() { "beef", "potato", "carrot" } }
        });
    }

    [Fact]
    public void SearchByTitle_FindsPartialMatch()
    {
        var m = MakeManager();
        var result = m.SearchByTitle("pasta");
        Assert.Single(result);
        Assert.Equal("Pasta Carbonara", result[0].Title);
    }

    [Fact]
    public void SearchByTitle_CaseInsensitive()
    {
        var m = MakeManager();
        var result = m.SearchByTitle("CHICKEN");
        Assert.Single(result);
    }

    [Fact]
    public void SearchByTitle_Empty_ReturnsAll()
    {
        var m = MakeManager();
        var result = m.SearchByTitle("");
        Assert.Equal(3, result.Count);
    }

    [Fact]
    public void SearchByTitle_NoMatch_ReturnsEmpty()
    {
        var m = MakeManager();
        var result = m.SearchByTitle("xyz");
        Assert.Empty(result);
    }

    [Fact]
    public void SearchByIngredient_FindsMatch()
    {
        var m = MakeManager();
        var result = m.SearchByIngredient("egg");
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public void SearchByIngredient_CaseInsensitive()
    {
        var m = MakeManager();
        var result = m.SearchByIngredient("BEEF");
        Assert.Single(result);
    }

    [Fact]
    public void SearchByIngredient_Empty_ReturnsAll()
    {
        var m = MakeManager();
        var result = m.SearchByIngredient("");
        Assert.Equal(3, result.Count);
    }
}
