using UnityEngine;

namespace GameDevWithMarco.Traps
{
    public class DestroyOnTouchWithAxe : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Axe")
            {
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
