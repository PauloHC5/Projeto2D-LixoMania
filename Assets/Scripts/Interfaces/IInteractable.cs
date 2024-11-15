using UnityEngine;

public interface IInteractable
{
    public T Interact<T>() where T : class;
}