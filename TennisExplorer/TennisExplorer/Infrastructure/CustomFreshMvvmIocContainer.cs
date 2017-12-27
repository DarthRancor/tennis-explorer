using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using FreshMvvm;

namespace TennisExplorer.Infrastructure
{
    public class CustomFreshMvvmIocContainer : FreshMvvm.IFreshIOC
    {
        public IRegisterOptions Register<RegisterType>(RegisterType instance) where RegisterType : class
        {
            return null;
        }

        public IRegisterOptions Register<RegisterType>(RegisterType instance, string name) where RegisterType : class
        {
            AppDependencySetup.Register(instance, name);
            return new CustomRegisterOptions();
        }

        public object Resolve(Type resolveType)
        {
            return AppDependencySetup.Resolve(resolveType);
        }

        public ResolveType Resolve<ResolveType>() where ResolveType : class
        {
            return AppDependencySetup.Resolve<ResolveType>();
        }

        public ResolveType Resolve<ResolveType>(string name) where ResolveType : class
        {
            var service = AppDependencySetup.Resolve<ResolveType>(name);
            return service;
        }

        public void Unregister<RegisterType>()
        {
            throw new NotImplementedException();
        }

        public void Unregister<RegisterType>(string name)
        {
            throw new NotImplementedException();
        }

        IRegisterOptions IFreshIOC.Register<RegisterType, RegisterImplementation>()
        {
            throw new NotImplementedException();
        }
    }

    public class CustomRegisterOptions : IRegisterOptions
    {
        public IRegisterOptions AsMultiInstance()
        {
            return new CustomRegisterOptions();
        }

        public IRegisterOptions AsSingleton()
        {
            return new CustomRegisterOptions();
        }

        public IRegisterOptions UsingConstructor<RegisterType>(Expression<Func<RegisterType>> constructor)
        {
            return new CustomRegisterOptions();
        }

        public IRegisterOptions WithStrongReference()
        {
            return new CustomRegisterOptions();
        }

        public IRegisterOptions WithWeakReference()
        {
            return new CustomRegisterOptions();
        }
    }
}
