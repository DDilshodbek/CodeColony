using UnityEngine;

namespace CodeColony.World
{
    /// <summary>A finite supply of generic resources. Add a collider to this object or a child.</summary>
    [DisallowMultipleComponent]
    public sealed class ResourceNode : MonoBehaviour
    {
        [SerializeField, Min(0)] private int remainingAmount = 50;

        public int RemainingAmount => Mathf.Max(0, remainingAmount);
        public bool IsDepleted => RemainingAmount == 0;

        /// <summary>Extracts up to the requested amount and returns the actual yield.</summary>
        public int Extract(int requestedAmount)
        {
            int extracted = Mathf.Clamp(requestedAmount, 0, RemainingAmount);
            remainingAmount = RemainingAmount - extracted;
            return extracted;
        }
    }
}
