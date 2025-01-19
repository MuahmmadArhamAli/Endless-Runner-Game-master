using System;
using UnityEngine;

public class Invincibility : MonoBehaviour
{
    public static event EventHandler OnPlayerEntered;
    private void OnTriggerEnter (){
        OnPlayerEntered?.Invoke(this, EventArgs.Empty);
    }
}
