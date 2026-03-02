using UnityEngine;
using UnityEngine.InputSystem;

public class PickableDetector : MonoBehaviour
{
    public Transform rightHandTransform;

    public float detectRadius = 0.5f;

    public LayerMask pickableLayer;

    public InputAction aButtonAction;

    private bool isHolding = false;
    private GameObject holdingOBJ;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        aButtonAction.performed += ctx => OnAButtonPressed();
        aButtonAction.Enable();
    }

    void Oestroy()
    {
        if(aButtonAction != null)
        {
            aButtonAction.performed -= ctx => OnAButtonPressed();
            aButtonAction.Disable();
            aButtonAction.Dispose();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnAButtonPressed()
    {
        if (isHolding)
        {
            holdingOBJ.GetComponent<QueenPickup>().OnCancleDrag();
            isHolding = false;
            return;
        }


        Vector3 center = rightHandTransform.position;
        Collider[] hits = Physics.OverlapSphere(center, detectRadius, pickableLayer);

        if (hits == null || hits.Length == 0)
        {
            Debug.Log("没有找到 Pickable 物体。");
            return;
        }

        // 找最近的 collider（可根据需求改为第一个或按 tag/组件筛选）
        Collider nearest = hits[0];
        float nearestDistSqr = (nearest.transform.position - center).sqrMagnitude;
        for (int i = 1; i < hits.Length; i++)
        {
            float d = (hits[i].transform.position - center).sqrMagnitude;
            if (d < nearestDistSqr)
            {
                nearest = hits[i];
                nearestDistSqr = d;
            }
        }

        GameObject foundGO = nearest.gameObject;
        Debug.Log($"找到 Pickable 物体: {foundGO.name}", foundGO);
        foundGO.GetComponent<QueenPickup>().OnDragQueen(transform);
        isHolding = true;
        holdingOBJ = foundGO;
    }
}
