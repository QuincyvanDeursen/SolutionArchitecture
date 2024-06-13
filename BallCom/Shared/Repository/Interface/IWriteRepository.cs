using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.EventSourcing;

namespace Shared.Repository.Interface
{
    public interface IWriteRepository<in T> where T : Event
    {
        void Save(T @event);
        void Delete(T @event);
    }
}
