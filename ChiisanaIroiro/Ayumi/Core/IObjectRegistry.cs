using System;

namespace ChiisanaIroiro.Ayumi.Core
{
    public interface IObjectRegistry
    {
        A GetRegisteredObject<A>();
        A GetRegisteredObject<A>(params Object[] constructorParamList);
        void RegisterObject<A, I>() where I : A;
    }
}