using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TomatoLib.Core.Utilities.Extensions;
using TomatoLib.Core.Utilities.Reflection;

namespace TomatoLib.Core.Reflection
{
    /// <summary>
    ///     Basic implementation of <see cref="IReflectionCache"/>.
    /// </summary>
    public class ReflectionCache : IReflectionCache
    {
        private readonly Dictionary<ReflectionType, Dictionary<string, object>> Cache;

        public static IReflectionCache Instance { get; set; }

        public ReflectionCache()
        {
            Cache = new Dictionary<ReflectionType, Dictionary<string, object>>();
            GenerateEmptyCache(ref Cache, ReflectionType.Field);
            GenerateEmptyCache(ref Cache, ReflectionType.Property);
            GenerateEmptyCache(ref Cache, ReflectionType.Method);
            GenerateEmptyCache(ref Cache, ReflectionType.Constructor);
            GenerateEmptyCache(ref Cache, ReflectionType.Type);
            GenerateEmptyCache(ref Cache, ReflectionType.NestedType);
        }

        private static void GenerateEmptyCache(ref Dictionary<ReflectionType, Dictionary<string, object>> dictionary,
            ReflectionType reflectionType) => dictionary.Add(reflectionType, new Dictionary<string, object>());

        public object InvokeUnderlyingMethod(FieldInfo info, string methodName, object instance,
            params object[] parameters) => info.FieldType.GetCachedMethod(methodName).Invoke(instance, parameters);

        public object InvokeUnderlyingMethod(PropertyInfo info, string methodName, object instance,
            params object[] parameters) =>
            info.PropertyType.GetCachedMethod(methodName).Invoke(instance, parameters);

        public FieldInfo GetCachedField(Type type, string key) => RetrieveFromCache(ReflectionType.Field,
            GetUniqueFieldKey(type, key), () => type.GetField(key, ReflectionHelper.UniversalFlags));

        public PropertyInfo GetCachedProperty(Type type, string key) => RetrieveFromCache(ReflectionType.Property,
            GetUniquePropertyKey(type, key), () => type.GetProperty(key, ReflectionHelper.UniversalFlags));

        public MethodInfo GetCachedMethod(Type type, string key) => RetrieveFromCache(ReflectionType.Method,
            GetUniqueMethodKey(type, key), () => type.GetMethod(key, ReflectionHelper.UniversalFlags));

        public ConstructorInfo GetCachedConstructor(Type type, params Type[] identity) => RetrieveFromCache(
            ReflectionType.Constructor, GetUniqueConstructorKey(type, identity),
            () => type.GetConstructor(ReflectionHelper.UniversalFlags, null, identity, null));

        public Type GetCachedType(Assembly assembly, string key) => RetrieveFromCache(ReflectionType.Type,
            GetUniqueTypeKey(assembly, key), () => assembly.GetType(key));

        public Type GetCachedNestedType(Type type, string key) => RetrieveFromCache(ReflectionType.Type,
            GetUniqueNestedTypeKey(type, key), () => type.GetNestedType(key, ReflectionHelper.UniversalFlags));

        public string GetUniqueFieldKey(Type type, string key) => $"{type.FullName}->{key}";

        public string GetUniquePropertyKey(Type type, string key) => $"{type.FullName}->{key}";

        public string GetUniqueMethodKey(Type type, string key) => $"{type.FullName}::{key}";

        public string GetUniqueConstructorKey(Type type, params Type[] identity)
        {
            string sewnTypes = string.Join(",", identity.Select(x => x.FullName));

            return $"{type.FullName}.ctor:{{{sewnTypes}}}";
        }

        public string GetUniqueTypeKey(Assembly assembly, string key) => $"{assembly.FullName}.{key}";

        public string GetUniqueNestedTypeKey(Type type, string key) => $"{type.FullName}.{key}";

        public TReturn RetrieveFromCache<TReturn>(ReflectionType refType, string key, Func<TReturn> fallback)
        {
            if (Cache[refType].TryGetValue(key, out object found))
                return (TReturn)found;

            return (TReturn)(Cache[refType][key] = fallback());
        }

        public void ReplaceInfoInstance(FieldInfo info, object instance = null, object replacementInstance = null) =>
            info.SetValue(instance, replacementInstance ?? Activator.CreateInstance(info.FieldType));

        public void ReplaceInfoInstance(PropertyInfo info, object instance = null, object replacementInstance = null) =>
            info.SetValue(instance, replacementInstance ?? Activator.CreateInstance(info.PropertyType));

        public TReturn GetValue<TReturn>(FieldInfo info, object instance = null) =>
            (TReturn)info.GetValue(instance);

        public TReturn GetValue<TReturn>(PropertyInfo info, object instance = null) =>
            (TReturn)info.GetValue(instance);

        public void SetValue(FieldInfo info, object fieldInstance = null, object fieldValue = null) =>
            info.SetValue(fieldInstance, fieldValue);

        public void SetValue(PropertyInfo info, object fieldInstance = null, object fieldValue = null) =>
            info.SetValue(fieldInstance, fieldValue);

        public TReturn GetFieldValue<TType, TReturn>(TType instance, string field) =>
            (TReturn)typeof(TType).GetCachedField(field).GetValue(instance);

        public TReturn GetPropertyValue<TType, TReturn>(TType instance, string property) =>
            (TReturn)typeof(TType).GetCachedField(property).GetValue(instance);

        public void SetFieldValue<TType>(TType instance, string field, object fieldValue = null) =>
            typeof(TType).GetCachedField(field).SetValue(instance, fieldValue);

        public void SetPropertyValue<TType>(TType instance, string property, object fieldValue = null) =>
            typeof(TType).GetCachedField(property).SetValue(instance, fieldValue);
    }
}