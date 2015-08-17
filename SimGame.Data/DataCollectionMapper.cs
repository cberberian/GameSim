using System.Collections.Generic;
using System.Linq;
using SimGame.Data.Interface;
using SimGame.Domain;

namespace SimGame.Data
{
    public static class DataCollectionMapper
    {
        public static void MapCollection<T>(ICollection<T> sourceCollection, ICollection<T> destinationCollection, CollectionMapperOptions options)
            where T: DomainObject
        {
            if (options.Add)
            {
                foreach (var src in sourceCollection.Where(x => x.Id == 0))
                {
                    destinationCollection.Add(src);
                }
            }
            if (options.Update)
            {
                foreach (var src in sourceCollection.Where(x=>x.Id > 0))
                {
                    var dest = destinationCollection.FirstOrDefault(x => x.Id == src.Id);
                    if (dest != null)
                        options.Context.SetValues(dest, src);
                }
            }
            if (options.Delete)
            {
                foreach (var dest in destinationCollection.Where(dest => sourceCollection.All(x=>x.Id != dest.Id) && dest.Id > 0))
                {
                    dest.Deleted = true;
                }

                var deleted = destinationCollection.Where(x => x.Deleted).ToArray();
                foreach(var d in deleted)
                {
                    destinationCollection.Remove(d);
                    options.Context.Delete(d);
                }
            }
        }
    }

    public class CollectionMapperOptions
    {
        public CollectionMapperOptions()
        {
            Add = true;
            Update = true;
            Delete = true;
        }

        public bool Add { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
        public IGameSimContext Context { get; set; }
        
    }
}