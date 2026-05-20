using System;
using System.Collections.Generic;

/// <summary>
/// A generic service locator pattern that stores a registry of objects of any type
/// Only holds one type of each object.
/// </summary>
public static class ServiceLocator
{
    private static readonly Dictionary<Type, object> services = new();

    /// <summary>
    /// Registers a service to the service locator
    /// Fails if service already exists
    /// </summary>
    /// <param name="service">Service of type object</param>
    /// <typeparam name="T">Any Type</typeparam>
    public static void Register<T>(T service) => services[typeof(T)] = service;
    
    /// <summary>
    /// Gets the service of type T from the service locator
    /// </summary>
    /// <typeparam name="T">Any type</typeparam>
    /// <returns>The registered service</returns>
    public static T Get<T>() => (T)services[typeof(T)];

    public static bool TryGet<T>(out T service)
    {
        if (services.TryGetValue(typeof(T), out var obj))
        {
            service = (T)obj;
            return true;
        }
        service = default;
        return false;
    }
}