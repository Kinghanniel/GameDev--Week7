using UnityEngine;

namespace GameDevWithMarco.Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        Animator anim;
        Vector2 movement;
        [SerializeField] GameObject handsController;

        private void Awake()
        {
            anim = GetComponent<Animator>();
        }

        private void Update()
        {
            movement.x = Input.GetAxis("Horizontal");
            movement.y = Input.GetAxis("Vertical");
            anim.SetFloat("Speed", movement.sqrMagnitude);
        }

        public void PlayerHit()
        {
            anim.SetTrigger("Hit");
        }
    }
}
