using System;
using System.Collections.Generic;
using System.Text;

namespace HarborManager.Contracts
{
    public class BoatDTO
    {
        public int Id { get; set; }
        public string Bezeichnung { get; set; }
        public bool CheckedIn { get; set; }
    }
}
