﻿#region License
/*
Copyright (c) 2005-2012, CellAO Team

All rights reserved.

Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

    * Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
    * Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
    * Neither the name of the CellAO Team nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
"AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR
CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL,
EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO,
PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/
#endregion
#region Usings...
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Must have
using AO.Core.Scripting;
#endregion
#region NameSpace
namespace ZoneEngine.Script
{

    #region Class ServerProperties

    /// <summary>
    /// Changes to XP, Coin drop, and player vs player should go here
    /// Has to be public or when in assembly form .net wont see it
    /// </summary>
    public class ServerProperties : IAOScript
    {
        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Constructors
        public ServerProperties()
        {
        }

        #endregion

        #region Script Entry Point
        /// <summary>
        /// Script Entry Point
        /// </summary>
        /// <param name="args"></param>
        public void Main(string[] args)
        {
        }
        #endregion

        #region OnConnect
        public void OnConnect(Character character)
        {
            Console.WriteLine("Client OnConnect Test 2");
        }
        #endregion

    }
    #endregion Class ServerProperties
}
#endregion NameSpace
