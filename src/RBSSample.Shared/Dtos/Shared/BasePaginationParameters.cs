using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBSSample.Shared.Dtos.Shared;
public abstract class BasePaginationParameters
{
    internal int MaxPageSize { get; } = 20;
    internal int DefaultPageSize { get; set; } = 10;

    public int PageNumber { get; set; } = 1;

    public int PageSize
    {
        get
        {
            return DefaultPageSize;
        }
        set
        {
            DefaultPageSize = value > MaxPageSize ? MaxPageSize : value;
        }
    }
}
