using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class RepositoryBase
    {
        protected IMapper Mapper { get; set; }

        public RepositoryBase()
        {
            var config = EntityDTOMapper.Configure();
            this.Mapper = config.CreateMapper();
        }
    }
}
