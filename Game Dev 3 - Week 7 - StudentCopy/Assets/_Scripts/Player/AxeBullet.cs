using System.Collections;
using GameDevWithMarco.Managers;
using GameDevWithMarco.ObserverPattern;
using UnityEngine;

namespace GameDevWithMarco.Player
{
    public class AxeBullet : MonoBehaviour
    {
        Animator animator;
        Rigidbody2D rb;
        [SerializeField] GameEvent axeCollected;
        [SerializeField] GameEvent axeHit;
        bool haveIHitThetarget = false;
        ScreenBounds screenBounds;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            screenBounds = FindObjectOfType<ScreenBounds>();
            StartCoroutine(GrabbableAfterABit());
        }



        private void OnCollisionEnter2D(Collision2D collision)
        {
            //Stops the rotation
            animator.SetTrigger("HitTarget");
            //To keep track of logic
            haveIHitThetarget = true;
            //Raises the event
            axeHit.Raise();
            //Sestroys the rigid body to stop the axe
            Destroy(rb);

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player" && haveIHitThetarget)
            {
                //Raises the event to let the player know
                axeCollected.Raise();
                //Destroys this ones since we do not need it anymore
                Destroy(gameObject);
            }
        }

        IEnumerator GrabbableAfterABit()
        {
            //Waits for a bit
            yield return new WaitForSeconds(1.25f);
            //Makes the axe grabbable
            haveIHitThetarget = true;
        }

    }
}
