using RecipeManagement.Core;
using Xunit;

namespace RecipeManagement.Tests;

public class PartBIntegrationTests
{
    private static RecipeManager MakeFullManager()
    {
        return new RecipeManager(new[]
        {
            new Recipe { Id = 1, Title = "Pasta Carbonara",
                Ingredients = new() { "noodles", "egg", "bacon" },
                Nutrition = new NutritionInfo { ProteinG = 18 } },
            new Recipe { Id = 2, Title = "Chicken Salad",
                Ingredients = new() { "chicken", "lettuce", "egg" },
                Nutrition = new NutritionInfo { ProteinG = 25 } },
            new Recipe { Id = 3, Title = "Beef Stew",
                Ingredients = new() { "beef", "potato", "carrot" },
                Nutrition = new NutritionInfo { ProteinG = 22 } },
            new Recipe { Id = 4, Title = "Vegan Bowl",
                Ingredients = new() { "tofu", "rice", "broccoli" },
                Nutrition = new NutritionInfo { ProteinG = 15 } }
        });
    }

    [Fact]
    public void FullFlow_Search_Save_GetProtein_WorksTogether()
    {
        var m = MakeFullManager();

        // 搜索含 egg 的食谱，应该有 2 个
        var found = m.SearchByIngredient("egg");
        Assert.Equal(2, found.Count);

        // 收藏搜索到的第一个
        Assert.True(m.AddSavedRecipe(found[0].Id));
        Assert.True(m.IsRecipeSaved(found[0].Id));

        // 蛋白质最高的食谱
        var top = m.GetHighestProteinRecipes(1);
        Assert.Equal("Chicken Salad", top[0].Title);

        // 收藏列表
        var saved = m.GetSavedRecipes();
        Assert.Contains(found[0].Id, saved);

        // 移除收藏
        Assert.True(m.RemoveSavedRecipe(found[0].Id));
        Assert.Empty(m.GetSavedRecipes());
    }

    [Fact]
    public void FullFlow_SearchByTitle_ThenSaveMultiple()
    {
        var m = MakeFullManager();

        // 搜索标题包含字母 a 的食谱
        var found = m.SearchByTitle("a");
        Assert.True(found.Count >= 3);

        // 全部收藏
        foreach (var r in found)
            m.AddSavedRecipe(r.Id);

        // 收藏数量 = 搜索结果数量
        Assert.Equal(found.Count, m.GetSavedRecipes().Count);

        // 重复收藏不增加
        Assert.False(m.AddSavedRecipe(found[0].Id));
        Assert.Equal(found.Count, m.GetSavedRecipes().Count);
    }

    [Fact]
    public void FullFlow_NullNutrition_DoesNotBreakSorting()
    {
        var m = MakeFullManager();
        m.AddRecipe(new Recipe { Id = 5, Title = "Water", Nutrition = null });

        var top = m.GetHighestProteinRecipes(5);
        Assert.Equal(5, top.Count);
        Assert.Equal("Water", top[4].Title); // 没营养信息的排最后
    }
}
