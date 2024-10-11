using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneArea : MonoBehaviour
{
    public CinemachineVirtualCamera VirtualCamera;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && VirtualCamera != null)
        {
            VirtualCamera.Follow = transform;            
        }
    }
}
