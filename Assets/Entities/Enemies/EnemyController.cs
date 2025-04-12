using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{
    private GameObject playerObject = null;
    private Vector2 playerPosition;

    [SerializeField] private float moveSpeed = 1.5f;
    [SerializeField] private bool moveEnabled = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (playerObject == null)
        {
            playerObject = GameObject.Find("Player");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = new Vector2(playerObject.transform.position.x, playerObject.transform.position.y);
        if (moveEnabled)
        {

        }
    }
}
