using RecipeManagement.Core;
using Xunit;

namespace RecipeManagement.Tests;

public class PartBSavedTests
{
    private static RecipeManager MakeManager()
    {
        return new RecipeManager(new[]
        {
            new Recipe { Id = 1, Title = "Pasta" },
            new Recipe { Id = 2, Title = "Chicken" },
            new Recipe { Id = 3, Title = "Beef" }
        });
    }

    [Fact]
    public void AddSavedRecipe_AddsId()
    {
        var m = MakeManager();
        Assert.True(m.AddSavedRecipe(1));
        Assert.True(m.IsRecipeSaved(1));
    }

    [Fact]
    public void AddSavedRecipe_Duplicate_ReturnsFalse()
    {
        var m = MakeManager();
        Assert.True(m.AddSavedRecipe(1));
        Assert.False(m.AddSavedRecipe(1));
    }

    [Fact]
    public void AddSavedRecipe_NonExistentRecipe_ReturnsFalse()
    {
        var m = MakeManager();
        Assert.False(m.AddSavedRecipe(999));
    }

    [Fact]
    public void RemoveSavedRecipe_RemovesId()
    {
        var m = MakeManager();
        m.AddSavedRecipe(1);
        Assert.True(m.RemoveSavedRecipe(1));
        Assert.False(m.IsRecipeSaved(1));
    }

    [Fact]
    public void RemoveSavedRecipe_NotSaved_ReturnsFalse()
    {
        var m = MakeManager();
        Assert.False(m.RemoveSavedRecipe(2));
    }

    [Fact]
    public void IsRecipeSaved_ChecksCorrectly()
    {
        var m = MakeManager();
        m.AddSavedRecipe(2);
        Assert.True(m.IsRecipeSaved(2));
        Assert.False(m.IsRecipeSaved(3));
    }

    [Fact]
    public void GetSavedRecipes_ReturnsAllSaved()
    {
        var m = MakeManager();
        m.AddSavedRecipe(1);
        m.AddSavedRecipe(3);
        var result = m.GetSavedRecipes();
        Assert.Equal(2, result.Count);
        Assert.Contains(1, result);
        Assert.Contains(3, result);
    }

    [Fact]
    public void GetSavedRecipes_Empty_ReturnsEmpty()
    {
        var m = MakeManager();
        Assert.Empty(m.GetSavedRecipes());
    }
}
