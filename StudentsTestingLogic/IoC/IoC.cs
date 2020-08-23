using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsTesting1.IoC
{
    public class IoCContainer
    {
        private readonly Dictionary<Type, Type> registeredObjects = new Dictionary<Type, Type>();

        public void RegisterObject<TypeToRegister, ConcreteType>()
        {
            registeredObjects.Add(typeof(TypeToRegister), typeof(ConcreteType));
        }

        public object ResolveObject(Type typeToResolve, IEnumerable<object> parameters)
        {
            Type resolvedType;
            registeredObjects.TryGetValue(typeToResolve, out resolvedType);
            if (resolvedType == null)
            {
                object[] parametersArray = parameters.ToArray();
                return Activator.CreateInstance(typeToResolve, parametersArray);
            }

            object[] param = parameters.ToArray();
            return Activator.CreateInstance(resolvedType, param);
        }
    }
}
