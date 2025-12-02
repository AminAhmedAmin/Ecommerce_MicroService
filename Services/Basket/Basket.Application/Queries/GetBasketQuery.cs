using Basket.Application.Responses;
using MediatR;

namespace Basket.Application.Queries
{
    public class GetBasketQuery : IRequest<ShoppingCartResponse>
    {
        public string UserName { get; set; }

        public GetBasketQuery(string userName)
        {
            UserName = userName;
        }
    }
}
