using UnityEngine;

public class SafeZone : MonoBehaviour
{
    public EnemyChaser enemy;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 玩家进入安全区，敌人暂停
            enemy.moveSpeed = 0f;
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 玩家离开安全区，敌人恢复追踪
            enemy.moveSpeed = 0.8f;
        }
    }
}