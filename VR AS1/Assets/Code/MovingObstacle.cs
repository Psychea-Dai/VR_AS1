using UnityEngine;
using UnityEngine.SceneManagement; 

public class MovingObstacle : MonoBehaviour
{
    public float speed = 3f;
    public float distance = 5f;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position; // 记住开始的位置
    }
    
    void Update()
    {
        // 让物体在 X 轴上来回移动 (PingPong效果)
        float move = Mathf.PingPong(Time.time * speed, distance);
        transform.position = new Vector3(startPos.x + move, startPos.y, startPos.z);
        }
    void OnTriggerEnter(Collider other)
    {
        // 只要碰到的东西是玩家（Tag 是 Player）
        if (other.CompareTag("Player"))
        {
            Debug.Log("传送门被触发了！正在前往 Scene_Puzzle..."); // 这句会在控制台显示，帮你调试
            SceneManager.LoadScene("Scene_Puzzle");
        }
    }


}