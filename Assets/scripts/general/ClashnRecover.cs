using System.Collections;
using UnityEngine;

public class ClashnRecover : MonoBehaviour
{
    playerHealth player;

    Health bossEnemy;
    AdvancedEnemyAiHealth advEnemy;
    EnemyHealth earlyEnemy;

    bool isClash = false;

    GameObject currentEnemy;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")
            .GetComponent<playerHealth>();

        var bossObj = GameObject.FindGameObjectsWithTag("BossEnemy");
        if (bossObj.Length > 0)
            bossEnemy = bossObj[0].GetComponent<Health>();

        var advObj = GameObject.FindGameObjectsWithTag("AdvancedEnemy");
        if (advObj.Length > 0)
            advEnemy = advObj[0].GetComponent<AdvancedEnemyAiHealth>();

        var earlyObj = GameObject.FindGameObjectsWithTag("EarlyEnemy");
        if (earlyObj.Length > 0)
            earlyEnemy = earlyObj[0].GetComponent<EnemyHealth>();
    }

    void Update()
    {
        if (isClash)
            return;

        if (player.StClash)
        {
            currentEnemy = player.gameObject;
            StartCoroutine(HandleClash());
        }
        else if (bossEnemy != null && bossEnemy.StClash)
        {
            currentEnemy = bossEnemy.gameObject;
            StartCoroutine(HandleClash());
        }
        else if (advEnemy != null && advEnemy.StClash)
        {
            currentEnemy = advEnemy.gameObject;
            StartCoroutine(HandleClash());
        }
        else if (earlyEnemy != null && earlyEnemy.StClash)
        {
            currentEnemy = earlyEnemy.gameObject;
            StartCoroutine(HandleClash());
        }
    }

    IEnumerator HandleClash()
    {
        isClash = true;

        // Lock player
        player.playerCombat.LockActions(1.5f);
        player.playerMovement.LockMovement(1.5f);

        // Lock current enemy
        if (currentEnemy == bossEnemy?.gameObject)
        {
            bossEnemy.bt.LockAction(true);
            bossEnemy.bossai.LockMovement(1.5f);
        }
        else if (currentEnemy == advEnemy?.gameObject)
        {
            advEnemy.enemyScript.LockAction(1.5f);
        }
        else if (currentEnemy == earlyEnemy?.gameObject)
        {
            earlyEnemy.earlyEnemyAi.LockAction(1.5f);
        }

        Vector2 direction = currentEnemy.transform.position - player.transform.position;
        direction.Normalize();

        float pushDistance = 1f;

        player.transform.position -= (Vector3)(direction * pushDistance);
        currentEnemy.transform.position += (Vector3)(direction * pushDistance);

        yield return new WaitForSeconds(1.5f);

        if (currentEnemy == bossEnemy?.gameObject)
        {
            bossEnemy.bt.LockAction(false);
        }
        else if (currentEnemy == advEnemy?.gameObject)
        {
            advEnemy.enemyScript.LockAction(1.5f);
        }

        currentEnemy = null;
        isClash = false;
    }
}