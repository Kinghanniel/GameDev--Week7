using UnityEngine;

namespace GameDevWithMarco.CameraStuff
{
    public class CameraShake : MonoBehaviour
    {
        private Vector3 originalPosition;
        private float shakeDuration = 0f;
        private float shakeMagnitude = 0.1f;
        private float dampingSpeed = 1.0f;

        [SerializeField] float actualShakeDuration;
        [SerializeField] float actualShakeMagnitude;

        private void Awake()
        {
            originalPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -10);
        }

        // Update is called once per frame
        void Update()
        {
            if (shakeDuration > 0)
            {
                transform.localPosition = originalPosition + Random.insideUnitSphere * shakeMagnitude;

                shakeDuration -= Time.deltaTime * dampingSpeed;
            }
            else
            {
                shakeDuration = 0f;
                transform.localPosition = originalPosition;
            }
        }

        private void StartShake(float duration, float magnitude)
        {
            originalPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -10);
            shakeDuration = duration;
            shakeMagnitude = magnitude;
        }

        public void ShakeReaction()
        {
            StartShake(actualShakeDuration, actualShakeMagnitude);
        }
    }
}
