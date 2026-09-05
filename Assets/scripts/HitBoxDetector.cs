using UnityEngine;

public class HitBoxDetector : MonoBehaviour
{
    const string attack1 = "attack1";
    const string attack2 = "attack2";
    const string superPowerAttack = "superpowerattack";
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(attack1))
        {
            Debug.Log("Player hit with Attack1");
        }
        else if(collision.CompareTag(attack2))
        {
            Debug.Log("Player hit with Attack2");
        }
        else if(collision.CompareTag(superPowerAttack))
        {
            Debug.Log("Player hit with Super Power Attack");
        }
    }
}
