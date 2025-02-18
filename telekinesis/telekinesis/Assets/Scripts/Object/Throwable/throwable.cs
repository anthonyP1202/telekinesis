using Unity.VisualScripting;
using UnityEngine;

public class throwable : MonoBehaviour
{
    [Header("reference")]
    [SerializeField] public GameObject indicator;
     private bool isTargeted = false;

    public void Targeted( bool targetedIsIt)
    {
        isTargeted = targetedIsIt;
    }

    public void Update()
    {
        if (!isTargeted)
        {
            indicator.GetComponent<MeshRenderer>().enabled = false;
        }
        else 
        {
            indicator.GetComponent<MeshRenderer>().enabled = true;
            indicator.transform.position = new Vector3(x:this.transform.position.x, y: this.transform.position.y + 1 + (Mathf.Sin(Time.time) * 0.05f ), z: this.transform.position.z);
        }
    }
}
