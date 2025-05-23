using UnityEngine;
using UnityEngine.Rendering;

public class Parallax : MonoBehaviour
{
    private float length, startpos;
    [SerializeField] private GameObject cameraObject;
    [SerializeField] private float parallaxEffect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float temp = cameraObject.transform.position.x * (1 - parallaxEffect);
        float dist = cameraObject.transform.position.x * parallaxEffect;
        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

        if (temp > startpos + length)
        {
            startpos += length;
        }
        else if (temp < startpos - length)
        {
            startpos -= length;
        }
    }
}
