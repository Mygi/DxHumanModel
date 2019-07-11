using System;
using System.Collections.Generic;
using DxHumanModel.Topology;
using ProtoBuf;

namespace DxHumanModel.Transporters
{
    [ProtoContract]
    public class TransportEvent
    {
        [ProtoMember(1)]
        public Dictionary<string, decimal> Concentrations { get; set;  }

        [ProtoMember(2)]
        public Volume volume;

        [ProtoMember(3)]
        public int messageIcrement;

        public TransportEvent()
        {
        }
    }
}
