using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeLeafs : MonoBehaviour
{
    public float targetAlpha = 25f;
    bool Fade = false;
    public List<SpriteRenderer> renderers;    

    // Start is called before the first frame update
    void Start()
    {
        renderers = new List<SpriteRenderer>(GetComponentsInChildren<SpriteRenderer>());
    }

    // Update is called once per frame
    void Update()
    {        
        foreach (SpriteRenderer renderer in renderers)
        {
            if (Fade)
            {
                renderer.color = new Color(
                    renderer.color.r,
                    renderer.color.g, renderer.color.b,
                    Mathf.MoveTowards(renderer.color.a, targetAlpha / 100, Time.deltaTime));
            }
            else
            {
                renderer.color = new Color(
                    renderer.color.r,
                    renderer.color.g, renderer.color.b,
                    Mathf.MoveTowards(renderer.color.a, 1f, Time.deltaTime));
            }
        }                    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Fade = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Fade = false;
    }
}
