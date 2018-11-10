﻿namespace AEngine
{
    public abstract class PacketBase
    {
        public PacketBase(int packetId)
        {
            PacketId = packetId;
        }
        public int PacketId;
        public abstract void Decode(PacketReader r);
        public abstract void Encode(PacketWriter w);
    }

    public interface ITrans
    {
        void Decode(PacketReader r);
        void Encode(PacketWriter w);
    }
}