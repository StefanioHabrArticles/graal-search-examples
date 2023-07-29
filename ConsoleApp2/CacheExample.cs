namespace ConsoleApp2;

class Cache
{
    public void Set<TResource>(TResource resource)
        where TResource : IResource<TResource>
    {
        var cacheKey = TResource.CacheKey;
        // ...
    }
}

interface IResource
{
    string CacheKey { get; }
}

[CacheKey(nameof(MyResource))]
record MyResource;

class CacheKeyAttribute : Attribute
{
    public string Value { get; }

    public CacheKeyAttribute(string value) =>
        Value = value;
}

abstract class Resource<TKeyProvider>
    where TKeyProvider :
    KeyProvider, new()
{
    public string CacheKey =>
        new TKeyProvider().Key;
}

abstract class KeyProvider
{
    public abstract string Key { get; }
}

    interface IResource<TResource>
        where TResource : IResource<TResource>
    {
        static abstract string CacheKey { get; }
    }

static class Test
{
    static void TestMethod()
    {
        //new Cache().Set(1);
        IResource res = new Res();
        string s = res.CacheKey;
        new MyResource();
        string ss = new CacheKeyAttribute("").Value;

        Resource<Provider> f = null;
        string saa = f.CacheKey;
    }

    class Res : IResource
    {
        public string CacheKey { get; }
    }

    class Provider : KeyProvider
    {
        public override string Key { get; }
    }

    class MyRes : Resource<Provider>
    {
        public static string CacheKey { get; }
    }
}