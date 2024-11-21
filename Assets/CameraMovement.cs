using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private PixelPerfectCamera perfectCamera;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    private void Start()
    {
        perfectCamera.assetsPPU = 55;

        GameManager.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameManager.GameState state)
    {
        if(state == GameManager.GameState.Introduction)
        {
            StartCoroutine(StartIntroduction());
        }
    }    

    private IEnumerator StartIntroduction()
    {
        yield return new WaitForSeconds(3f);

        while (transform.position != new Vector3(transform.position.x, -20f, transform.position.z))
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, -20f, transform.position.z), moveSpeed * Time.deltaTime);            
            yield return null;
        }

        yield return new WaitForSeconds(3f);

        if (transform.position == new Vector3(transform.position.x, -20f, transform.position.z))
        {
            virtualCamera.Follow = GameObject.FindWithTag("Player").transform;
            perfectCamera.assetsPPU = 90;
            GameManager.Instance.UpdateGameState(GameManager.GameState.Start);            
        }
    }
}
