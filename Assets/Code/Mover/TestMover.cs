using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeatterinMysteeri
{
    public class TestMover : MonoBehaviour
    {
        Vector2 targetPos = new Vector2 (1.5f, 0);
        Vector2 origPos;
        Vector2 currentPos;
        NPC_mover mover;
        void Start()
        {
            mover = GetComponent<NPC_mover>();
            StartCoroutine(TestMove());
        }

        IEnumerator TestMove()
        {
            origPos = transform.position;
            currentPos = origPos;
            while(true)
            {
                mover.MoveInput = targetPos - currentPos;
                currentPos = Vector2.MoveTowards(currentPos, targetPos, Time.deltaTime);
                mover.Speed = 1.0f;
                transform.position = currentPos;
                if(currentPos == targetPos)
                {
                    mover.Speed = 0f;
                    yield return new WaitForSeconds(2.0f);
                    targetPos = origPos;
                    origPos = transform.position;
                }
                yield return null;
            }
        }
    }
}
