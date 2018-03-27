using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarborManager.Contracts;

namespace HarborManager.Logic
{
    public class Docks : IDocks
    {

        private IHarborDbContext _harborDbContext;
        public Docks(IHarborDbContext harborDbContext)
        {
            _harborDbContext = harborDbContext;
        }

        public IEnumerable<DockDTO> GetAll()
        {
            return _harborDbContext.Docks.ToList();
        }

        public DockDTO GetById(int booking)
        {
            throw new NotImplementedException();
        }

        public int Add(DockDTO booking)
        {
            throw new NotImplementedException();
        }
    }
}
