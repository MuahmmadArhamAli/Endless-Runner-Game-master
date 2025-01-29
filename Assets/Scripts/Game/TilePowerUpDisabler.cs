using UnityEngine;

public class TilePowerUpDisabler : MonoBehaviour{

    [SerializeField] private GameObject powerUp;

    public void TogglePowerUp(){
        if (Random.Range(0f,1f) > 0.1)
            powerUp.SetActive(false);
        else
            powerUp.SetActive(true);
    }
    
}
