using UnityEngine;

namespace CodeColony.Robots
{
    /// <summary>Stores a single generic resource, up to a fixed capacity.</summary>
    [DisallowMultipleComponent]
    public sealed class RobotInventory : MonoBehaviour
    {
        [SerializeField, Min(1)] private int capacity = 20;

        public int Capacity => Mathf.Max(1, capacity);
        public int Count { get; private set; }
        public int AvailableCapacity => Mathf.Max(0, Capacity - Count);

        /// <summary>Adds what fits and returns the amount actually stored.</summary>
        public int Add(int amount)
        {
            int accepted = Mathf.Clamp(amount, 0, AvailableCapacity);
            Count += accepted;
            return accepted;
        }

        /// <summary>Removes available resources and returns the amount removed.</summary>
        public int Remove(int amount)
        {
            int removed = Mathf.Clamp(amount, 0, Count);
            Count -= removed;
            return removed;
        }
    }
}
