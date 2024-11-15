using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotDogStore : MonoBehaviour, IInteractable
{
    [SerializeField] int healAmount = 5;

    [SerializeField] private GameObject caixaDeDialogo;

    [SerializeField] private GameObject textoPedido;
    [SerializeField] private GameObject textoObrigado;    

    public T Interact<T>() where T : class
    {
        textoPedido.SetActive(false);
        textoObrigado.SetActive(true);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.heal);

        return this as T;
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
