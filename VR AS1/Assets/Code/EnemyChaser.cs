using UnityEngine;
using System.Collections;

public class EnemyChaser : MonoBehaviour
{
    public Transform playerTransform;   // 玩家位置（后面连接XR Origin）
    public float moveSpeed = 0.9f;      // 追踪速度（很慢，0.8米/秒）
    public float catchDistance = 0.8f;  // 多近算抓到玩家
    public float activateDelay = 5f;    // 游戏开始几秒后敌人开始移动
    
     private bool isActive = false;
    private bool isCoolingDown = false;  // 新增：冷却中不再重复触发
    private float timer = 0f;
    private Vector3 spawnPosition;

    void Start()
    {
        spawnPosition = transform.position;  // 记住初始位置
    }

    void Update()
    {
        if (isCoolingDown) return;  // 冷却期间完全停止

        if (!isActive)
        {
            timer += Time.deltaTime;
            if (timer >= activateDelay)
                isActive = true;
            return;
        }

        if (playerTransform == null) return;

        Vector3 targetPos = new Vector3(
            playerTransform.position.x,
            transform.position.y,
            playerTransform.position.z
        );

        transform.position = Vector3.MoveTowards(
            transform.position, targetPos, moveSpeed * Time.deltaTime
        );

        transform.LookAt(new Vector3(
            playerTransform.position.x,
            playerTransform.position.y,
            playerTransform.position.z
        ));

        float distance = Vector3.Distance(new Vector3(transform.position.x,playerTransform.position.y,transform.position.z), playerTransform.position);
        if (distance <= catchDistance)
        {
            GameManager.Instance.PlayerCaught();
            StartCoroutine(CatchCooldown());
        }
    }

    IEnumerator CatchCooldown()
    {
        isCoolingDown = true;
        isActive = false;

        // 瞬间传回出生点
        transform.position = spawnPosition;

        yield return new WaitForSeconds(4f);  // 给玩家4秒缓冲

        isCoolingDown = false;
        isActive = true;
    }

    public void ResetPosition(Vector3 startPos)
    {
        StopAllCoroutines();
        transform.position = startPos;
        spawnPosition = startPos;
        isCoolingDown = false;
        isActive = false;
        timer = 0f;
        StartCoroutine(CatchCooldown());
    }
}