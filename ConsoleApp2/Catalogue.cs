using System.Collections;
using System.Numerics;

#pragma warning disable CS8631
#pragma warning disable CS8603

namespace ConsoleApp2
{
    public class Catalogue<T> : IEnumerable<CatalogueEvent>
    {
        private readonly List<CatalogueEvent> _catalogueEvents = new();

        public TGroup Reduce<TGroup>(Func<T, TGroup> map)
            where TGroup :
            IAdditionOperators<TGroup, TGroup, TGroup>,
            IAdditiveIdentity<TGroup, TGroup>,
            IUnaryNegationOperators<TGroup, TGroup> =>
            _catalogueEvents.Select(t =>
                t switch
                {
                    Add<T> add => map(add.Data),
                    Remove<T> rm => -map(rm.Data),
                    Nothing => TGroup.AdditiveIdentity,
                    _ => default
                }
            ).Sum();

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
            catalogue.Reduce<PairedEnumerable<List<T>, ListWrapper<T>, T>>(
                x => new List<T> { x }
            ).Merge();
    }
}