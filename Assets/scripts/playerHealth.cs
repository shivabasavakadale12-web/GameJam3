using UnityEngine;

public class playerHealth : MonoBehaviour
{
    
    const string enemyattack1 = "enemyattack1";

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(enemyattack1))
        {
            Debug.Log("Player hit with Enemy Attack1");
        }
    }
}
