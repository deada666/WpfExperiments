using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;

namespace WpfExperiments.Extensions
{
    internal static class UnityExtensions
    {
        private static readonly InjectionMember[] _interception = new InjectionMember[] {
            new InterceptionBehavior<PolicyInjectionBehavior>(),
                        new Interceptor<InterfaceInterceptor>(),
                        new Interceptor<VirtualMethodInterceptor>()
        };

        public static IUnityContainer RegisterWithInterceptor<T>(this IUnityContainer container)
        {
            return container.RegisterType<T>(_interception);
        }

        public static IUnityContainer RegisterWithInterceptor<T>(this IUnityContainer container, LifetimeManager manager)
        {
            return container.RegisterType<T>(manager, _interception);
        }

        public static IUnityContainer RegisterSingleton<TFrom, TTo>(this IUnityContainer container)
             where TTo : TFrom
        {
            return container.RegisterType<TFrom, TTo>(new ContainerControlledLifetimeManager());
        }

        public static IUnityContainer RegisterSingleton<T>(this IUnityContainer container)
        {
            return container.RegisterType<T>(new ContainerControlledLifetimeManager());
        }

        public static IUnityContainer RegisterWithInterceptor<TFrom, TTo>(this IUnityContainer container)
             where TTo : TFrom
        {
            return container.RegisterType<TFrom, TTo>(_interception);
        }

        public static IUnityContainer RegisterWithInterceptor<TFrom, TTo>(this IUnityContainer container, LifetimeManager manager)
             where TTo : TFrom
        {
            return container.RegisterType<TFrom, TTo>(manager, _interception);
        }

        public static IUnityContainer RegisterTypesWithInterceptors(this IUnityContainer container, IEnumerable<Type> types, Func<Type, IEnumerable<Type>> getFromTypes = null, Func<Type, string> getName = null, Func<Type, LifetimeManager> getLifetimeManager = null)
        {
            return container.RegisterTypes(types, getFromTypes, getName, getLifetimeManager, t => _interception);
        }
    }
}
