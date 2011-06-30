﻿/*
 * This file is subject to the terms and conditions defined in
 * file 'license.txt', which is part of this source code package.
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace SteamKit2
{
    /// <summary>
    /// This client is capable of connecting to a config server.
    /// </summary>
    public sealed class ConfigServerClient : ServerClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigServerClient"/> class.
        /// </summary>
        public ConfigServerClient()
        {
        }


#if false
        // v0v
        public byte[] GetUnk()
        {
            if ( !this.HandshakeServer( EServerType.ConfigServer ) )
                return null;

            uint externalIp = this.Socket.Reader.ReadUInt32();

            if ( !this.SendCommand( 7 ) ) // command: who knows!
                return null;

            return this.Socket.Reader.ReadBytes( 9 );

        }
#endif

        public byte[] GetNetworkKey()
        {
            if ( !this.HandshakeServer( EServerType.ConfigServer ) )
                return null;

            uint externalIp = this.Socket.Reader.ReadUInt32();

            if ( !this.SendCommand( 4 ) ) // command: get network key
                return null;

            ushort keyLen = NetHelpers.EndianSwap( this.Socket.Reader.ReadUInt16() );
            byte[] key = this.Socket.Reader.ReadBytes( keyLen );

            ushort sigLen = NetHelpers.EndianSwap( this.Socket.Reader.ReadUInt16() );
            byte[] signature = this.Socket.Reader.ReadBytes( sigLen );

            return key;
        }


        public byte[] GetContentDescriptionRecord()
        {
            return this.GetContentDescriptionRecord( null );
        }
        /// <summary>
        /// Gets the current content description record (CDR) provided by the config server.
        /// Optionally accepts an old CDR hash in order to determine if a new CDR should be downloaded or not.
        /// The old CDR hash is a SHA-1 hash of the entire CDR payload.
        /// </summary>
        /// <param name="oldCDRHash">An optional CDR hash.</param>
        /// <returns>A byte blob representing the CDR on success; otherwise, null.</returns>
        public byte[] GetContentDescriptionRecord( byte[] oldCDRHash )
        {
            try
            {
                if ( !this.HandshakeServer( EServerType.ConfigServer ) )
                    return null;

                uint externalIp = Socket.Reader.ReadUInt32();

                if ( oldCDRHash == null )
                    oldCDRHash = new byte[ 20 ];

                if ( !SendCommand( 9, oldCDRHash ) )
                {
                    return null;
                }

                byte[] unk = Socket.Reader.ReadBytes( 11 );

                TcpPacket pack = Socket.ReceivePacket();
                Socket.Disconnect();

                if ( pack == null )
                    return null;

                return pack.GetPayload();
            }
            catch ( Exception ex )
            {
                DebugLog.WriteLine( "ConfigServerClient", "GetContentDescriptionRecord threw an exception.\n{0}", ex.ToString() );

                Socket.Disconnect();
                return null;
            }

        }

        public byte[] GetClientConfigRecord()
        {

            if ( !this.HandshakeServer( EServerType.ConfigServer ) )
            {
                this.Disconnect();
                return null;
            }

            uint externalIp = Socket.Reader.ReadUInt32();

            if ( !this.SendCommand( 1 ) ) // command: Get CCR
            {
                return null;
            }

            TcpPacket pack = Socket.ReceivePacket();
            this.Disconnect();

            if ( pack == null )
                return null;

            return pack.GetPayload();
        }
    }
}