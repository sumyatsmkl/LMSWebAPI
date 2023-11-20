using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO;

 public record SystemCodeValueDTO
{

    public Guid CodeValueId { get; init; }
    public string Code { get; init; }
    public string CodeValue { get; init; } = null!;
    public int CodeTypeId { get; init; }
    public int Status { get; init; }
    public bool IsDefault { get; init; }
    public string Remarks { get; init; }
    public int? CodeOrder { get; init; }
}
