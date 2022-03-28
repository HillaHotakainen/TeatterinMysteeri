using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeatterinMysteeri
{
    public class LightProjector : MonoBehaviour
    {
        public Transform vaaka;
        public Transform pysty; // eri valon suunnille
        private Vector3 pos = Vector3.zero;
        private Vector3 direction = new Vector3(1,0,0);
        private Vector3 targetPos = Vector3.zero;
        private int distance = 0;
        void Start()
        {
            pos = transform.position;
            distance = countDistance();
            Instantiate(vaaka, transform.position + direction, transform.rotation);
        }
        void Update()
        {
        
        }
        public int countDistance() {
            return 1;
        }
    }
}
