using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCPlayerInteraction : DamageableCharacter
{
    public List<string> npcTexts;

    [SerializeField] private GameObject textBox;    

    private TextMeshProUGUI npcTextMeshPro;
    private NPC npc;

    private Vector3 textOriginalScale;    
    private int lastSortedNumber = 0;

    private IEnumerator textBoxRoutine;

    private void Start()
    {
        npcTextMeshPro = GetComponentInChildren<TextMeshProUGUI>();
        npc = GetComponent<NPC>();  
        if(npcTextMeshPro) textOriginalScale = npcTextMeshPro.transform.localScale;        

        textBox.SetActive(false);
    }

    private void Update()
    {
        FlipTextRelativeToTheParent();
    }

    public override void OnHit(float damage, Vector2 knockback)
    {
        if(textBoxRoutine != null) StopCoroutine(textBoxRoutine);
        textBoxRoutine = TextBoxRoutine();
        StartCoroutine(textBoxRoutine);
        npc.StunNPC();
        base.OnHit(damage, knockback);
    }

    private string ChooseRandomText()
    {
        if (npcTexts.Count != 0)
        {
            int randomTextIndex = Random.Range(0, npcTexts.Count);
            lastSortedNumber = randomTextIndex;
            while (randomTextIndex == lastSortedNumber)
            {
                randomTextIndex = Random.Range(0, npcTexts.Count);
            }

            return npcTexts[randomTextIndex];
        }

        return "Vc nao colocou texto pre definido";
    }

    private void FlipTextRelativeToTheParent()
    {
        if (npcTextMeshPro == null) return;

        Vector3 textFlippedScale = npcTextMeshPro.transform.localScale = new Vector3(-textOriginalScale.x, textOriginalScale.y, textOriginalScale.z);

        if (npc.IsNPCRotated) npcTextMeshPro.transform.localScale = textFlippedScale;
        else npcTextMeshPro.transform.localScale = textOriginalScale;
    }

    private IEnumerator TextBoxRoutine()
    {
        textBox.SetActive(true);
        npcTextMeshPro.text = ChooseRandomText();
        yield return new WaitForSeconds(npc.StunTime);
        textBox.SetActive(false);        

    }
}
