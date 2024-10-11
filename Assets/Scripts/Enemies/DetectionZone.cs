using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    Collider2D col;
    public List<Collider2D> detectedObjs = new List<Collider2D>();
    public string tagTarget = "Player";

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == tagTarget)
        {
            detectedObjs.Add(collision);
        }        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == tagTarget)
        {
            detectedObjs.Remove(collision);
        }
    }
}
