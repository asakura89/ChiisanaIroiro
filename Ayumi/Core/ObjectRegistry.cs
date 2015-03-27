﻿using System;
using System.Collections.Generic;

namespace Ayumi.Core
{
    public class ObjectRegistry : IObjectRegistry
    {
        private static readonly Dictionary<Type, Type> objectList = new Dictionary<Type, Type>();

        public A GetRegisteredObject<A>()
        {
            return GetRegisteredObject<A>(null);
        }

        public A GetRegisteredObject<A>(params Object[] constructorParamList)
        {
            Type implType = objectList[typeof(A)];
            return (A)Activator.CreateInstance(implType, constructorParamList);
        }

        public void RegisterObject<A, I>() where I : A
        {
            Type abstractType = typeof(A);
            Type implType = typeof (I);

            if (!objectList.ContainsKey(abstractType))
                objectList.Add(abstractType, implType);
        } 
    }
}