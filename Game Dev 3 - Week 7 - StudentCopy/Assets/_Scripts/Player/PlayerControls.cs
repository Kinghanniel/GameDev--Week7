using GameDevWithMarco.Managers;
using UnityEngine;


namespace GameDevWithMarco.Player
{
    public class PlayerControls : MonoBehaviour
    {
        public float moveSpeed = 5f;
        public float rotationSpeed = 200f; // Adjust the rotation speed as needed
        private Rigidbody2D rb;
        Vector2 movement;
        ScreenBounds screenBounds;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            screenBounds = FindObjectOfType<ScreenBounds>();
        }

        private void Update()
        {
            // Handle player input
            movement.x = Input.GetAxis("Horizontal");
            movement.y = Input.GetAxis("Vertical");
        }

        private void FixedUpdate()
        {
            OutOfBoundsLogic();
            CharacterRotationLogic();
        }

        private void CharacterRotationLogic()
        {
            // Calculate the rotation angle based on the movement direction
            if (movement != Vector2.zero)
            {
                float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
                // Smoothly rotate the game object towards the target rotation
                Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            }
        }

        private void OutOfBoundsLogic()
        {
            Vector2 currentPosition = rb.position;
            Vector2 newPosition = currentPosition + movement * moveSpeed * Time.fixedDeltaTime;
            if (screenBounds.AmIOutOfBounds(newPosition))
            {
                Vector2 newestPosition = screenBounds.CalculateWrappedPosition(newPosition);
                transform.localPosition = newestPosition;
            }
            else
            {
                rb.MovePosition(newPosition);
            }
        }

        private void MoveOutOfBoundsLogic()
        {
            Vector3 newVelocity = rb.velocity;
            Vector3 tempPosition = transform.localPosition + newVelocity * Time.deltaTime;
            if (screenBounds.AmIOutOfBounds(tempPosition))
            {
                Vector2 newPosition = screenBounds.CalculateWrappedPosition(tempPosition);
                transform.localPosition = newPosition;
            }
            else
            {
                rb.MovePosition(tempPosition);
            }
        }
    }
}

