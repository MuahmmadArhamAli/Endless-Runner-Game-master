using UnityEngine;

public class TileGemDisabler : MonoBehaviour{

    [SerializeField] private GameObject[] firstSet;
    [SerializeField] private GameObject[] secondSet;

    private void Start(){
        if (Random.Range(0f,1f) < 0.5){
            foreach (GameObject gameObject in firstSet){
                gameObject.SetActive(false);
            }
        }

        else{
            foreach (GameObject gameObject in secondSet){
                gameObject.SetActive(false);
            }
        }
    }
}
