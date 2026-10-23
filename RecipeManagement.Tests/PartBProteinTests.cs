using RecipeManagement.Core;
using Xunit;

namespace RecipeManagement.Tests;

public class PartBProteinTests
{
    private static RecipeManager MakeManager()
    {
        return new RecipeManager(new[]
        {
            new Recipe { Id = 1, Title = "Chicken Breast",
                Nutrition = new NutritionInfo { ProteinG = 31 } },
            new Recipe { Id = 2, Title = "Rice",
                Nutrition = new NutritionInfo { ProteinG = 2.7 } },
            new Recipe { Id = 3, Title = "Beef Steak",
                Nutrition = new NutritionInfo { ProteinG = 26 } },
            new Recipe { Id = 4, Title = "Salad" }
        });
    }

    [Fact]
    public void GetHighestProtein_ReturnsTopTwo()
    {
        var m = MakeManager();
        var result = m.GetHighestProteinRecipes(2);
        Assert.Equal(2, result.Count);
        Assert.Equal("Chicken Breast", result[0].Title);
        Assert.Equal("Beef Steak", result[1].Title);
    }

    [Fact]
    public void GetHighestProtein_CountLargerThanTotal_ReturnsAll()
    {
        var m = MakeManager();
        var result = m.GetHighestProteinRecipes(10);
        Assert.Equal(4, result.Count);
    }

    [Fact]
    public void GetHighestProtein_ZeroOrNegative_ReturnsEmpty()
    {
        var m = MakeManager();
        Assert.Empty(m.GetHighestProteinRecipes(0));
        Assert.Empty(m.GetHighestProteinRecipes(-5));
    }

    [Fact]
    public void GetHighestProtein_NullNutrition_TreatedAsZero()
    {
        var m = MakeManager();
        var result = m.GetHighestProteinRecipes(4);
        Assert.Equal("Salad", result[3].Title);
    }
}
