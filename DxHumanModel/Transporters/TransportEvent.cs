using System;
using System.Collections.Generic;
using DxHumanModel.Classifiers;
using ProtoBuf;

namespace DxHumanModel.Transporters
{
    [ProtoContract]
    public class TransportEvent
    {
        [ProtoMember(1)]
        public Dictionary<string, double> Concentrations { get; set;  }

        [ProtoMember(2)]
        public Volume<int> volume;

        [ProtoMember(3)]
        public int messageIcrement;

        public TransportEvent()
        {
        }
    }
}
