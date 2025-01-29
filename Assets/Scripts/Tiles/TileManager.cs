using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class TileManager : MonoBehaviour
{   
    public static TileManager Instance { get; private set;}
    public int scoreMultiplier = 1;

    private List<GameObject> activeTiles;
    public GameObject[] tilePrefabs;

    public float tileLength = 30;
    public int numberOfTiles = 3;
    public int totalNumOfTiles = 8;

    public float zSpawn = 0;

    private Transform playerTransform;

    private int previousIndex;

    private void Awake(){
        if (Instance == null){
            Instance = this;
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        activeTiles = new List<GameObject>();
        for (int i = 0; i < numberOfTiles; i++)
        {
            if(i==0)
                SpawnTile();
            else
                SpawnTile(UnityEngine.Random.Range(0, totalNumOfTiles));
        }

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        ScoreMultiplier.OnPlayerEntered += ScoreMultiplier_OnPlayerEntered;

    }

    private void ScoreMultiplier_OnPlayerEntered(object sender, EventArgs e){
        scoreMultiplier += 1;

        StartCoroutine(ScoreMultiplierTimer());
    }
    void Update()
    {
        if(playerTransform.position.z - 30 >= zSpawn - (numberOfTiles * tileLength))
        {
            int index = UnityEngine.Random.Range(0, totalNumOfTiles);
            while(index == previousIndex)
                index = UnityEngine.Random.Range(0, totalNumOfTiles);

            DeleteTile();
            SpawnTile(index);
        }
            
    }

    public void SpawnTile(int index = 0)
    {
        GameObject tile = tilePrefabs[index];
        if (tile.activeInHierarchy)
            tile = tilePrefabs[index + 8];

        if(tile.activeInHierarchy)
            tile = tilePrefabs[index + 16];

        tile.transform.position = Vector3.forward * zSpawn;
        tile.transform.rotation = Quaternion.identity;
        tile.SetActive(true);

        tile.GetComponent<TilePowerUpDisabler>().TogglePowerUp();

        activeTiles.Add(tile);
        zSpawn += tileLength;
        previousIndex = index;
    }

    private void DeleteTile()
    {
        activeTiles[0].SetActive(false);
        activeTiles.RemoveAt(0);
        PlayerManager.score += 3 * scoreMultiplier;
    }

    private System.Collections.IEnumerator ScoreMultiplierTimer(){
        yield return new WaitForSeconds(5f);

        scoreMultiplier -= 1;
    }

    private void OnDestroy(){
        ScoreMultiplier.OnPlayerEntered -= ScoreMultiplier_OnPlayerEntered;
        StopCoroutine(ScoreMultiplierTimer());
    }
}
