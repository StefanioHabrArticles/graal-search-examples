using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    public class Catalogue<T> : IEnumerable<CatalogueEvent>
    {
        private readonly List<CatalogueEvent> _catalogueEvents = new();

        public S Reduce<S, G>(Func<T, S> map, G group = default)
            where G : struct, IGroup<S>
            => _catalogueEvents.Select(t =>
                t switch
                {
                    Add<T> add => map(add.Data),
                    Remove<T> rm => group.Inverse(map(rm.Data)),
                    Nothing => group.Zero,
                    _ => default
                }
            ).Sum<S, G>();

        public Catalogue<T> Add(T item)
        {
            _catalogueEvents.Add(new Add<T>(item));
            return this;
        }

        public Catalogue<T> Remove(T item)
        {
            _catalogueEvents.Add(new Remove<T>(item));
            return this;
        }

        public Catalogue<T> Pass()
        {
            _catalogueEvents.Add(new Nothing());
            return this;
        }

        public IEnumerator<CatalogueEvent> GetEnumerator() =>
            _catalogueEvents.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();
    }

    public record CatalogueEvent;

    public record Add<T>(T Data) : CatalogueEvent;

    public record Remove<T>(T Data) : CatalogueEvent;

    public record Nothing : CatalogueEvent;
    
    public static class CatalogueExtensions
    {
        public static List<T> Collect<T>(this Catalogue<T> catalogue) =>
            catalogue.Reduce<PairedList<T>, PairedListGroup<T>>(
                x => new PairedList<T>(x)
            ).ToList();
    }
}