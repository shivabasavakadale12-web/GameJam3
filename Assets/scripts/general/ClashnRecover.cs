using System.Collections;
using UnityEngine;

public class ClashnRecover : MonoBehaviour
{
    playerHealth player;
    Health bossEnemy;
    AdvancedEnemyAiHealth advEnemy;
    EnemyHealth earlyEnemy;

    bool isClash = false;

    void Start()
    {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<playerHealth>();

      var bossObj = GameObject.FindGameObjectsWithTag("BossEnemy");
      if (bossObj.Length > 0) bossEnemy = bossObj[0].GetComponent<Health>();

      var advObj = GameObject.FindGameObjectsWithTag("AdvancedEnemy");
      if (advObj.Length > 0) advEnemy = advObj[0].GetComponent<AdvancedEnemyAiHealth>();

      var earlyObj = GameObject.FindGameObjectsWithTag("EarlyEnemy");
      if (earlyObj.Length > 0) earlyEnemy = earlyObj[0].GetComponent<EnemyHealth>();
    }
    
    void Update()
    {
        if (isClash) return; 

        if (player.StClash || (bossEnemy != null && bossEnemy.StClash) ||
            (advEnemy != null && advEnemy.StClash) || (earlyEnemy != null && earlyEnemy.StClash))
        {
            StartCoroutine(HandleClash());
        }
    }

    IEnumerator HandleClash()
    {
        isClash = true;
        yield return new WaitForSeconds(1.5f);
        isClash = false;
    }
}
