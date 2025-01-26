using GameDevWithMarco.Singleton;
using GameDevWithMarco.Data;
using System.Collections;
using UnityEngine;

namespace GameDevWithMarco.Managers
{
    public class VfxManager : Singleton<VfxManager>
    {
        //to call the logic in other scripts
        public void HitStop(float stopDuration)
        {
            StartCoroutine(HitStopCoroutine(stopDuration));
        }

     

        //will stop the game for a set amount of time and then resume it
        IEnumerator HitStopCoroutine(float duration)
        {

            //stop the game copletely
            Time.timeScale = 0;

            //waits
            //needs to WaitForSecondsRealtime or the stop wil not work correctly
            yield return new WaitForSecondsRealtime(duration);

            //resumes the game
            Time.timeScale = 1;
        }
    }
}
