using System.Numerics;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class controller : MonoBehaviour
{
    
    private Vector2 movement;
    public Vector2 looking;

    [Header("references")]
    [SerializeField]public player player;
    public Animator animator;
    private void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();
    }

    private void OnLook(InputValue value)
    {
        looking = value.Get<Vector2>();
    }

    public void OnInteract(InputValue value)
    {
        
        if (player.heldItem != null)
        {
            animator.Play("Punching");
            Task.Delay(7000);
            player.heldItem.GetComponent<throwable>().transform.parent = null;
            player.heldItem.GetComponent<Rigidbody>().isKinematic = false;
            player.heldItem.GetComponent<Rigidbody>().AddForce(new Vector3((player.heldItem.GetComponent<throwable>().transform.position.x - player.transform.position.x) * 5 , (player.heldItem.GetComponent<throwable>().transform.position.y - player.transform.position.y) * 5, (player.heldItem.GetComponent<throwable>().transform.position.z - player.transform.position.z) * 5), ForceMode.Impulse);
            player.heldItem = null;
            
        }
        else
        {
            if (player.raycastObject != null)
            {
                player.heldItem = player.raycastObject;
                player.heldItem.GetComponent<throwable>().GetComponent<Rigidbody>().isKinematic = true;
                player.heldItem.GetComponent<throwable>().transform.parent = player.transform;
                player.heldItem.GetComponent<throwable>().transform.localPosition = new Vector3(0.0f, 0.10f, 3.0f);
            }
        }
    }

    //public void OnInteract(InputValue value)
    //{
    //    
    //}

    private void Update()
    {
        Vector3 right = movement.x * player.transform.right ;
        Vector3 forward = movement.y * player.transform.forward ;
        Vector3 velocity = right + forward ;
        animator.SetFloat("speed", forward.magnitude);
        player.Move(velocity * Time.deltaTime);
    }
}
