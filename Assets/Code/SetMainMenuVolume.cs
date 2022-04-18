using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeatterinMysteeri
{
    public class SetMainMenuVolume : MonoBehaviour
    {
        void Start()
        {
            StartCoroutine("DisableAfterOneFrameAndHopeItsNotTooVisible");
        }

        IEnumerator DisableAfterOneFrameAndHopeItsNotTooVisible()
        {
            yield return new WaitForSeconds(1.0f);
            Destroy(gameObject);
        }
    }
}
