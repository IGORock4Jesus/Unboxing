using System;
using System.Collections.Generic;
using System.Net;

namespace Unboxing;
internal static class ResourceRepository
{
    private class Repository<T> : Dictionary<string, T> where T : IDisposable;

    private class RepositoryMap : Dictionary<Type, Repository<IDisposable>>;

    private static readonly RepositoryMap Map = new();

    public static void Add<T>(string name, T resource) where T : IDisposable
    {
        var repository = GetRepository<T>();
        repository.Add(name, resource);
    }

    private static Repository<IDisposable> GetRepository<T>() where T : IDisposable
    {
        if (!Map.ContainsKey(typeof(T)))
        {
            Map.Add(typeof(T), new Repository<IDisposable>());
        }

        return Map[typeof(T)];
    }

    public static void Remove<T>(string name) where T : IDisposable
    {
        var repository = GetRepository<T>();
        var item = repository.GetValueOrDefault(name);
        if (item is not null)
        {
            item.Dispose();
            repository.Remove(name);
        }
    }

    public static void Clear<T>() where T : IDisposable
    {
        var repository = GetRepository<T>();
        foreach (var item in repository)
        {
            item.Value.Dispose();
        }
        repository.Clear();
    }

    public static bool Contains<T>(string name) where T : IDisposable
    {
        var repository = GetRepository<T>();
        return repository.ContainsKey(name);
    }

    public static T Get<T>(string name) where T : IDisposable
    {
        var repository = GetRepository<T>();
        return (T)repository[name];
    }
}
