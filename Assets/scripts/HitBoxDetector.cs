using UnityEngine;

public class HitBoxDetector : MonoBehaviour
{
    const string attack1 = "attack1";
    const string attack2 = "attack2";
    const string superPowerAttack = "superpowerattack";

    int health = 5;

    Animator animator;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(attack1))
        {
            health--;
            Debug.Log("Player hit with Attack1");
            Invoke("hurtanimation", 0.6f);
        }
        else if (collision.CompareTag(attack2))
        {
            health--;
            Debug.Log("Player hit with Attack2");
            Invoke("hurtanimation", 0.5f);
        }
        else if (collision.CompareTag(superPowerAttack))
        {
            health--;
            Debug.Log("Player hit with Super Power Attack");
            Invoke("hurtanimation", 0.3f);
        }

        if (health <= 0)
        {
            
            animator.SetTrigger("IsDead");
            Invoke("OnDestroy", 2f);
        }
        Debug.Log("Enemy Health: " + health);
    }
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void hurtanimation()
    {
        animator.SetTrigger("Ishurt");
    }

    void OnDestroy()
    {
        Destroy(gameObject);
    }
}
