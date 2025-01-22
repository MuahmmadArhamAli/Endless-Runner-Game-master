using System;
using UnityEngine;

public class Invincibility : MonoBehaviour
{
    public static event EventHandler OnPlayerEntered;
    private void OnTriggerEnter (){
        gameObject.SetActive(false);

        OnPlayerEntered?.Invoke(this, EventArgs.Empty);
    }
}
