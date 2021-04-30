using AutoMapper;
using RecipeBLL.DTOS;
using RecipeDAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBLL.Repository.User
{
    public class UserRepository : RepositoryClass<UserDTO, RecipeDAL.DAL.User>, IUserRepository
    {
        public UserRepository(RecipeContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {

        }

    }
}
