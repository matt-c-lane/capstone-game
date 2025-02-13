using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float FollowSpeed = 2f;
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        if (target != null) // Ensure target exists to prevent errors
        {   // camera speed
            Vector3 newPos = new Vector3(target.position.x, target.position.y, -10f);
            transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);
        }
    }
}
