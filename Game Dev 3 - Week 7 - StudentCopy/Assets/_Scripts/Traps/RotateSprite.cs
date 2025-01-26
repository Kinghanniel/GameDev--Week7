using UnityEngine;

namespace GameDevWithMarco.Traps
{
    public class RotateSprite : MonoBehaviour
    {
        // Set the rotation speed in degrees per second
        [SerializeField] float rotationSpeed = 30f;

        // Update is called once per frame
        void Update()
        {
            // Rotate the GameObject around the Z-axis
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
    }
}
