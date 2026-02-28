
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class XR3dUI : MonoBehaviour
{
    public LayerMask castLayer;

    public Transform handTrans;
    private float raycastDist = 50;
    Transform hitTrans;

    public XRNode handRole = XRNode.LeftHand;
    bool triggerState = false;

    void Update()
    {
        InputDevices.GetDeviceAtXRNode(handRole).TryGetFeatureValue(CommonUsages.triggerButton, out bool trigger);

        if (trigger && !triggerState) // on trigger down
        {
            if (Physics.Raycast(handTrans.position, handTrans.forward, out RaycastHit hit, raycastDist, castLayer))
            {
                hitTrans = hit.transform;
                switch (hitTrans.name) //switch is like a more effeciant if else block
                {
                    case "Btn1":
                        //do something
                        print("Button 1");
                        break;
                    case "Btn2":
                        //do something
                        print("Button 2");
                        break;
                    default:
                        break;
                }
            }
        }
        triggerState = trigger;
    }

    void FixedUpdate()
    {
        if (Physics.Raycast(handTrans.position, handTrans.forward, out RaycastHit hit, raycastDist, castLayer))
        {
            if (hitTrans != null && hit.transform != hitTrans)
            {
                hitTrans.GetComponent<Image>().color = Color.white;
            }
            hitTrans = hit.transform;
            hitTrans.GetComponent<Image>().color = new Color(1, .5f, .5f);
        }
        else if (hitTrans != null)
        {
            hitTrans.GetComponent<Image>().color = Color.white;
            hitTrans = null;
        }
    }
}