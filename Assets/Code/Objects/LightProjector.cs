using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeatterinMysteeri
{
    public class LightProjector : MonoBehaviour
    {
        public Transform vaaka;
        public Transform pysty;
        [SerializeField] private string direction = "right";
        private Vector2 dirVector;
        private RaycastHit2D closestCollider;
        private GameObject beam;
        void Start()
        {
            // isketään annetun suunnan mukaan säde käyntiin
            switch(direction) {
                case "right":
                    beam = Instantiate(vaaka, transform.position + new Vector3(1,0,0), Quaternion.identity).gameObject;
                    dirVector = new Vector2(1,0);
                    break;
                case "left":
                    beam = Instantiate(vaaka, transform.position + new Vector3(-1,0,0), Quaternion.identity).gameObject;
                    dirVector = new Vector2(-1,0);
                    break;
                case "up":
                    beam = Instantiate(pysty, transform.position + new Vector3(0,1,0), Quaternion.identity).gameObject;
                    dirVector = new Vector2(0,1);
                    break;
                case "down":
                    beam = Instantiate(pysty, transform.position + new Vector3(0,-1,0), Quaternion.identity).gameObject;
                    dirVector = new Vector2(0,-1);
                    break;
                default:
                    break;
            }
        }
        void Update()
        {
            // tää on täs ettei se valon raycast törmää itteensä
            var light = LayerMask.GetMask("Light");
            // katsotaan miten pitkällä on lähin collider valon suuntaan (maksimietäisyys 50)
            closestCollider = Physics2D.Raycast(transform.position, dirVector, 50f, ~light);

            // skaalataan ja siirretään valoa täyttämään koko etäisyys
            if (direction == "right" | direction == "left") {
                beam.transform.position = new Vector2(transform.position.x + dirVector.x * closestCollider.distance/2, transform.position.y);
                beam.transform.localScale = new Vector2(closestCollider.distance, 1);
            } else {
                beam.transform.position = new Vector2(transform.position.x, transform.position.y + dirVector.y * closestCollider.distance/2);
                beam.transform.localScale = new Vector2(1, closestCollider.distance);
            }
        }
    }
}
