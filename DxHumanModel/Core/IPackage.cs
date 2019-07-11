using System;
using System.Collections.Generic;
using ProtoBuf;

namespace DxHumanModel.Core
{
    public interface IPackage<S> where S : IAutomataState
    {
        [ProtoMember(2)]
        int messageIcrement { get; }

        S state { get; set; }
    }
}
