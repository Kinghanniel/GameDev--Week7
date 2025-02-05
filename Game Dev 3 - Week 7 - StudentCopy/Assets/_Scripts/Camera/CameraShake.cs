using UnityEngine;

namespace GameDevWithMarco.CameraStuff
{
    // This class handles a simple camera shake effect by modifying its local position
    public class CameraShake : MonoBehaviour
    {
        // Stores the camera's original position to reset after shaking
        private Vector3 originalPosition;

        // Shake effect duration (remaining time for the effect)
        private float shakeDuration = 0f;

        // The intensity of the shake effect
        private float shakeMagnitude = 0.1f;

        // Controls how quickly the shake effect fades
        private float dampingSpeed = 1.0f;

        // Serialized fields for setting shake duration and magnitude from the Inspector
        [SerializeField] float actualShakeDuration;
        [SerializeField] float actualShakeMagnitude;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// It initializes the original camera position.
        /// </summary>
        private void Awake()
        {
            // Ensures the camera's original position is correctly set (z = -10 to maintain depth)
            originalPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -10);
        }

        /// <summary>
        /// Update is called once per frame.
        /// Handles the shake effect and gradually reduces its intensity over time.
        /// </summary>
        void Update()
        {
            if (shakeDuration > 0)
            {
                // Randomly offsets the camera position within a small sphere to create a shake effect
                transform.localPosition = originalPosition + Random.insideUnitSphere * shakeMagnitude;

                // Reduce shake duration over time, applying the damping effect
                shakeDuration -= Time.deltaTime * dampingSpeed;
            }
            else
            {
                // Reset shake parameters when duration reaches zero
                shakeDuration = 0f;
                transform.localPosition = originalPosition;
            }
        }

        /// <summary>
        /// Starts the camera shake effect with a specified duration and magnitude.
        /// </summary>
        /// <param name="duration">How long the shake will last.</param>
        /// <param name="magnitude">How intense the shake effect is.</param>
        private void StartShake(float duration, float magnitude)
        {
            // Ensure the original position is updated before starting the shake
            originalPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -10);

            // Set the shake duration and intensity
            shakeDuration = duration;
            shakeMagnitude = magnitude;
        }

        /// <summary>
        /// Public method to trigger a camera shake using predefined duration and magnitude.
        /// </summary>
        public void ShakeReaction()
        {
            StartShake(actualShakeDuration, actualShakeMagnitude);
        }
    }
}
