using System.Collections;
using UnityEngine;

public class hitBoxController : MonoBehaviour
{
    BehaviourTree bt;

    void Start()
    {
        bt = GetComponent<BehaviourTree>();
    }

    public void EnableCurrentHitbox()
    {
        bt.HitBox[bt.CurrentHitboxIndex].enabled = true;
        StartCoroutine(TurnoffHitbox());
    }
    IEnumerator TurnoffHitbox()
    {
        yield return new WaitForSeconds(1);
        bt.HitBox[bt.CurrentHitboxIndex].enabled = false;
    }

    public void DisableCurrentHitbox()
    {
        bt.isAttacking = false;
        bt.HitBox[bt.CurrentHitboxIndex].enabled = false;
    }
}