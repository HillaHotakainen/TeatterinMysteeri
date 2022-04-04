using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeatterinMysteeri
{
    public class LightDoor : MonoBehaviour
    {
        [SerializeField] Sprite open;
        [SerializeField] Sprite closed;
        public List<LightReceiver> receivers = new List<LightReceiver>();
        private bool unlocked;
        private List<bool> receiverStatuses = new List<bool>();
        private Collider2D hitbox;
        private SpriteRenderer spr;
        void Start()
        {
            spr = GetComponent<SpriteRenderer>();
            hitbox = GetComponent<Collider2D>();
        }
        void Update()
        {
            // tarkistaa kaikkien oveen liitettyjen vastaanottimien tilan
            // jos yksikin ei saa valoa, ovi on kiinni
            receiverStatuses.Clear();
            for(int i = 0; i < receivers.Count; i++) {
                receiverStatuses.Add(receivers[i].receivingLight);
            }
            if (receiverStatuses.Contains(false)) {
                unlocked = false;
            } else {
                unlocked = true;
            }

            if (unlocked) {
                spr.sprite = open;
                hitbox.enabled = false;
            } else {
                spr.sprite = closed;
                hitbox.enabled = true;
            }
        }
    }
}
