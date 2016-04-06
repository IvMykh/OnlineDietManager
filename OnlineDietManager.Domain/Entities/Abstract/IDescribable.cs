using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineDietManager.Domain.Entities.Abstract
{
    public interface IDescribable
    {
        string Description { get; }
    }
}
