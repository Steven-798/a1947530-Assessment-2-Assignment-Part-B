# Recipe Management System

A C# console application that manages a recipe catalogue. Part of the Software Development Practice assignment (2026 Sem 2).

## Project structure

- `RecipeManagement.Core` – the `RecipeManager` class and models
- `RecipeManagement.Application` – console entry point
- `RecipeManagement.Tests` – xUnit unit tests
- `data/` – recipe data in JSON format

## Features

### Part A – collections

- **Dictionary** – stores recipes by ID for fast lookup
- **List** – shopping list
- **LinkedList** – cooking plan, keeps the order of steps
- **Stack** – undo history for deleted recipes
- **Queue** – cooking instructions in order

### Part B – new features

- `SearchByTitle` – find recipes by title, case-insensitive
- `SearchByIngredient` – find recipes that contain an ingredient
- `GetHighestProteinRecipes` – top N recipes by protein per serving
- Saved recipes – add, remove, check and list favourites, stored in a `HashSet<int>`

## Design

See `Design.md` for the collection choices and complexity analysis.

## Running the tests

```bash
dotnet test
```

All 53 tests pass (31 from Part A, 22 new in Part B).
