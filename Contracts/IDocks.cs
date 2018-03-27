using System;
using System.Collections.Generic;
using System.Text;

namespace HarborManager.Contracts
{
    public interface IDocks
    {
        IEnumerable<DockDTO> GetAll();
        DockDTO GetById(int booking);
        int Add(DockDTO booking);
    }
}
