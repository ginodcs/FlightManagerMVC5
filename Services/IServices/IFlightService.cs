using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARQ.Maqueta.Entities;

namespace ARQ.Maqueta.Services
{
    public interface IFlightService
    {
        Flight Add(Flight flight);

        void Change(Flight flight);

        void Remove(int Id);

        List<Flight> Search(string searchValue);

        Flight Find(int id);

    }
}
