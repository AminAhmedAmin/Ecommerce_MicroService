using MediatR;
using Ordering.Application.Responses;
using System.Collections.Generic;

namespace Ordering.Application.Queries
{
    public class GetOrderListQuery : IRequest<List<OrderResponse>>
    {
        public string UserName { get; set; }

        public GetOrderListQuery(string userName)
        {
            UserName = userName;
        }
    }
}
