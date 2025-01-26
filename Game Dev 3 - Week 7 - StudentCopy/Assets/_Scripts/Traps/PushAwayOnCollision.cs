using DG.Tweening;
using GameDevWithMarco.CameraStuff;
using GameDevWithMarco.Managers;
using GameDevWithMarco.ObserverPattern;
using UnityEngine;

namespace GameDevWithMarco.Traps
{
    public class PushAwayOnCollision : MonoBehaviour
    {
        [SerializeField] float distanceFromCollision;
        //Tweening variables
        [SerializeField] float pushBackTime;
        [SerializeField] Ease pushBackEase;
        [SerializeField] float scaleBounceTime;
        [SerializeField] Vector3 scalePunchStrenght;

        //To Raise the Collided with trap event
        [SerializeField] GameEvent collidedWithTrap;


        private void OnCollisionEnter2D(Collision2D collision)
        {
            //Creates a local variable of type Rigidbody2D and initialises it
            Rigidbody2D rb = new Rigidbody2D();
            //Gets the rigid body of the collision object
            rb = collision.gameObject.GetComponent<Rigidbody2D>();
            //Raises the event
            collidedWithTrap.Raise();

            //---Push Back---
            //Gets direction between this and collision object
            Vector2 direction = (gameObject.transform.position - collision.gameObject.transform.position).normalized;
            // Calculate the position a set distance away from the collision point
            Vector3 newPosition = collision.contacts[0].point + -direction * distanceFromCollision;
            //Lerps the collision game object in that position
            collision.gameObject.transform.DOMove(newPosition, pushBackTime).SetEase(pushBackEase);

            //---Scale Bounce---
            collision.gameObject.transform.DOPunchScale(scalePunchStrenght, scaleBounceTime);

            //---Ripple Effect---
            CameraRippleEffect.Instance.Ripple(collision.contacts[0].point);

            VfxManager.Instance.HitStop(0.25f);

        }
    }
}
