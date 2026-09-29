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
    }

    public void DisableCurrentHitbox()
    {
        bt.isAttacking = false;
        bt.HitBox[bt.CurrentHitboxIndex].enabled = false;
    }
}