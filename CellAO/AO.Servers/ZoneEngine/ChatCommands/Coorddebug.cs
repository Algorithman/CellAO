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

namespace ZoneEngine.ChatCommands
{
    using System.Collections.Generic;

    using AO.Core;

    using ZoneEngine.Script;

    public class ChatCommandcoorddebug : AOChatCommand
    {
        public override void ExecuteCommand(Client client, Identity target, string[] args)
        {
            client.SendChatText(
                client.Character.Coordinates + " " + client.Character.PlayField + " "
                + client.Character.Heading.x.ToString() + " " + client.Character.Heading.y.ToString() + " "
                + client.Character.Heading.z.ToString() + " " + client.Character.Heading.w.ToString() + " "
                + client.Character.Heading.VectorRepresentation());
        }

        public override void CommandHelp(Client client)
        {
            // No help needed, no arguments can be given
            client.SendChatText("This command shows your coordinates, playfield and heading.");
            return;
        }

        public override bool CheckCommandArguments(string[] args)
        {
            // Always true, no arguments to check
            return true;
        }

        public override int GMLevelNeeded()
        {
            return 0;
        }

        public override List<string> ListCommands()
        {
            List<string> temp = new List<string>();
            temp.Add("coorddebug");
            return temp;
        }
    }
}