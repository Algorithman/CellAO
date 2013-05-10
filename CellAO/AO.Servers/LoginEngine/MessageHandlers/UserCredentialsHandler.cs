﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserCredentialsHandler.cs" company="CellAO Team">
//   Copyright © 2005-2013 CellAO Team.
//   
//   All rights reserved.
//   
//   Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
//   
//       * Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
//       * Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
//       * Neither the name of the CellAO Team nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.
//   
//   THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
//   "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
//   LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
//   A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR
//   CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL,
//   EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO,
//   PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
//   PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
//   LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
//   NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
//   SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
// </copyright>
// <summary>
//   Defines the UserCredentialsHandler type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LoginEngine.MessageHandlers
{
    using System;
    using System.ComponentModel.Composition;
    using System.Linq;

    using AO.Core;
    using AO.Core.Components;

    using LoginEngine.Packets;
    using LoginEngine.QueryBase;

    using SmokeLounge.AOtomation.Messaging.GameData;
    using SmokeLounge.AOtomation.Messaging.Messages;
    using SmokeLounge.AOtomation.Messaging.Messages.SystemMessages;

    using Identity = SmokeLounge.AOtomation.Messaging.GameData.Identity;

    [Export(typeof(IHandleMessage))]
    public class UserCredentialsHandler : IHandleMessage<UserCredentialsMessage>
    {
        #region Public Methods and Operators

        public void Handle(object sender, Message message)
        {
            var client = (Client)sender;
            var userCredentialsMessage = (UserCredentialsMessage)message.Body;
            var checkLogin = new CheckLogin();
            if (checkLogin.IsLoginAllowed(client, userCredentialsMessage.UserName) == false)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(
                    "Client '" + client.AccountName
                    + "' banned, not a valid username, or sent a malformed Authentication Packet");
                Console.ResetColor();

                client.Send(0x00001F83, new LoginErrorMessage { Error = LoginError.InvalidUserNamePassword });
                client.Server.DisconnectClient(client);
                return;
            }

            if (checkLogin.IsLoginCorrect(client, userCredentialsMessage.Credentials) == false)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Client '" + client.AccountName + "' failed Authentication.");
                Console.ResetColor();

                client.Send(0x00001F83, new LoginErrorMessage { Error = LoginError.InvalidUserNamePassword });
                client.Server.DisconnectClient(client);
                return;
            }

            var expansions = 0;
            var allowedCharacters = 0;

            /* This checks your expansions and
               number of characters allowed (num. of chars doesn't work)*/
            var sqlQuery = "SELECT `Expansions`,`Allowed_Characters` FROM `login` WHERE Username = '"
                           + client.AccountName + "'";
            var ms = new SqlWrapper();
            var dt = ms.ReadDatatable(sqlQuery);
            if (dt.Rows.Count > 0)
            {
                expansions = int.Parse((string)dt.Rows[0][0]);
                allowedCharacters = (int)dt.Rows[0][1];
            }

            var characters = from c in CharacterList.LoadCharacters(client.AccountName)
                             select
                                 new LoginCharacterInfo
                                     {
                                         Unknown1 = 4, 
                                         Id = c.Id, 
                                         PlayfieldProxyVersion = 0x61, 
                                         PlayfieldId =
                                             new Identity
                                                 {
                                                     Type = IdentityType.Playfield, 
                                                     Instance = c.Playfield
                                                 }, 
                                         PlayfieldAttribute = 1, 
                                         ExitDoor = 0, 
                                         ExitDoorId = Identity.None, 
                                         Unknown2 = 1, 
                                         CharacterInfoVersion = 5, 
                                         CharacterId = c.Id, 
                                         Name = c.Name, 
                                         Breed = (Breed)c.Breed, 
                                         Gender = (Gender)c.Gender, 
                                         Profession = (Profession)c.Profession, 
                                         Level = c.Level, 
                                         AreaName = "area unknown", 
                                         Status = CharacterStatus.Active
                                     };
            var characterListMessage = new CharacterListMessage
                                           {
                                               Characters = characters.ToArray(), 
                                               AllowedCharacters = allowedCharacters, 
                                               Expansions = expansions
                                           };
            client.Send(0x0000615B, characterListMessage);
        }

        #endregion
    }
}