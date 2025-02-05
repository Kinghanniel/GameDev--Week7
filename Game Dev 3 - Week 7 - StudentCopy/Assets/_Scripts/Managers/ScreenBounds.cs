using UnityEngine;
using UnityEngine.Events;

namespace GameDevWithMarco.Managers
{
    // Ensures the GameObject has a BoxCollider2D component
    [RequireComponent(typeof(BoxCollider2D))]
    public class ScreenBounds : MonoBehaviour
    {
        /// <summary>
        /// Code inspired by this video: https://www.youtube.com/watch?v=1a9ag16PeFw
        /// This script ensures objects don't leave the screen by defining boundaries and handling wrap-around logic.
        /// </summary>

        // Reference to the main camera
        public Camera mainCamera;

        // BoxCollider2D used to define screen boundaries
        private BoxCollider2D boxCollider;

        // Unity event triggered when an object exits the screen bounds
        public UnityEvent<Collider2D> ExitTriggerFired;

        // Offset applied when teleporting objects to prevent overlap with the boundary
        [SerializeField] private float teleportOffset = 0.2f;

        // Offset to handle corner wrapping correctly
        [SerializeField] private float cornerOffset = 1;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// Initializes references to the camera and collider.
        /// </summary>
        private void Awake()
        {
            mainCamera = Camera.main;
            this.mainCamera.transform.localScale = Vector3.one; // Ensures camera scale is reset
            boxCollider = GetComponent<BoxCollider2D>();
            boxCollider.isTrigger = true; // Allows detecting exit without physical collision
        }

        /// <summary>
        /// Start is called before the first frame update.
        /// Sets the position and updates the collider size to match the screen bounds.
        /// </summary>
        private void Start()
        {
            transform.position = Vector3.zero; // Ensure this object is centered
            UpdateBoundsSize();
        }

        /// <summary>
        /// Adjusts the collider size to match the camera's viewport.
        /// </summary>
        public void UpdateBoundsSize()
        {
            // Calculate the height of the screen (Orthographic size is half the height, so multiply by 2)
            float ySize = mainCamera.orthographicSize * 2;

            // Calculate width based on aspect ratio
            Vector2 boxColliderSize = new Vector2(ySize * mainCamera.aspect, ySize);

            // Apply the calculated size to the collider
            boxCollider.size = boxColliderSize;
        }

        /// <summary>
        /// Called when another collider exits the screen boundary.
        /// Triggers the UnityEvent to notify other scripts.
        /// </summary>
        private void OnTriggerExit2D(Collider2D collision)
        {
            ExitTriggerFired?.Invoke(collision);
        }

        /// <summary>
        /// Checks if a given world position is outside the screen bounds.
        /// </summary>
        /// <param name="worldPosition">The position to check.</param>
        /// <returns>True if the object is out of bounds, otherwise false.</returns>
        public bool AmIOutOfBounds(Vector3 worldPosition)
        {
            return
                Mathf.Abs(worldPosition.x) > Mathf.Abs(boxCollider.bounds.min.x) ||
                Mathf.Abs(worldPosition.y) > Mathf.Abs(boxCollider.bounds.min.y);
        }

        /// <summary>
        /// Calculates a wrapped position if an object moves beyond the screen boundaries.
        /// This allows objects to teleport to the opposite side of the screen.
        /// </summary>
        /// <param name="worldPosition">The object's current position.</param>
        /// <returns>The new position after wrapping around the screen.</returns>
        public Vector2 CalculateWrappedPosition(Vector2 worldPosition)
        {
            // Check if the object is beyond the X or Y bounds
            bool xBoundResult = Mathf.Abs(worldPosition.x) > (Mathf.Abs(boxCollider.bounds.min.x) - cornerOffset);
            bool yBoundResult = Mathf.Abs(worldPosition.y) > (Mathf.Abs(boxCollider.bounds.min.y) - cornerOffset);

            // Get the sign (+1 or -1) of the world position to determine direction
            Vector2 signWorldPosition = new Vector2(Mathf.Sign(worldPosition.x), Mathf.Sign(worldPosition.y));

            // If out of bounds in both X and Y directions (corner wrap)
            if (xBoundResult && yBoundResult)
            {
                return Vector2.Scale(worldPosition, Vector2.one * -1) + Vector2.Scale(new Vector2(teleportOffset, teleportOffset), signWorldPosition);
            }
            // If out of bounds in the X direction only
            else if (xBoundResult)
            {
                return new Vector2(worldPosition.x * -1, worldPosition.y) + new Vector2(teleportOffset * signWorldPosition.x, teleportOffset);
            }
            // If out of bounds in the Y direction only
            else if (yBoundResult)
            {
                return new Vector2(worldPosition.x, worldPosition.y * -1) + new Vector2(teleportOffset, teleportOffset * signWorldPosition.y);
            }
            // If not out of bounds, return the original position
            else
            {
                return worldPosition;
            }
        }
    }
}
