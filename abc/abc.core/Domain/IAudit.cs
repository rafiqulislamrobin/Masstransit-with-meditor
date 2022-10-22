using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc.core.Domain
{
    public interface IAudit
    {
        DateTimeOffset CreatedOn { get; }
        DateTimeOffset? ModifiedOn { get; }

        void SetCreatedOn(DateTimeOffset dateTimeOffset);

        void SetModifiedOn(DateTimeOffset dateTimeOffset);
    }
}
