using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TeamFoundation.Common.Repositories
{
    public interface IWriteableRepository
    {
        void CreateRelation(object targetResource, object resourceToBeAdded);
        void Remove(object entity);
        void Save(object entity);
    }
}
