using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AEngine;

namespace P3Packet
{
    public class LoginReqPacket : PacketBase
    {
        public string AccountName;

        public LoginReqPacket() : base((int)ePacketId.LoginReq)
        {
        }

        public override void Encode(PacketWriter w)
        {
            w.SetString(AccountName);
        }

        public override void Decode(PacketReader r)
        {
            AccountName = r.GetString();
        }
    }

    public class LoginAckPacket : PacketBase
    {
        public bool Success;

        public LoginAckPacket() : base((int)ePacketId.LoginAck)
        {
        }

        public override void Encode(PacketWriter w)
        {
            w.SetBool(Success);
        }

        public override void Decode(PacketReader r)
        {
            Success = r.GetBool();
        }
    }
}
