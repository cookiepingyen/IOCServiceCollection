using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOCServiceCollection
{
    public class ServiceCollection : IServiceCollection
    {
        private bool _isReadOnly;
        public Dictionary<Type, ServiceDescriptor> dictiontry = new Dictionary<Type, ServiceDescriptor>();

        public int Count => dictiontry.Count;

        public bool IsReadOnly => _isReadOnly;

        public Microsoft.Extensions.DependencyInjection.ServiceDescriptor this[int index] { get => GetServiceDescriptorByIndex(index); set => throw new NotImplementedException(); }

        private ServiceDescriptor GetServiceDescriptorByIndex(int index)
        {
            int current = 0;
            ServiceDescriptor descriptor = null;
            foreach (var dict in dictiontry)
            {
                if (current == index)
                {
                    descriptor = dict.Value;
                    break;
                }
                current++;
            }
            return descriptor;
        }
        public IServiceCollection AddSingleton<Ttype>()
        {
            return AddSingleton(typeof(Ttype), typeof(Ttype));
        }

        public IServiceCollection AddSingleton<Ttype, TService>()
        {
            return AddSingleton(typeof(Ttype), typeof(TService));
        }

        public IServiceCollection AddSingleton(Type serviceType, Type implementationType)
        {
            var descriptor = new ServiceDescriptor(serviceType, implementationType, ServiceLifetime.Singleton);
            Add(descriptor);
            return this;
        }

        public IServiceCollection AddSingleton(Type serviceType, Func<IServiceProvider, object> implementationFactory)
        {
            var descriptor = new ServiceDescriptor(serviceType, implementationFactory, ServiceLifetime.Singleton);
            Add(descriptor);
            return this;
        }

        public IServiceCollection AddScoped<Ttype, TService>()
        {
            return AddScoped(typeof(Ttype), typeof(TService));
        }

        public IServiceCollection AddScoped(Type serviceType, Type implementationType)
        {
            var descriptor = new ServiceDescriptor(serviceType, implementationType, ServiceLifetime.Scoped);
            Add(descriptor);
            return this;
        }

        public IServiceCollection AddScoped(Type serviceType, Func<IServiceProvider, object> implementationFactory)
        {
            var descriptor = new ServiceDescriptor(serviceType, implementationFactory, ServiceLifetime.Scoped);
            Add(descriptor);
            return this;
        }

        public IServiceCollection AddTransient<Ttype, TService>()
        {
            return AddTransient(typeof(Ttype), typeof(TService));
        }

        public IServiceCollection AddTransient(Type serviceType, Type implementationType)
        {
            var descriptor = new ServiceDescriptor(serviceType, implementationType, ServiceLifetime.Transient);
            Add(descriptor);
            return this;
        }

        public IServiceCollection AddTransient(Type serviceType, Func<IServiceProvider, object> implementationFactory)
        {
            var descriptor = new ServiceDescriptor(serviceType, implementationFactory, ServiceLifetime.Transient);
            Add(descriptor);
            return this;
        }

        public IServiceCollection AddTransient<Ttype>(Func<IServiceProvider, object> implementationFactory)
        {
            var descriptor = new ServiceDescriptor(typeof(Ttype), implementationFactory, ServiceLifetime.Transient);
            Add(descriptor);
            return this;
        }


        public ServiceProvider BuildServiceProvider()
        {
            return new ServiceProvider(this);
        }

        public int IndexOf(Microsoft.Extensions.DependencyInjection.ServiceDescriptor item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, Microsoft.Extensions.DependencyInjection.ServiceDescriptor item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public void Add(ServiceDescriptor item)
        {
            if (!dictiontry.ContainsKey(item.ServiceType))
                dictiontry.Add(item.ServiceType, item);
            else
                dictiontry[item.ServiceType] = item;
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(Microsoft.Extensions.DependencyInjection.ServiceDescriptor item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Microsoft.Extensions.DependencyInjection.ServiceDescriptor[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(Microsoft.Extensions.DependencyInjection.ServiceDescriptor item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<Microsoft.Extensions.DependencyInjection.ServiceDescriptor> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
