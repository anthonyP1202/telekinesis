using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;
using static UnityEngine.UI.Image;

public class player : MonoBehaviour
{
    [Header("references")]
    [SerializeField] public Rigidbody rb;

    [HideInInspector] public Object raycastObject;
    [HideInInspector] public Object heldItem;

    public void Move(Vector3 velocity)
    {
        rb.MovePosition(rb.position + velocity*5);
        //if (heldItem != null)
        //{
        //    heldItem.transform.localPosition = new Vector3(rb.transform.position.x, rb.transform.position.y, rb.transform.position.z);
        //}
    }

    public void Update()
    {
        RaycastHit hit;
        Vector3 raycastOrigin = new Vector3(this.transform.position.x, this.transform.position.y - 0.8f, this.transform.position.z);


        if (Physics.Raycast(raycastOrigin, transform.forward, out hit, 10.0f))
        {
            Debug.DrawRay(raycastOrigin, transform.forward * hit.distance, Color.red);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.GetComponent<throwable>())
                {
                    if (hit.collider.gameObject != heldItem)
                    {
                        if (raycastObject != null)
                        {
                            raycastObject.GetComponent<throwable>().Targeted(false);
                        }
                        raycastObject = hit.collider.gameObject;
                        raycastObject.GetComponent<throwable>().Targeted(true);
                    } else
                    {
                        raycastObject.GetComponent<throwable>().Targeted(false);
                    }
                }
                else if (raycastObject != null)
                {
                    raycastObject.GetComponent<throwable>().Targeted(false);
                    raycastObject = null;
                }
                else
                {
                    raycastObject = null;
                }
            }
        }
        else {
            if (raycastObject)
            {
                raycastObject.GetComponent<throwable>().Targeted(false);
            }
            raycastObject = null;
            Debug.DrawRay(raycastOrigin, transform.forward * 5, Color.green);
        }
    }
}
