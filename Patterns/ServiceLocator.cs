using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A generic service locator pattern that stores a registry of objects of any type
/// Only holds one type of each object.
/// </summary>
public static class ServiceLocator
{
    private const bool debugMode = false;
    
    private static readonly Dictionary<Type, object> services = new();
    public static Dictionary<Type, object> GetServices() => services;

    /// <summary>
    /// Registers a service to the service locator
    /// Fails if service already exists
    /// </summary>
    /// <param name="service">Service of type object</param>
    /// <typeparam name="T">Any Type</typeparam>
    public static void Register<T>(T service)
    {
        services[typeof(T)] = service;
        if(debugMode) Debug.Log($"Registered service: {typeof(T)}");
    }

    /// <summary>
    /// Deregisters a service from the service locator
    /// Fails if service dosent exist
    /// </summary>
    /// <param name="service"></param>
    /// <typeparam name="T"></typeparam>
    public static void Deregister<T>()
    {
        if(services.ContainsKey(typeof(T))) services.Remove(typeof(T));
        if(debugMode) Debug.Log($"Deregistered service: {typeof(T)}");
    }

    /// <summary>
    /// Gets the service of type T from the service locator
    /// </summary>
    /// <typeparam name="T">Any type</typeparam>
    /// <returns>The registered service</returns>
    public static T Get<T>()
    {
        if(debugMode) Debug.Log($"Getting service: {typeof(T)}");
        return (T)services[typeof(T)];
    } 

    /// <summary>
    /// Trys to get the service
    /// </summary>
    /// <param name="service">Service of type object</param>
    /// <typeparam name="T">Any Type</typeparam>
    /// <returns>Boolean if service was found</returns>
    public static bool TryGet<T>(out T service)
    {
        if (services.TryGetValue(typeof(T), out var obj))
        {
            service = (T)obj;
            if(debugMode) Debug.Log($"Found service: {typeof(T)}");
            return true;
        }
        if(debugMode) Debug.Log($"Could not find service: {typeof(T)}");
        service = default;
        return false;
    }
}