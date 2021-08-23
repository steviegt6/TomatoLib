using System;
using System.Reflection;
using TomatoLib.Core.Reflection;
using TomatoLib.Core.Utilities.Reflection;

namespace TomatoLib.Common.Utilities.Extensions
{
    public static class ReflectionExtensions
    {
        /// <inheritdoc cref="IReflectionCache.InvokeUnderlyingMethod(FieldInfo, string, object, object[])"/>.
        public static object InvokeUnderlyingMethod(this FieldInfo info, string methodName, object instance,
            params object[] parameters) =>
            ReflectionCache.Instance.InvokeUnderlyingMethod(info, methodName, instance, parameters);

        /// <inheritdoc cref="IReflectionCache.InvokeUnderlyingMethod(PropertyInfo, string, object, object[])"/>.
        public static object InvokeUnderlyingMethod(this PropertyInfo info, string methodName, object instance,
            params object[] parameters) =>
            ReflectionCache.Instance.InvokeUnderlyingMethod(info, methodName, instance, parameters);

        /// <inheritdoc cref="IReflectionCache.GetCachedField"/>.
        public static FieldInfo GetCachedField(this Type type, string key) =>
            ReflectionCache.Instance.GetCachedField(type, key);

        /// <inheritdoc cref="IReflectionCache.GetCachedProperty"/>.
        public static PropertyInfo GetCachedProperty(this Type type, string key) =>
            ReflectionCache.Instance.GetCachedProperty(type, key);

        /// <inheritdoc cref="IReflectionCache.GetCachedMethod"/>.
        public static MethodInfo GetCachedMethod(this Type type, string key) =>
            ReflectionCache.Instance.GetCachedMethod(type, key);

        /// <inheritdoc cref="IReflectionCache.GetCachedConstructor"/>.
        public static ConstructorInfo GetCachedConstructor(this Type type, params Type[] identity) =>
            ReflectionCache.Instance.GetCachedConstructor(type, identity);

        /// <inheritdoc cref="IReflectionCache.GetCachedType"/>.
        public static Type GetCachedType(this Assembly assembly, string key) =>
            ReflectionCache.Instance.GetCachedType(assembly, key);

        /// <inheritdoc cref="IReflectionCache.GetCachedNestedType"/>.
        public static Type GetCachedNestedType(this Type type, string key) =>
            ReflectionCache.Instance.GetCachedNestedType(type, key);

        /// <inheritdoc cref="IReflectionCache.ReplaceInfoInstance(FieldInfo, object, object)"/>.
        public static void ReplaceInfoInstance(this FieldInfo info, object instance = null,
            object replacementInstance = null) =>
            ReflectionCache.Instance.ReplaceInfoInstance(info, instance, replacementInstance);

        /// <inheritdoc cref="IReflectionCache.ReplaceInfoInstance(PropertyInfo, object, object)"/>.
        public static void ReplaceInfoInstance(this PropertyInfo info, object instance = null,
            object replacementInstance = null) =>
            ReflectionCache.Instance.ReplaceInfoInstance(info, instance, replacementInstance);

        /// <inheritdoc cref="IReflectionCache.GetValue{TReturn}(FieldInfo, object)"/>.
        public static TReturn GetValue<TReturn>(this FieldInfo info, object fieldInstance = null) =>
            ReflectionCache.Instance.GetValue<TReturn>(info, fieldInstance);

        /// <inheritdoc cref="IReflectionCache.GetValue{TReturn}(PropertyInfo, object)"/>.
        public static TReturn GetValue<TReturn>(this PropertyInfo info, object fieldInstance = null) =>
            ReflectionCache.Instance.GetValue<TReturn>(info, fieldInstance);

        /// <inheritdoc cref="IReflectionCache.SetValue(FieldInfo, object, object)"/>.
        public static void SetValue(this FieldInfo info, object instance = null, object fieldValue = null) =>
            ReflectionCache.Instance.SetValue(info, instance, fieldValue);

        /// <inheritdoc cref="IReflectionCache.SetValue(PropertyInfo, object, object)"/>.
        public static void SetValue(this PropertyInfo info, object instance = null, object fieldValue = null) =>
            ReflectionCache.Instance.SetValue(info, instance, fieldValue);

        /// <inheritdoc cref="IReflectionCache.GetFieldValue{TType, TReturn}"/>.
        public static TReturn GetFieldValue<TType, TReturn>(this TType instance, string field) =>
            ReflectionCache.Instance.GetFieldValue<TType, TReturn>(instance, field);

        /// <inheritdoc cref="IReflectionCache.GetPropertyValue{TType, TReturn}"/>.
        public static TReturn GetPropertyValue<TType, TReturn>(this TType instance, string property) =>
            ReflectionCache.Instance.GetPropertyValue<TType, TReturn>(instance, property);

        /// <inheritdoc cref="IReflectionCache.SetFieldValue{TType}"/>.
        public static void SetFieldValue<TType>(this TType instance, string field, object fieldValue = null) =>
            ReflectionCache.Instance.SetFieldValue(instance, field, fieldValue);

        /// <inheritdoc cref="IReflectionCache.SetPropertyValue{TType}"/>.
        public static void SetPropertyValue<TType>(this TType instance, string property, object fieldValue = null) =>
            ReflectionCache.Instance.SetPropertyValue(instance, property, fieldValue);
    }
}