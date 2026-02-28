using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTriggerZone : MonoBehaviour
{
    public string endSceneName = "EndScene";
    private bool isUnlocked = false;
    
    public void Unlock()
    {
        isUnlocked = true;
        Debug.Log("触发区已解锁，等待玩家进入");
    }
    
    void OnTriggerEnter(Collider other)
    {
        // 调试用：看是什么物体触碰了触发区
        Debug.Log($"触发区碰到: {other.name}, Tag: {other.tag}, 门解锁状态: {isUnlocked}");
        
        if (other.CompareTag("Player") && isUnlocked)
        {
            Debug.Log("加载EndScene...");
            SceneManager.LoadScene(endSceneName);
        }
    }
}