using System;
using System.Collections;
using System.Security.Cryptography;
using UnityEngine;

public class InvincibiltyPowerUp : MonoBehaviour{

    [SerializeField] private PlayerController playerController;

    [SerializeField] private float invincibilityTime = 5f;

    private bool isInvincible;
    public float timer;
    private void Start(){
        timer = 0f;
        Invincibility.OnPlayerEntered += Invincibilty_OnPlayerEntered;
    }

    private void Invincibilty_OnPlayerEntered( object sender, EventArgs e ){
        playerController.isInvincible = true;
        timer = invincibilityTime;
    }

    private void Update() {
        if (timer > 0f) {
            timer -= Time.deltaTime;
        }
        else {
            timer = 0f;
            playerController.isInvincible = false;
        }
    }

    private void OnDestroy(){
        Invincibility.OnPlayerEntered -= Invincibilty_OnPlayerEntered;
    }
}
