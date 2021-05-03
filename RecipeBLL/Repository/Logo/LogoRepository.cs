using RecipeBLL.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeDAL.DAL;
using AutoMapper;
using RecipeDAL.Context;

namespace RecipeBLL.Repository.Logo
{
    public class LogoRepository: RepositoryClass<LogoDTO, RecipeDAL.DAL.Logo>,ILogoRepository
    {
        public LogoRepository(RecipeContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {

        }
    }
}
