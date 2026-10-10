using System.Collections;
using UnityEngine;

public class ClashnRecover : MonoBehaviour
{
    public int clashThreshold = 10;
    [SerializeField] float clashDuration = 1.5f;
    [SerializeField] float hitWindow = 3f;      
    [SerializeField] float pushDistance = 1f;
    [SerializeField] float pushTime = 0.2f;
    [SerializeField] playerHealth player;

    int playerCount, enemyCount;
    float lastHitTime;
    public bool isClash;

    public void ReportHit(bool fromPlayer, GameObject enemyObj)
    {
        if (isClash || enemyObj == null) return;

        if (Time.time - lastHitTime > hitWindow)
        {
            playerCount = 0;
            enemyCount = 0;
        }
        lastHitTime = Time.time;

        if (fromPlayer) playerCount++;
        else enemyCount++;

        if (playerCount + enemyCount >= clashThreshold)
            StartCoroutine(HandleClash(enemyObj));
    }

    IEnumerator HandleClash(GameObject enemyObj)
    {
        isClash = true;

        Health boss = enemyObj.GetComponentInParent<Health>();
        AdvancedEnemyAiHealth adv = enemyObj.GetComponentInParent<AdvancedEnemyAiHealth>();
        EnemyHealth early = enemyObj.GetComponentInParent<EnemyHealth>();

        player.playerCombat.LockActions(clashDuration);
        player.playerMovement.LockMovement(clashDuration);

        if (boss != null)
        {
            boss.bt.LockAction(true);  
            boss.bossai.LockMovement(clashDuration);
        }
        else if (adv != null) adv.enemyScript.LockAction(clashDuration);
        else if (early != null) early.earlyEnemyAi.LockAction(clashDuration);

        bool playerDominant = playerCount > enemyCount;
        bool enemyDominant = enemyCount > playerCount;

        float side = Mathf.Sign(enemyObj.transform.position.x - player.transform.position.x);
        Vector2 dir = new Vector2(side, 0f);
        Vector3 playerMove = (!enemyDominant) ? (Vector3)(-dir * pushDistance) : Vector3.zero;
        Vector3 enemyMove = (!playerDominant) ? (Vector3)(dir * pushDistance) : Vector3.zero;

        float t = 0f;
        Vector3 pStart = player.transform.position;
        Vector3 eStart = enemyObj.transform.position;
        while (t < pushTime)
        {
            t += Time.deltaTime;
            boss.bt.SetColliderOff();
            float k = Mathf.Clamp01(t / pushTime);
            if (player != null) player.transform.position = pStart + playerMove * k;
            if (enemyObj != null) enemyObj.transform.position = eStart + enemyMove * k;
            yield return null;
        }

        yield return new WaitForSeconds(Mathf.Max(0f, clashDuration - pushTime));

        if (boss != null) boss.bt.LockAction(false);

        playerCount = 0;
        enemyCount = 0;
        isClash = false;
    }
}