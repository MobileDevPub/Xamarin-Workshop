using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMovieCollection.Implementation.Utilities
{
    /// <summary>
    /// ServiceContainer that keeps track of all the interfaces and it's implementations
    /// to enable Dependency Injection
    /// </summary>
    public class ServiceContainer
    {
        static readonly object Locker = new object();
        static ServiceContainer _Instance;

        private ServiceContainer()
        {
            Services = new Dictionary<Type, Lazy<object>>();
        }

        private Dictionary<Type, Lazy<object>> Services { get; set; }

        private static ServiceContainer Instance
        {
            get
            {
                lock (Locker)
                {
                    if (_Instance == null)
                        _Instance = new ServiceContainer();
                    return _Instance;
                }
            }
        }

        /// <summary>
        /// Registers the specified service.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="service">The service.</param>
        public static void Register<T>(T service)
        {
            Instance.Services[typeof(T)] = new Lazy<object>(() => service);
        }

        /// <summary>
        /// Registers this instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "By Design")]
        public static void Register<T>()
            where T : new()
        {
            Instance.Services[typeof(T)] = new Lazy<object>(() => new T());
        }

        /// <summary>
        /// Registers the specified function.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="function">The function.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "By Design")]
        public static void Register<T>(Func<object> function)
        {
            Instance.Services[typeof(T)] = new Lazy<object>(function);
        }

        /// <summary>
        /// Resolves this instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException"></exception>
        public static T Resolve<T>()
        {
            Lazy<object> service;
            if (Instance.Services.TryGetValue(typeof(T), out service))
            {
                return (T)service.Value;
            }
            else
            {
                throw new KeyNotFoundException(string.Format(CultureInfo.InvariantCulture, "Service not found for type '{0}'", typeof(T)));
            }
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public static void Clear()
        {
            Instance.Services.Clear();
        }

    }
}
