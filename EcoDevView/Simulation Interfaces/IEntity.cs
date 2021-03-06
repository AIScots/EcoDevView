﻿namespace Eco.DevView
{
    /// <summary>
    /// A general interface that describes 
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        int Id { get; }

        /// <summary>
        /// X-coordinate
        /// </summary>
        float X { get; }

        /// <summary>
        /// Z-coordinate
        /// </summary>
        float Z { get; }

        /// <summary>
        /// Species of the entity.
        /// </summary>
        string Species { get; }

        /// <summary>
        /// Health within [0..1], whereas "0" is "dead" and "1" is as alive as can be.
        /// </summary>
        float Health { get; }
    }
}
