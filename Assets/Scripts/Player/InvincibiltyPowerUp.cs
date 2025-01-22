using System;
using System.Collections;
using UnityEngine;

public class InvincibiltyPowerUp : MonoBehaviour{

    [SerializeField] private PlayerController playerController;

    [SerializeField] private float invincibilityTime = 5f;
    private void Start(){
        Invincibility.OnPlayerEntered += Invincibilty_OnPlayerEntered;
    }

    private void Invincibilty_OnPlayerEntered( object sender, EventArgs e ){
        playerController.isInvincible = true;
        
        StartCoroutine(OnInvincible());
    }

    private IEnumerator OnInvincible(){
        yield return new WaitForSeconds(invincibilityTime);

        playerController.isInvincible = false;
    }

    private void OnDestroy(){
        Invincibility.OnPlayerEntered -= Invincibilty_OnPlayerEntered;
        StopCoroutine(OnInvincible());
    }
}
