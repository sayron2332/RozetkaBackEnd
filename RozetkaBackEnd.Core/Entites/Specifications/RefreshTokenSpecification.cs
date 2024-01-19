using Ardalis.Specification;
using RozetkaBackEnd.Core.Entites.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RozetkaBackEnd.Core.Entites.Specifications
{
    
        public static class RefreshTokenSpecification
        {
            public class GetRefreshToken : Specification<RefreshToken>
            {
                public GetRefreshToken(string refreshToken)
                {
                    Query.Where(t => t.Token == refreshToken);
                }
            }
        }
    
}
