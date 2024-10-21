using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOCServiceCollection
{
    public class ServiceCollection
    {
        private readonly List<ServiceDescriptor> _descriptors = new List<ServiceDescriptor>();
        private bool _isReadOnly;
        private Dictionary<Type, object> dictiontry = new Dictionary<Type, object>();

        public int Count => _descriptors.Count;

        public bool IsReadOnly => _isReadOnly;

        public ServiceCollection AddSingleton<Ttype, TService>()
        {
            return AddSingleton(typeof(Ttype), typeof(TService));
        }

        public ServiceCollection AddSingleton(Type serviceType, Type implementationType)
        {
            var descriptor = new ServiceDescriptor(serviceType, implementationType, ServiceLifetime.Singleton);
            dictiontry.Add(serviceType, descriptor);
            return this;
        }

        public ServiceCollection AddSingleton(Type serviceType, Func<ServiceProvider, object> implementationFactory)
        {
            var descriptor = new ServiceDescriptor(serviceType, implementationFactory, ServiceLifetime.Singleton);
            dictiontry.Add(serviceType, descriptor);
            return this;
        }

        public ServiceCollection AddScoped<Ttype, TService>()
        {
            return AddScoped(typeof(Ttype), typeof(TService));
        }

        public ServiceCollection AddScoped(Type serviceType, Type implementationType)
        {
            var descriptor = new ServiceDescriptor(serviceType, implementationType, ServiceLifetime.Scoped);
            dictiontry.Add(serviceType, descriptor);
            return this;
        }

        public ServiceCollection AddScoped(Type serviceType, Func<ServiceProvider, object> implementationFactory)
        {
            var descriptor = new ServiceDescriptor(serviceType, implementationFactory, ServiceLifetime.Scoped);
            dictiontry.Add(serviceType, descriptor);
            return this;
        }

        public ServiceCollection AddTransient<Ttype, TService>()
        {
            return AddTransient(typeof(Ttype), typeof(TService));
        }

        public ServiceCollection AddTransient(Type serviceType, Type implementationType)
        {
            var descriptor = new ServiceDescriptor(serviceType, implementationType, ServiceLifetime.Transient);
            dictiontry.Add(serviceType, descriptor);
            return this;
        }

        public ServiceCollection AddTransient(Type serviceType, Func<ServiceProvider, object> implementationFactory)
        {
            var descriptor = new ServiceDescriptor(serviceType, implementationFactory, ServiceLifetime.Transient);
            dictiontry.Add(serviceType, descriptor);
            return this;
        }
    }
}
