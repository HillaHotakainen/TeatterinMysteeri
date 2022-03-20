using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeatterinMysteeri
{
    public class LockedDoor : MonoBehaviour
    {
        [SerializeField]
        private KeyHolder keyHolder;    //Tein näin, voisi varmaan tehdä myös FindObjectOfTypellä
        private void Update()
        {
            if(keyHolder.GotKey)        //Simppeli boolean check, jos keyholderilla on avain, tämä tuhoaa itsensä
            {
                Destroy(gameObject);
            }
        }
    }
}
