using System;

namespace Eco.DevView.DummyServer
{
    enum UpdateOutcome
    {
        None,
        Changed,
        Removed
    }

    interface IUpdateable
    {
        /// <summary>
        /// Unique identifier of the thing.
        /// </summary>
        int Id { get; }

        /// <summary>
        /// Returns <c>true</c> if this entity has changed during this update, <c>false</c> otherwise.
        /// </summary>
        /// <param name="random"><see cref="Random"/> that can be used to determine the new state</param>
        /// <returns></returns>
        UpdateOutcome Update(Random random);
    }
}
