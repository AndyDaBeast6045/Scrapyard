using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private float cameraSpeed = 1.0f;
    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(1, 0, 0) * cameraSpeed *  Time.deltaTime;
    }
}
