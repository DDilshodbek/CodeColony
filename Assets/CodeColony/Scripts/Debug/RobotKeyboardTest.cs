using CodeColony.Robots;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CodeColony.Debugging
{
    /// <summary>Temporary keyboard testing component. Remove when the coding system is connected.</summary>
    [AddComponentMenu("CodeColony/Debug/Robot Keyboard Test (Temporary)")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(RobotController))]
    public sealed class RobotKeyboardTest : MonoBehaviour
    {
        private RobotController robot;

        private void Awake()
        {
            robot = GetComponent<RobotController>();
        }

        private void Update()
        {
            Keyboard keyboard = Keyboard.current;
            if (keyboard == null || robot == null)
                return;

            if (keyboard.wKey.wasPressedThisFrame)
            {
                robot.MoveForward();
                Debug.Log($"[RobotKeyboardTest] W -> MoveForward(); position: {robot.transform.position}", this);
            }

            if (keyboard.aKey.wasPressedThisFrame)
            {
                robot.TurnLeft();
                Debug.Log($"[RobotKeyboardTest] A -> TurnLeft(); forward: {robot.transform.forward}", this);
            }

            if (keyboard.dKey.wasPressedThisFrame)
            {
                robot.TurnRight();
                Debug.Log($"[RobotKeyboardTest] D -> TurnRight(); forward: {robot.transform.forward}", this);
            }

            if (keyboard.eKey.wasPressedThisFrame)
            {
                RobotInventory inventory = robot.GetComponent<RobotInventory>();
                int countBefore = inventory.Count;
                robot.Collect();
                Debug.Log($"[RobotKeyboardTest] E -> Collect(); collected: {inventory.Count - countBefore}; " +
                    $"inventory: {inventory.Count}/{inventory.Capacity}. " +
                    "If collected is 0, check capacity and an enabled, non-depleted resource collider in range on an allowed layer.", this);
            }
        }
    }
}
