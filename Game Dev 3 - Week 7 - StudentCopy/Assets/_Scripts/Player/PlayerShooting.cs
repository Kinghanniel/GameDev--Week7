using UnityEngine;

namespace GameDevWithMarco.Player
{
    public class PlayerShooting : MonoBehaviour
    {
        [SerializeField] GameObject axe;
        [SerializeField] GameObject axePrefabToThrow;
        [SerializeField] Transform axeHoldingPosition;
        [SerializeField] float axeDistanceRange;
        bool amIIHoldingTheTheAxe = true;
        [SerializeField] float axeForce;


        // Update is called once per frame
        void Update()
        {
            ThrowCollectTheAxe();
        }

        private void ThrowCollectTheAxe()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (amIIHoldingTheTheAxe == true)
                {
                    //Deactivate the axe
                    axe.SetActive(false);
                    //To keep track if I am holding the axe or not
                    amIIHoldingTheTheAxe = false;
                    //Spawns a new axe
                    var thrownAxe = Instantiate(axePrefabToThrow, axeHoldingPosition.transform.position, transform.rotation);
                    //Adds a rigidbody2d to it
                    Rigidbody2D rb = thrownAxe.GetComponent<Rigidbody2D>();
                    //Adds force to it
                    rb.AddForce(transform.right * axeForce * Time.deltaTime, ForceMode2D.Impulse);
                }
            }
        }
        public void RecoverAxe()
        {
            //Makes the bool true
            amIIHoldingTheTheAxe = true;
            //Reactivate the real axe
            axe.SetActive(true);
        }
    }
}
