using System.Collections;
using UnityEngine;

public class PowerAttackController : MonoBehaviour
{
    PlayerMovement player;
    Rigidbody2D rb;
    Animator animator;
    bool counterAttackstart = false;
    public bool IsCounterAttackRunning => counterAttackstart;
    Camera mc;
    IEnemy enemy;
    void Start()
    {
        player = FindAnyObjectByType<PlayerMovement>();
        mc = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }
    void OnEnable()
    {
        enemy = GetComponent<IEnemy>();

        if (enemy != null)
            enemy.CounterAttack += StartCounterAttack;
    }

    void OnDisable()
    {
        if (enemy != null)
            enemy.CounterAttack -= StartCounterAttack;
    }

    public void StartCounterAttack()
    {
        if (counterAttackstart) return;
        counterAttackstart = true;
        SnapToAttackPosition();
        StartCoroutine(PowerAttackRoutine());
    }

    public void SnapToAttackPosition()
    {
        Vector3 playerPosition = player.transform.position;

        float d = Random.Range(7f, 8f);

        float enemyX = playerPosition.x + d;
        rb.position = new Vector2(enemyX, rb.position.y);
    }

    IEnumerator PowerAttackRoutine()
    {
        Debug.Log("Enemy is using Power Attack!");

        animator.SetTrigger("PowerAttack");

        yield break;

    }
}
