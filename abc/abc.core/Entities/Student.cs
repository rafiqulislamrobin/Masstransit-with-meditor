using abc.core.Domain;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc.core.Entities
{
    public class Student : Entity<Guid>, IAudit
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public Student(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        #region Audit_Properties

        public DateTimeOffset CreatedOn { get; private set; }
        public DateTimeOffset? ModifiedOn { get; private set; }

        public void SetCreatedOn(DateTimeOffset dateTimeOffset)
        {
            CreatedOn = dateTimeOffset;
        }

        public void SetModifiedOn(DateTimeOffset dateTimeOffset)
        {
            ModifiedOn = dateTimeOffset;
        }

        #endregion Audit_Properties
    }
}
