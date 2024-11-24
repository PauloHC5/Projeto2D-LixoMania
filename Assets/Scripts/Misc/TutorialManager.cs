using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{    
    public GameObject boss;
    public TextMeshProUGUI bossTextMeshPro;    

    [SerializeField] private GameObject player;

    [SerializeField] private BossDictionary bossDictionary;


    [Header("Listeners")]
    public GameEventListener playerHealthEventListener;

    public GameEventListener trashDumpAccumulatedEventListener;

    public GameEventListener polutionHighEventListener;

    private Dictionary<string, string> bossTexts;

    private bool breakRoutine = false;

    private void Start()
    {
        GameManager.OnGameStateChanged += OnGameChanged;

        bossTexts = bossDictionary.ToDictionary();
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
        else if(state == GameManager.GameState.Victory)
        {
            StartCoroutine(VictoryRoutine());
        }
        else if(state == GameManager.GameState.Restart)
        {
            boss.gameObject.SetActive(false);
        }
        else if(state == GameManager.GameState.Defeat)
        {
            if(GameManager.Instance.deathReason != GameManager.DeathReason.None) StartCoroutine(DefeatRoutine());
        }
    }

    private IEnumerator IntroductionRoutine()
    {        
        yield return new WaitForSeconds(1f);        
        AudioManager.Instance.PlaySFX(AudioManager.Instance.telefone);
        yield return new WaitForSeconds(1.5f);
        AudioManager.Instance.StopSFX();        
        AudioManager.Instance.PlaySFX(AudioManager.Instance.telefonePickup);
        yield return new WaitForSeconds(1.5f);
        boss.gameObject.SetActive(true);
        bossTextMeshPro.text = bossTexts["Introduction pt1"];
        AudioManager.Instance.PlaySFX(AudioManager.Instance.cartoonTalking);
        yield return new WaitForSeconds(5f);
        bossTextMeshPro.text = bossTexts["Introduction pt2"];
        yield return new WaitForSeconds(7f);
        bossTextMeshPro.text = bossTexts["Introduction pt3"];
        yield return new WaitForSeconds(7f);
        bossTextMeshPro.text = bossTexts["Introduction pt4"];
        yield return new WaitForSeconds(2f);
        AudioManager.Instance.StopSFX();
        yield return new WaitForSeconds(5f);        
        AudioManager.Instance.PlaySFX(AudioManager.Instance.telefonePickup);        
        boss.gameObject.SetActive(false);
    }

    public void HealthTutorial()
    {
        if(!breakRoutine)
        {
            StartCoroutine(TutorialRotine("Heal"));
            StartCoroutine(TutorialBreakRoutine());
            playerHealthEventListener.enabled = false;
        }        
    }    

    public void TrashDumpTutorial()
    {
        if (!breakRoutine)
        {
            StartCoroutine(TutorialRotine("TrashDump"));
            StartCoroutine(TutorialBreakRoutine());
            trashDumpAccumulatedEventListener.enabled = false;
        }
    }      

    public void PolutionTutorial()
    {
        if (!breakRoutine)
        {
            StartCoroutine(TutorialRotine("Polution"));
            StartCoroutine(TutorialBreakRoutine());
            polutionHighEventListener.enabled = false;
        }
    }

    private IEnumerator TutorialRotine(string tutorialSubject)
    {
        boss.gameObject.SetActive(true);
        bossTextMeshPro.text = bossTexts[tutorialSubject];
        AudioManager.Instance.PlaySFX(AudioManager.Instance.cartoonTalking);
        yield return new WaitForSeconds(10f);
        AudioManager.Instance.StopSFX();
        AudioManager.Instance.PlaySFX(AudioManager.Instance.telefonePickup);
        boss.gameObject.SetActive(false);
    }

    private IEnumerator TutorialBreakRoutine()
    {
        breakRoutine = true;
        yield return new WaitForSeconds(30f);
        breakRoutine = false;
    }

    private IEnumerator VictoryRoutine()
    {
        boss.gameObject.SetActive(true);
        bossTextMeshPro.text = bossTexts["Victory"];
        AudioManager.Instance.PlaySFX(AudioManager.Instance.cartoonTalking);
        yield return new WaitForSeconds(10f);
        AudioManager.Instance.StopSFX();        
    }
    private IEnumerator DefeatRoutine()
    {
        boss.gameObject.SetActive(true);        
        bossTextMeshPro.text = bossTexts[GameManager.Instance.deathReason.ToString()];
        AudioManager.Instance.PlaySFX(AudioManager.Instance.cartoonTalking);
        yield return new WaitForSeconds(10f);
        AudioManager.Instance.StopSFX();        
    }
}

[Serializable]
public class BossDictionary
{
    [SerializeField] BossDictionaryItem[] bossDictionaryItems;

    public Dictionary<string, string> ToDictionary()
    {
        Dictionary<string, string> newDict = new Dictionary<string, string>();

        foreach(var item in bossDictionaryItems)
        {
            newDict.Add(item.subject, item.text);
        }

        return newDict;
    }
}

[Serializable]
public class BossDictionaryItem
{
    [SerializeField] public string subject;
    [SerializeField] public string text;
}
