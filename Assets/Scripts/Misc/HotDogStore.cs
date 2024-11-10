using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotDogStore : MonoBehaviour, IInteractable
{
    public GameObject caixaDeDialogo;

    public GameObject textoPedido;
    public GameObject textoObrigado;
    public DamageableCharacter playerHealth;

    public GameObject Interact()
    {
        textoPedido.SetActive(false);
        textoObrigado.SetActive(true);
        playerHealth.Health += 5;

        return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            caixaDeDialogo.SetActive(true);
            textoPedido.SetActive(true);
        }        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            caixaDeDialogo.SetActive(false);
            textoPedido.SetActive(false);
            textoObrigado.SetActive(false);
        }        
    }
}
