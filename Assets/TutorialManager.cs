using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{    
    public GameObject boss;
    public TextMeshProUGUI bossTextMeshPro;
    public Image cellphone;

    private void Start()
    {
        GameManager.OnGameStateChanged += OnGameChanged;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= OnGameChanged;
    }

    private void OnGameChanged(GameManager.GameState state)
    {
        if(state == GameManager.GameState.Introduction)
        {
            StartCoroutine(IntroductionRoutine());
        }
    }

    private IEnumerator IntroductionRoutine()
    {
        yield return new WaitForSeconds(1f);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.telefone);
        yield return new WaitForSeconds(2f);
        AudioManager.Instance.StopSFX();
        AudioManager.Instance.PlaySFX(AudioManager.Instance.telefonePickup);
        yield return new WaitForSeconds(1.5f);
        boss.gameObject.SetActive(true);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.cartoonTalking);
        yield return new WaitForSeconds(15f);
        AudioManager.Instance.StopSFX();
        AudioManager.Instance.PlaySFX(AudioManager.Instance.telefonePickup);
    }
}
