using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using HarborManager.Contracts;

namespace HarborManager.Logic
{
    public class Boats : IBoats
    {

        private IHarborDbContext _harborDbContext;
        public Boats(IHarborDbContext harborDbContext)
        {
            _harborDbContext = harborDbContext;
        }

        public IEnumerable<BoatDTO> GetAll()
        {
            return _harborDbContext.Boats.ToList();
        }

        public BoatDTO GetById(int id)
        {
            var item = _harborDbContext.Boats.FirstOrDefault(t => t.Id == id);

            return item;
        }

        public int Add(BoatDTO boat)
        {
            if (boat == null)
            {
                throw new System.ArgumentNullException();
            }

            _harborDbContext.Boats.Add(boat);
            _harborDbContext.Complete();

            return boat.Id;
        }
    }
}
