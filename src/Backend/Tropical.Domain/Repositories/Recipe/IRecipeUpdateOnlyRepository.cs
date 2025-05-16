namespace Tropical.Domain.Repositories.Recipe
{
    public interface IRecipeUpdateOnlyRepository
    {
        //tive que remover o public do método getbyId, já que um tinha asnotracking e o outro atualizaria a entity
        // já que  ela tem a mesma assinatura da readonlyrepository , então tive que modificar a assinatura no repository
        Task<Entities.Recipe?> GetById(Entities.User user, long recipeId);
        void Update(Domain.Entities.Recipe recipe);
    }
}
