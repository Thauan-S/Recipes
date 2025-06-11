using Microsoft.EntityFrameworkCore;
using Tropical.Domain.Dtos;
using Tropical.Domain.Entities;
using Tropical.Domain.Repositories.Recipe;
using Tropical.Domain.Repositories.User;
using Microsoft.EntityFrameworkCore.Query;
using Tropical.Comunication.Pagination;

namespace Tropical.Infrastructure.Data.Repositories
{
    public class RecipeRepository : IRecipeWriteOnlyRepository, IRecipeReadOnlyRepository, IRecipeUpdateOnlyRepository
    {
        private readonly AppDbContext _appDbContext;

        public RecipeRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task Add(Recipe recipe)
        {
            await _appDbContext.Recipes.AddAsync(recipe);
        }

        public async Task<IList<Recipe>> Filter(User user, FilterRecipesDto filters)
        {
            
            var query = _appDbContext
                .Recipes
                .AsNoTracking()
                .Include(recipe => recipe.Ingredients) 
                .Where(r => r.UserId == user.Id && r.Active);
            if (filters.Difficulties.Any())
            {
                query = query.Where(r => r.Difficulty.HasValue
                && filters.Difficulties.Contains(r.Difficulty.Value));
            }
            if (filters.CookingTimes.Any())
            {
                query = query.Where(r => r.CookingTime.HasValue
                && filters.CookingTimes.Contains(r.CookingTime.Value));
            }
            if (filters.DishTypes.Any())
            {
                query = query.Where(r => r.DishTypes.Any(dishtype => filters.DishTypes.Contains(dishtype.Type)));
            }
            if (!string.IsNullOrWhiteSpace(filters.RecipeTitle_Ingredient))
            {
                query = query.Where(r =>
                    r.Title.Contains(filters.RecipeTitle_Ingredient) ||
                    r.Ingredients.Any(i =>
                    i.Item.Contains(filters.RecipeTitle_Ingredient)
                ));
            }
            return await query.ToListAsync();
        }

        async Task<Recipe?> IRecipeReadOnlyRepository.GetById(User user, long recipeId)
        {   // note que as duas GetById() não são públicas
            return await GetFullRecipe()   
                .AsNoTracking()             
                .FirstOrDefaultAsync(recipe => recipe.Active && recipe.UserId == user.Id && recipe.Id == recipeId);
        }

        public async Task Delete(long recipeId)
        {
            var recipe = await _appDbContext.Recipes.FindAsync(recipeId);
            _appDbContext.Recipes.Remove(recipe!);
        }

        public async Task<bool> RecipeExists(User user, long recipeId)
        {
            return await _appDbContext
               .Recipes
               .AsNoTracking()
               .AnyAsync(recipe => recipe.Active && recipe.Id == recipeId && recipe.UserId == user.Id);
        }

        async Task<Recipe?> IRecipeUpdateOnlyRepository.GetById(User user, long recipeId)
        {
            return await GetFullRecipe()
                 .FirstOrDefaultAsync(recipe => recipe.Active && recipe.UserId == user.Id && recipe.Id == recipeId);
        }

        public void Update(Recipe recipe)
        {
            _appDbContext.Recipes.Update(recipe);
        }

        private IIncludableQueryable<Recipe, IList<DishType>> GetFullRecipe()
        {   // já que utilizo essses includes nos dois métodos
            return _appDbContext
                 .Recipes
                 .Include(recipe => recipe.Ingredients)
                 .Include(recipe => recipe.Instructions)
                 .Include(recipe => recipe.DishTypes);
        }

        public async Task<IList<Recipe>> GetForDashBoard(User user,PaginationParameters parameters)
        {
            return await GetFullRecipe()
                 .AsNoTracking()
                 .Skip((parameters.Page-1)*parameters.Page)  
                 .Where(recipe => recipe.Active && recipe.UserId == user.Id)
                 .OrderByDescending(recipe => recipe.CreatedOn)
                 .Take(parameters.PageSize)
                 .ToListAsync();
        }
    }
}