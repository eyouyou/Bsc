﻿using System;
using System.Linq;
using Bsc.Dmtds.Core.Runtime.Dependency;
using Bsc.Dmtds.Ninject.InRequestScope;
using Ninject.Planning.Bindings;
using Ninject.Syntax;

namespace Bsc.Dmtds.Ninject
{
    public static class ContainerManagerExtensions
    {
        public static IBindingNamedWithOrOnSyntax<T> PerLifeStyle<T>(this IBindingWhenInNamedWithOrOnSyntax<T> binding, ComponentLifeStyle lifeStyle)
        {
            switch (lifeStyle)
            {
                case ComponentLifeStyle.Singleton:
                    return binding.InSingletonScope();
                case ComponentLifeStyle.InThreadScope:
                    return binding.InThreadScope();
                case ComponentLifeStyle.InRequestScope:
                    return binding.InRequestScope();
                case ComponentLifeStyle.Transient:
                default:
                    return binding.InTransientScope();
            }
        }
        public static IBindingSyntax MapKey<T>(this IBindingNamedSyntax<T> binding, string key)
        {
            IBindingSyntax bindingSyntax = binding;
            if (!string.IsNullOrEmpty(key))
            {
                bindingSyntax = binding.Named(key);
            }

            return bindingSyntax;
        }
        public static void ReplaceExisting(this IBindingSyntax bindingInSyntax, Type type)
        {
            var kernel = bindingInSyntax.Kernel;
            var bindingsToRemove = kernel.GetBindings(type).Where(b => string.Equals(b.Metadata.Name, bindingInSyntax.BindingConfiguration.Metadata.Name, StringComparison.Ordinal));
            foreach (var bindingToRemove in bindingsToRemove)
            {
                kernel.RemoveBinding(bindingToRemove);
            }

            var binding = new Binding(type, bindingInSyntax.BindingConfiguration);
            kernel.AddBinding(binding);
        }
    }
}