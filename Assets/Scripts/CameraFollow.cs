using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Player references")]
    public GameObject player;
    [Header("Camera properties")]
    public float spaceBetweenCamera = 5.0f;

    private Transform playerTransform;

    private void Start()
    {
        playerTransform = player.transform;
        // Ajust camera initial position
        transform.position = new Vector3(transform.position.x, playerTransform.position.y + spaceBetweenCamera, transform.position.z);
    }

    private void Update()
    {
        if (playerTransform.position.y + spaceBetweenCamera < transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, playerTransform.position.y + spaceBetweenCamera, transform.position.z);
        }
    }
}
