﻿using System.IO;
using Phantasma.Core.Cryptography;
using Phantasma.Core.Domain;

namespace Phantasma.Business.Blockchain.Storage
{
    // allows to encrypt data shared between two addresses
    public class SharedArchiveEncryption : IArchiveEncryption
    {
        public Address Source { get; private set; }
        public Address Destination { get; private set; }

        public SharedArchiveEncryption()
        {
        }

        public ArchiveEncryptionMode Mode => ArchiveEncryptionMode.Shared;

        public string EncryptName(string name, PhantasmaKeys keys)
        {
            throw new System.NotImplementedException();
        }
        public string DecryptName(string name, PhantasmaKeys keys)
        {
            throw new System.NotImplementedException();
        }
        public byte[] Encrypt(byte[] chunk, PhantasmaKeys keys)
        {
            if (keys.Address != this.Source && keys.Address != this.Destination)
            {
                throw new ChainException("encryption public address does not match");
            }

            return DiffieHellman.Encrypt(chunk, keys.PrivateKey);
        }

        public byte[] Decrypt(byte[] chunk, PhantasmaKeys keys)
        {
            if (keys.Address != this.Source && keys.Address != this.Destination)
            {
                throw new ChainException("decryption public address does not match");
            }

            return DiffieHellman.Decrypt(chunk, keys.PrivateKey);
        }

        public void SerializeData(BinaryWriter writer)
        {
            writer.WriteAddress(Source);
            writer.WriteAddress(Destination);
        }

        public void UnserializeData(BinaryReader reader)
        {
            this.Source = reader.ReadAddress();
            this.Destination = reader.ReadAddress();
        }
    }
}
