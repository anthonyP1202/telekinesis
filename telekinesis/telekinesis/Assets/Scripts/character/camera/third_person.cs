using UnityEngine;

public class third_person : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("references")]
    [SerializeField]public controller controller;
    [SerializeField]public camera_anchor anchor;

    [Header("settings")]
    [SerializeField] public float sensitivity = 1.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    private Vector2 pitchLimit = new Vector2(-40.0f, 80.0f);

    private void Start()
    {
        if (sensitivity <= 0)
        {
            sensitivity = 1.0f;
        }
    }

    private void Update()
    {

        yaw += controller.looking.x * sensitivity;

        pitch += controller.looking.y * sensitivity;
        pitch = Mathf.Clamp(pitch, pitchLimit.x, pitchLimit.y);

        controller.player.transform.rotation = Quaternion.Euler(0, yaw, 0);
        anchor.transform.localRotation = Quaternion.Euler(pitch, 0, 0);

        //playerController.character.transform.rotation = Quaternion.Euler(0, yaw, 0);
        //playerCamera.transform.localRotation = Quaternion.Euler(pitch, 0, 0);
    }
}
