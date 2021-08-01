using System;
using System.Reflection;

namespace TomatoLib.Core.Utilities.Reflection
{
    /// <summary>
    ///     Interface for caching reflection.
    /// </summary>
    public interface IReflectionCache
    {
        /// <summary>
        ///     Invokes the specified method defined in the type of the provided <see cref="FieldInfo"/>.
        /// </summary>
        object InvokeUnderlyingMethod(FieldInfo info, string methodName, object instance,
    params object[] parameters);

        /// <summary>
        ///     Invokes the specified method defined in the type of the provided <see cref="PropertyInfo"/>.
        /// </summary>
        object InvokeUnderlyingMethod(PropertyInfo info, string methodName, object instance,
            params object[] parameters);

        /// <summary>
        ///     Retrieves a <see cref="FieldInfo"/> from the cache. Caches if it has not been cached previously.
        /// </summary>
        FieldInfo GetCachedField(Type type, string key);

        /// <summary>
        ///     Retrieves a <see cref="PropertyInfo"/> from the cache. Caches if it has not been cached previously.
        /// </summary>
        PropertyInfo GetCachedProperty(Type type, string key);

        /// <summary>
        ///     Retrieves a <see cref="MethodInfo"/> from the cache. Caches if it has not been cached previously.
        /// </summary>
        MethodInfo GetCachedMethod(Type type, string key);

        /// <summary>
        ///     Retrieves a <see cref="ConstructorInfo"/> from the cache. Caches if it has not been cached previously.
        /// </summary>
        ConstructorInfo GetCachedConstructor(Type type, params Type[] identity);

        /// <summary>
        ///     Retrieves a <see cref="Type"/> from the cache. Caches if it has not been cached previously.
        /// </summary>
        Type GetCachedType(Assembly assembly, string key);

        /// <summary>
        ///     Retrieves a nested <see cref="Type"/> from the cache. Caches if it has not been cached previously.
        /// </summary>
        Type GetCachedNestedType(Type type, string key);

        /// <summary>
        ///     Returns a unique key used for caching <see cref="FieldInfo"/><c>s</c>.
        /// </summary>
        string GetUniqueFieldKey(Type type, string key);

        /// <summary>
        ///     Returns a unique key used for caching <see cref="PropertyInfo"/><c>s</c>.
        /// </summary>
        string GetUniquePropertyKey(Type type, string key);

        /// <summary>
        ///     Returns a unique key used for caching <see cref="MethodInfo"/><c>s</c>.
        /// </summary>
        string GetUniqueMethodKey(Type type, string key);

        /// <summary>
        ///     Returns a unique key used for caching <see cref="ConstructorInfo"/><c>s</c>.
        /// </summary>
        string GetUniqueConstructorKey(Type type, params Type[] identity);

        /// <summary>
        ///     Returns a unique key used for caching <see cref="Type"/><c>s</c>.
        /// </summary>
        string GetUniqueTypeKey(Assembly assembly, string key);

        /// <summary>
        ///     Returns a unique key used for caching nested <see cref="Type"/><c>s</c>.
        /// </summary>
        string GetUniqueNestedTypeKey(Type type, string key);

        /// <summary>
        ///     Manually retrieve an object from the cache based on the <see cref="ReflectionType"/> provided. Caches the invoked <paramref name="fallback"/> return value if none is present.
        /// </summary>
        TReturn RetrieveFromCache<TReturn>(ReflectionType refType, string key, Func<TReturn> fallback);

        /// <summary>
        ///     Sets the value of the <see cref="FieldInfo"/>. Attempts to create a new instance using <see cref="Activator.CreateInstance{T}"/> if <paramref name="replacementInstance"/> is <c>null</c>.
        /// </summary>
        void ReplaceInfoInstance(FieldInfo info, object instance = null, object replacementInstance = null);

        /// <summary>
        ///     Sets the value of the <see cref="PropertyInfo"/>. Attempts to create a new instance using <see cref="Activator.CreateInstance{T}"/> if <paramref name="replacementInstance"/> is <c>null</c>.
        /// </summary>
        void ReplaceInfoInstance(PropertyInfo info, object instance = null, object replacementInstance = null);

        /// <summary>
        ///     Returns the value of a <see cref="FieldInfo"/>.
        /// </summary>
        TReturn GetValue<TReturn>(FieldInfo info, object instance = null);

        /// <summary>
        ///     Returns the value of a <see cref="PropertyInfo"/>.
        /// </summary>
        TReturn GetValue<TReturn>(PropertyInfo info, object instance = null);

        /// <summary>
        ///     Sets the value of a <see cref="FieldInfo"/>.
        /// </summary>
        void SetValue(FieldInfo info, object fieldInstance = null, object fieldValue = null);

        /// <summary>
        ///     Sets the value of a <see cref="PropertyInfo"/>.
        /// </summary>
        void SetValue(PropertyInfo info, object fieldInstance = null, object fieldValue = null);

        /// <summary>
        ///     Returns an instanced value by accessing an underlying <see cref="FieldInfo"/> that is part of the provided <typeparamref name="TType"/> <paramref name="instance"/>.
        /// </summary>
        TReturn GetFieldValue<TType, TReturn>(TType instance, string field);

        /// <summary>
        ///     Returns an instanced value by accessing an underlying <see cref="PropertyInfo"/> that is part of the provided <typeparamref name="TType"/> <paramref name="instance"/>.
        /// </summary>
        TReturn GetPropertyValue<TType, TReturn>(TType instance, string property);

        /// <summary>
        ///     Sets an instanced value by accessing an underlying <see cref="FieldInfo"/> that is part of the provided <typeparamref name="TType"/> <paramref name="instance"/>.
        /// </summary>
        void SetFieldValue<TType>(TType instance, string field, object fieldValue = null);

        /// <summary>
        ///     Sets an instanced value by accessing an underlying <see cref="PropertyInfo"/> that is part of the provided <typeparamref name="TType"/> <paramref name="instance"/>.
        /// </summary>
        void SetPropertyValue<TType>(TType instance, string property, object fieldValue = null);
    }
}