using GameDevWithMarco.Singleton;
using UnityEngine;

namespace GameDevWithMarco.CameraStuff
{
    // CameraRippleEffect applies a ripple effect using a shader material
    // This class extends Singleton to ensure only one instance exists
    public class CameraRippleEffect : Singleton<CameraRippleEffect>
    {
        // Shader material responsible for the ripple effect
        public Material RippleMaterial;

        // Maximum ripple strength
        public float MaxAmount = 50f;

        // Friction determines how quickly the ripple effect fades (0 = no fade, 1 = instant stop)
        [Range(0, 1)]
        public float Friction = .9f;

        // Current ripple intensity
        private float Amount = 0f;

        // Ripple position in world space (public for external access)
        [HideInInspector] public Vector2 ripplePos;

        // Cached camera position
        private Vector3 pos;
        private UnityEngine.Camera cam;

        /// <summary>
        /// Start is called before the first frame update.
        /// It initializes the camera reference and calculates the initial ripple position.
        /// </summary>
        private void Start()
        {
            cam = GetComponent<UnityEngine.Camera>();
            pos = cam.WorldToScreenPoint(ripplePos);
        }

        /// <summary>
        /// Update is called once per frame.
        /// It continuously updates the ripple position and decreases the effect strength over time.
        /// </summary>
        void Update()
        {
            // Convert the world position of the ripple to screen space
            pos = cam.WorldToScreenPoint(ripplePos);

            // Apply the ripple strength to the shader
            this.RippleMaterial.SetFloat("_Amount", this.Amount);

            // Gradually reduce the ripple effect using friction
            this.Amount *= this.Friction;
        }

        /// <summary>
        /// Triggers a ripple effect at a specified position.
        /// </summary>
        /// <param name="ripplePosition">The world-space position where the ripple effect originates.</param>
        public void Ripple(Vector2 ripplePosition)
        {
            // Update the ripple position
            this.ripplePos = ripplePosition;

            // Reset the ripple effect to the maximum strength
            this.Amount = this.MaxAmount;

            // Convert position to screen space
            pos = cam.WorldToScreenPoint(ripplePos);

            // Update shader properties to position the ripple at the correct screen coordinates
            this.RippleMaterial.SetFloat("_CenterX", pos.x);
            this.RippleMaterial.SetFloat("_CenterY", pos.y);
        }

        /// <summary>
        /// Applies the ripple effect shader when rendering the camera image.
        /// </summary>
        /// <param name="src">The source render texture.</param>
        /// <param name="dst">The destination render texture.</param>
        void OnRenderImage(RenderTexture src, RenderTexture dst)
        {
            // Render the effect using the RippleMaterial
            Graphics.Blit(src, dst, this.RippleMaterial);
        }
    }
}
