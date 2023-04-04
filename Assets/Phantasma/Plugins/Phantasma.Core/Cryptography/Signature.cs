﻿using System.Collections.Generic;
using System.IO;
using Phantasma.Core.Domain;

namespace Phantasma.Core.Cryptography
{
    public abstract class Signature: ISerializable
    {
        public abstract SignatureKind Kind { get; }

        public abstract void SerializeData(BinaryWriter writer);
        public abstract void UnserializeData(BinaryReader reader);

        /// <summary>
        /// Checks if this transaction was signed by at least one of the addresses
        /// </summary>
        public abstract bool Verify(byte[] message, IEnumerable<Address> addresses);

        public bool Verify(byte[] message, Address address)
        {
            return Verify(message, new Address[] { address });
        }

        public byte[] ToByteArray()
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream))
                {
                    this.SerializeData(writer);
                }

                return stream.ToArray();
            }
        }
    }
}
