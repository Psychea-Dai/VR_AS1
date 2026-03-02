using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [Header("Collection")]
    public int totalItems = 3;
    private int collectedCount = 0;
    
    [Header("Scene door")]
    public GameObject doorLight;
    public GameObject doorBlocker;      // 门的实体（阻挡玩家的Collider）
    public DoorTriggerZone doorTrigger; // 直接引用DoorTriggerZone脚本
    public FloatingHUD hud;
    
    [Header("Enemy and relive")]
    public EnemyChaser enemy;
    public Transform enemyStartPosition;
    public Transform respawnPoint;
    public Transform playerBody;
    
    [Header("Sound")]
    public AudioClip ambienceSource;
    public AudioClip caughtSound;
    
    void Awake()
    {
        collectedCount = 0;
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    
    public void CollectItem()
    {
        collectedCount++;
        Debug.Log($"已收集 {collectedCount}/{totalItems}");
        
        if (hud != null) hud.UpdateCount(collectedCount, totalItems);
        
        if (collectedCount >= totalItems)
            UnlockDoor();
    }
    
    void UnlockDoor()
    {
        Debug.Log("门已解锁！");
        
        // 禁用门的实体碰撞，玩家可以走过去
        if (doorBlocker != null)
            doorBlocker.GetComponent<Collider>().enabled = false;
        else
            Debug.LogWarning("doorBlocker 未赋值！");
        
        // 解锁触发区（保持其Collider开启）
        if (doorTrigger != null)
            doorTrigger.Unlock();
        else
            Debug.LogWarning("doorTrigger 未赋值！");
    }
    
    public void PlayerCaught()
{
    Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
    if (caughtSound != null && playerBody != null)
        AudioSource.PlayClipAtPoint(caughtSound, playerBody.position);

    if (caughtSound != null && playerBody != null)
        AudioSource.PlayClipAtPoint(caughtSound, playerBody.position);

    // 传送玩家
    if (playerBody != null && respawnPoint != null)
    {
        playerBody.position = respawnPoint.position;
    }

    // 重置敌人
    if (enemy != null && enemyStartPosition != null)
        enemy.ResetPosition(enemyStartPosition.position);

    if (hud != null) hud.ShowMessage("You've been caught.");
}
    
    public void GoToNextScene()
    {
        SceneManager.LoadScene(1);
    }
}