using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Transform player;

    void Start()
    {
        // Find the player object by name
        player = GameObject.Find("Player").transform;

        // Enable the depth texture mode for the camera
        Camera.main.depthTextureMode = DepthTextureMode.Depth;
    }

    // LateUpdate is called once per frame after Update
    void LateUpdate()
    {
        if (player != null)
        {
            transform.position = new Vector3(player.position.x, player.position.y, -10);
        }
    }
}