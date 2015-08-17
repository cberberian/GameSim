using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace SimGame.Domain
{
    public abstract class DomainObjectCollectionMapper
    {
        public static ICollection<TD> MapCollection<TS, TD>(ICollection<TS> sourceCollection, ICollection<TD> destinationCollection)
            where TS : DomainObject
            where TD : DomainObject, new()
        {
            if (destinationCollection == null)
                destinationCollection = new List<TD>();

            foreach (var src in sourceCollection)
            {
                var src1 = src;
                var dest = destinationCollection.FirstOrDefault(x => x.Id == src1.Id);
                if (dest == null)
                {
                    dest = new TD();
                    destinationCollection.Add(dest);
                }
                Mapper.Map(src1, dest);
            }
            foreach (var dest1 in  destinationCollection)
            {
                if (sourceCollection.All(x => x.Id != dest1.Id))
                    dest1.Deleted = true;
            }
            return destinationCollection;
        }
        
    }
}