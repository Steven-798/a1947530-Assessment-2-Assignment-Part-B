# Design

Complete this document for **Part B**.

## 1. Saved recipe collection

Name the C# collection you selected for the saved/favourite feature and explain why it suits add, remove, duplicate prevention and membership checks.

## 2. Integration

In a short paragraph, explain how the new Part B features use the existing Part A `RecipeManager` and recipe catalogue.

## 3. Basic complexity

| Operation | Structure | Expected complexity | Reason |

| Lookup recipe by ID | Dictionary | Average O(1) | Hash-based key lookup. |
| Traverse cooking plan | LinkedList | O(n) | Each planned recipe may need to be visited. |
| Complete next instruction | Queue | O(1) | The item at the front is removed. |
| LINQ title/ingredient search | Recipe collection | O(n) | Each recipe may need to be inspected. |
| Check whether a recipe is saved | Your chosen collection | | Explain how your collection performs membership checks. |

# Design

Complete this document for **Part B**.

## 1. Saved recipe collection

I used `HashSet<int>` to store the saved recipe IDs.

It fits the feature well:
- `Add` returns false if the ID is already there, so duplicates are blocked for free.
- `Remove` and `Contains` are both O(1) on average, so checking and deleting are fast.
- I only store IDs, not whole recipes, so there's no duplicated data.

## 2. Integration

The new methods all live inside the existing `RecipeManager` class from Part A. `SearchByTitle` and `SearchByIngredient` just filter the recipes already held in the `_recipes` dictionary with LINQ. `GetHighestProteinRecipes` reads the `ProteinG` value from each recipe's `Nutrition` object and sorts it. The saved-recipe methods use a new private `HashSet<int>` field that only keeps IDs, so the catalogue itself is never touched. All 31 Part A tests still pass unchanged.

## 3. Basic complexity

| Operation | Structure | Expected complexity | Reason |

| Lookup recipe by ID | Dictionary | Average O(1) | Hash-based key lookup. |
| Traverse cooking plan | LinkedList | O(n) | Each planned recipe may need to be visited. |
| Complete next instruction | Queue | O(1) | The item at the front is removed. |
| LINQ title/ingredient search | Recipe collection | O(n) | Each recipe may need to be inspected. |
| Check whether a recipe is saved | HashSet\<int\> | Average O(1) | Hash-based membership check. |
| Add a recipe to saved | HashSet\<int\> | Average O(1) | Hash-based insert, duplicates rejected. |
| Remove a saved recipe | HashSet\<int\> | Average O(1) | Hash-based delete. |
| Get highest protein recipes | Recipe collection | O(n log n) | Sorted before taking the top N. |
