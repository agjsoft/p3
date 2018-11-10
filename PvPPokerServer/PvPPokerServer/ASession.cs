using AEngine;
using P3Packet;

namespace PvPPokerServer
{
    public class ASession : SessionBase
    {
        public override void OnPacket(int packetId, PacketReader r)
        {
            switch (packetId)
            {
                case (int)ePacketId.LoginReq:
                    {
                        var recvPacket = r.GetPacket<LoginReqPacket>();
                    }
                    break;
            }
        }
    }
}