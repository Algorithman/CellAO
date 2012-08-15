﻿#region License
// Copyright (c) 2005-2012, CellAO Team
// 
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
// 
//     * Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
//     * Neither the name of the CellAO Team nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
// "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
// LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
// A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR
// CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL,
// EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO,
// PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
// PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
// LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
// NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
#endregion

#region Usings...

#endregion

namespace ZoneEngine.Packets
{
    using AO.Core;

    public class KnuBotRejectedItems
    {
        public static void Send(Client cli, NonPlayerCharacterClass KnuBottarget, AOItem[] items)
        {
            PacketWriter pw = new PacketWriter();

            pw.PushByte(0xdf);
            pw.PushByte(0xdf);
            pw.PushShort(0xa);
            pw.PushShort(1);
            pw.PushShort(0);
            pw.PushInt(3086);
            pw.PushInt(cli.Character.ID);
            pw.PushInt(0x7864401d);
            pw.PushIdentity(cli.Character.Type, cli.Character.ID);
            pw.PushByte(0);
            pw.PushShort(2);
            pw.PushIdentity(KnuBottarget.Type, KnuBottarget.ID);
            pw.PushInt(items.Length);
            foreach (AOItem item in items)
            {
                pw.PushInt(item.lowID);
                pw.PushInt(item.highID);
                pw.PushInt(item.Quality);
                pw.PushInt(0x499602d2); // 1234567890  ???????
            }
            pw.PushInt(0);

            byte[] packet = pw.Finish();

            cli.SendCompressed(packet);
        }
    }
}