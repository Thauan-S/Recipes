
using Tropical.Comunication.Pagination;
using Tropical.Comunication.Responses;

namespace Tropical.Application.UseCases.Recipe.DashBoard
{
    public interface IGetDashBoardUseCase
    {
        Task<ResponseRecipesJson> Execute(PaginationParameters parameters);
    }
}
