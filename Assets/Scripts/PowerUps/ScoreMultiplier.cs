using UnityEngine;
using System;

public class ScoreMultiplier : MonoBehaviour{

    public static  event EventHandler OnPlayerEntered;
    private void OnTriggerEnter (){
        OnPlayerEntered?.Invoke(this, EventArgs.Empty);
    }
    
}
