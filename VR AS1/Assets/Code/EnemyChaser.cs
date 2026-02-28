using UnityEngine;

public class EnemyChaser : MonoBehaviour
{
    public Transform playerTransform;   // 玩家位置（后面连接XR Origin）
    public float moveSpeed = 0.9f;      // 追踪速度（很慢，0.8米/秒）
    public float catchDistance = 0.8f;  // 多近算抓到玩家
    public float activateDelay = 5f;    // 游戏开始几秒后敌人开始移动
    
    private bool isActive = false;
    private float timer = 0f;
    
    void Start()
    {
    // 自动找到主摄像机，不用手动拖拽
    if (playerTransform == null)
        playerTransform = Camera.main.transform;
    
    Invoke("Activate", activateDelay);
    }

    void Update()
    {
        if (!isActive)
        {
            timer += Time.deltaTime;
            if (timer >= activateDelay)
                isActive = true;
            return;
        }
        
        if (playerTransform == null) return;
        
        // 只追踪水平方向，不会飞起来
        Vector3 targetPos = new Vector3(
            playerTransform.position.x,
            transform.position.y,
            playerTransform.position.z
        );
        
        // 朝玩家移动
        transform.position = Vector3.MoveTowards(
            transform.position, targetPos, moveSpeed * Time.deltaTime
        );
        
        // 面朝玩家
        transform.LookAt(new Vector3(
            playerTransform.position.x,
            transform.position.y,
            playerTransform.position.z
        ));
        
        // 检测是否抓到玩家
        float distance = Vector3.Distance(transform.position, playerTransform.position);
        if (distance <= catchDistance)
        {
            GameManager.Instance.PlayerCaught();
        }
    }
    
    // 重置敌人位置（玩家死亡后调用）
    public void ResetPosition(Vector3 startPos)
    {
        transform.position = startPos;
        isActive = false;
        timer = 0f;
        // 3秒后重新激活
        Invoke("Reactivate", 3f);
    }
    
    void Reactivate()
    {
        isActive = true;
    }
}