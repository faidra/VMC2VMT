using System;
using UnityEngine;

namespace VMC2VMT
{
    public readonly struct PositionAndRotation : IEquatable<PositionAndRotation>
    {
        public readonly Vector3 Position;
        public readonly Quaternion Rotation;

        public static readonly PositionAndRotation Default = new PositionAndRotation(Vector3.zero, Quaternion.identity);

        public PositionAndRotation(Vector3 position, Quaternion rotation)
        {
            this.Position = position;
            this.Rotation = rotation;
        }

        public bool Equals(PositionAndRotation other)
        {
            return Position.Equals(other.Position) && Rotation.Equals(other.Rotation);
        }

        public override bool Equals(object obj)
        {
            return obj is PositionAndRotation other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Position, Rotation);
        }
    }
}
