using System.Collections.Generic;
using System.Threading.Tasks;
using Bookinist.DAL.Entityes;

namespace Bookinist.Services.Interfaces
{
    interface ISalesService
    {
        IEnumerable<Deal> Deals { get; }

        Task<Deal> MakeADeal(string BookName, Seller Seller, Buyer Buyer, decimal Price);
    }
}
