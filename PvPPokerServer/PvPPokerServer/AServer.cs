using System.Collections.Concurrent;
using AEngine;

namespace PvPPokerServer
{
    public class AServer : ServerBase<ASession>
    {
        public ConcurrentDictionary<string, ASession> UserMap = new ConcurrentDictionary<string, ASession>();

        public override void OnAccept(ASession session)
        {
        }

        public override void OnDisconnect(ASession session)
        {
        }
    }
}