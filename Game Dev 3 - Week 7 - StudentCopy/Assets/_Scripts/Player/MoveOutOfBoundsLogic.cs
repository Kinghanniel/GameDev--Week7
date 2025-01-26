using GameDevWithMarco.Managers;
using UnityEngine;

namespace GameDevWithMarco.Player
{
    public class MoveOutOfBoundsLogic : MonoBehaviour
    {
        Rigidbody2D rb;
        ScreenBounds screenBounds;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            screenBounds = FindObjectOfType<ScreenBounds>();
        }


        private void FixedUpdate()
        {
            if (rb != null)
            {
                OutOfBoundsLogic();
            }
        }

        //Will allow the axe to wrap around the screen if out of bounds
        private void OutOfBoundsLogic()
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
