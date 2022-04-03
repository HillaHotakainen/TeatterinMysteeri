using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TeatterinMysteeri
{
    public class Mirror : MonoBehaviour, IPointerDownHandler
    {
        public Transform vaaka;
        public Transform pysty;
        public Transform kulma;
        public Sprite koillinen;
        public Sprite kaakko;
        public Sprite lounas;
        public Sprite luode;
        [SerializeField] private string direction = "nw"; // ilmansuunta, nw/ne/sw/se
        private Vector3 sideVer;
        private Vector3 sideHor;
        private RaycastHit2D closestCollider;
        private GameObject beam;
        private GameObject hero;
        private GameObject valokulma;
        private bool reflectingHor = false;
        private bool reflectingVer = false;
        private bool closeEnough = false;
        private SpriteRenderer spr;
        private SpriteRenderer kulmaSpr;
        void Start()
        {
            spr = gameObject.GetComponent<SpriteRenderer>();
            hero = GameObject.Find("Hero");
            valokulma = Instantiate(kulma, transform.position, Quaternion.identity).gameObject;
            DontDestroyOnLoad(valokulma);
            kulmaSpr = valokulma.GetComponent<SpriteRenderer>();
            kulmaSpr.enabled = false;
        }
        void Update()
        {
            // joskus elämä on rankkaa
            switch (direction) {
                case "nw":
                    sideVer = Vector3.up;
                    sideHor = Vector3.left;
                    spr.sprite = luode;
                    spr.sortingOrder = 3;
                    valokulma.transform.rotation = Quaternion.Euler(0, 0, 180);
                    break;
                case "ne":
                    sideVer = Vector3.up;
                    sideHor = Vector3.right;
                    spr.sprite = koillinen;
                    spr.sortingOrder = 3;
                    valokulma.transform.rotation = Quaternion.Euler(0, 0, 90); 
                    break;
                case "sw":
                    sideVer = Vector3.down;
                    sideHor = Vector3.left;
                    spr.sprite = lounas;
                    spr.sortingOrder = 1;
                    valokulma.transform.rotation = Quaternion.Euler(0, 0, -90); 
                    break;
                case "se":
                    sideVer = Vector3.down;
                    sideHor = Vector3.right;
                    spr.sprite = kaakko;
                    spr.sortingOrder = 1;
                    valokulma.transform.rotation = Quaternion.Euler(0, 0, 0); 
                    break;
                default:
                    break;
            }

            // katsotaan onko oikeista suunnista tulossa valoa
            // pahoittelen noista erittäin rumista null checkeistä mutta unity
            var verCheck = Physics2D.OverlapPoint(transform.position + sideVer);
            var horCheck = Physics2D.OverlapPoint(transform.position + sideHor);
            
            if (verCheck != null) {
                var verTest = verCheck.GetComponent<LightBeamVertical>();
                if (verTest != null & verCheck.gameObject != beam) {
                    reflectingVer = true;
                } else {
                    reflectingVer = false;
                }
            } else {
                reflectingVer = false;
            }
            if (horCheck != null) {
                var horTest = horCheck.GetComponent<LightBeamHorizontal>();
                if (horTest != null & horCheck.gameObject != beam) {
                    reflectingHor = true;
                } else {
                    reflectingHor = false;
                }
            } else {
                reflectingHor = false;
            }

            // säteen kääntö ja vääntö skaba (luodaan kans semmonen jos ei oo jo ja tuhotaan jos ei vastaanota valoa)
            if (reflectingVer) {
                if (beam == null) {
                    beam = Instantiate(vaaka, transform.position + sideHor, Quaternion.identity).gameObject;
                    DontDestroyOnLoad(beam);
                }
                var light = LayerMask.GetMask("Light");
                closestCollider = Physics2D.Raycast(transform.position + sideHor/2, sideHor, 50f, ~light);
                beam.transform.position = new Vector2(transform.position.x + sideHor.x * closestCollider.distance/2 + sideHor.x/2, transform.position.y);
                beam.transform.localScale = new Vector2(closestCollider.distance, 1);
                kulmaSpr.enabled = true;
            } else if (beam != null & !reflectingHor) {
                Destroy(beam);
                kulmaSpr.enabled = false;
            }
            if (reflectingHor) {
                if (beam == null) {
                    beam = Instantiate(pysty, transform.position + sideVer, Quaternion.identity).gameObject;
                    DontDestroyOnLoad(beam);
                }
                var light = LayerMask.GetMask("Light");
                closestCollider = Physics2D.Raycast(transform.position + sideVer/2, sideVer, 50f, ~light);
                beam.transform.position = new Vector2(transform.position.x, transform.position.y + sideVer.y * closestCollider.distance/2 + sideVer.y/2);
                beam.transform.localScale = new Vector2(1, closestCollider.distance);
                kulmaSpr.enabled = true;
            } else if (beam != null & !reflectingVer) {
                Destroy(beam);
                kulmaSpr.enabled = false;
            }

            // tsekkaus onko Hero-objekti tarpeeks lähellä
            if (Vector3.Distance(transform.position, hero.transform.position) < 2) {
                closeEnough = true;
            }
            else {
                closeEnough = false;
            }
        }
        public void OnPointerDown(PointerEventData eventData) {
            // jos ollaa tarpeeks lähellä ni käännetää myötäpäivää =))))
            if (closeEnough) {
                switch (direction) {
                    case "nw":
                        direction = "ne";
                        break;
                    case "ne":
                        direction = "se";
                        break;
                    case "se":
                        direction = "sw";
                        break;
                    case "sw":
                        direction = "nw";
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
