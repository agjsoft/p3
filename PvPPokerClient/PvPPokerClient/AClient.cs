using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using AEngine;

namespace PvPPokerClient
{
    public class AClient : ClientBase
    {
        public override void OnConnect(SocketError result)
        {
        }

        public override void OnPacket(int packetId, PacketReader reader)
        {
            switch (packetId)
            {
                case 0:
                    break;
            }
        }
    }
}
