using CodeColony.World;
using UnityEngine;

namespace CodeColony.Robots
{
    /// <summary>Discrete commands for a future programming system to invoke.</summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(RobotInventory))]
    public sealed class RobotController : MonoBehaviour
    {
        [SerializeField, Min(0.01f)] private float moveDistance = 1f;
        [SerializeField, Min(0.01f)] private float collectionRange = 1.5f;
        [SerializeField, Min(1)] private int collectionAmount = 1;
        [SerializeField] private LayerMask resourceLayers = ~0;

        private RobotInventory inventory;

        private void Awake()
        {
            inventory = GetComponent<RobotInventory>();
        }

        /// <summary>Moves one step along local forward, without collision checks.</summary>
        public void MoveForward()
        {
            transform.position += transform.forward * Mathf.Max(0.01f, moveDistance);
        }

        public void TurnLeft()
        {
            transform.Rotate(0f, -90f, 0f, Space.World);
        }

        public void TurnRight()
        {
            transform.Rotate(0f, 90f, 0f, Space.World);
        }

        /// <summary>Collects from the nearest available node; does nothing if full or none is in range.</summary>
        public void Collect()
        {
            // Also supports calls before Awake on an inactive robot.
            if (inventory == null)
                inventory = GetComponent<RobotInventory>();

            if (inventory == null || inventory.AvailableCapacity == 0)
                return;

            Vector3 origin = transform.position;
            Collider[] hits = Physics.OverlapSphere(
                origin, Mathf.Max(0.01f, collectionRange), resourceLayers,
                QueryTriggerInteraction.Collide);

            ResourceNode nearestNode = null;
            float nearestDistanceSquared = float.PositiveInfinity;

            foreach (Collider hit in hits)
            {
                ResourceNode node = hit.GetComponentInParent<ResourceNode>();
                if (node == null || !node.isActiveAndEnabled || node.IsDepleted)
                    continue;

                float distanceSquared = (hit.ClosestPoint(origin) - origin).sqrMagnitude;
                if (distanceSquared < nearestDistanceSquared)
                {
                    nearestNode = node;
                    nearestDistanceSquared = distanceSquared;
                }
            }

            if (nearestNode == null)
                return;

            int requested = Mathf.Min(Mathf.Max(1, collectionAmount), inventory.AvailableCapacity);
            inventory.Add(nearestNode.Extract(requested));
        }
    }
}
