using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeatterinMysteeri
{
    // koodia ei sais toistaa mut jaoin nää pysty/vaakaversioihin selkeyden takia
    // ja koska aivot räjähtää
    public class LightBeamHorizontal : MonoBehaviour
    {
        private Vector3 direction = new Vector3(1,0,0);
        private Vector3 oppositeDirection = new Vector3(-1,0,0);
        private Vector3 targetPos = Vector3.zero;
        private Collider2D front;
        private Collider2D behind;
        void Update()
        {
            // unity raivos jos en laittanu tätä null checkkiä
            if (Physics2D.OverlapPoint(transform.position + oppositeDirection) != null) {
                behind = Physics2D.OverlapPoint(transform.position + oppositeDirection);

                // tää on vähän syvältä, mut unity pisti täysillä hanttii tänki kanssa
                // listattu kaikki jutut mitkä voi olla takana ilman että säde hajoaa
                // sentään toimii (kai) =)=)=)=))asfdf
                var test1 = behind.GetComponent<LightBeamHorizontal>();
                var test2 = behind.GetComponent<LightProjector>();
                var test3 = behind.GetComponent<InputProcessor>();

                if (test1 != null | test2 != null | test3 != null) {
                } else {
                    Destroy(gameObject);
                }
            } else {
                Destroy(gameObject);
            }

            // tarkistetaan myös jatkuvasti et muodostuuko eteen tilaa
            front = Physics2D.OverlapPoint(transform.position + direction);
            if (front == null) {
                Extend();
            }
        }
        void Extend()
        {
            // monistaa itsensä seuraavalle tilelle
            targetPos = transform.position + direction;
            Instantiate(gameObject, targetPos, transform.rotation);
        }
        void OnTriggerEnter2D(Collider2D collider) {
            // jos laatikko tulee säteen päälle niin hajotetaan
            if (collider.tag == "Crate") {
                Destroy(gameObject);
            }
        }
    }
}
