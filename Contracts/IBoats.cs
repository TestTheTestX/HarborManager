using System;
using System.Collections.Generic;
using System.Text;

namespace HarborManager.Contracts
{
    public interface IBoats
    {
        IEnumerable<BoatDTO> GetAll();
        BoatDTO GetById(int booking);
        int Add(BoatDTO booking);
    }
}
