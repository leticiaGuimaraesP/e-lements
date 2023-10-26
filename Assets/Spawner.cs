using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
   [SerializeField] public  GameObject CharacterPrefab;

    private int spawnCount = 0;
    private bool canSpawn = true;
    // Start is called before the first frame update
    void Start()
    {
         StartSpawner();
    }

    void StartSpawner(){
        StartCoroutine(SpawnCharacters());
    }

    // Update is called once per frame
    void Update()
    {
        //  if(Input.GetKeyDown(KeyCode.Space)){
        //     SpawnCharacters();
        // }
    }

    //   public void SpawnCharacters()
    // {
    //     Instantiate(CharacterPrefab, transform.position, Quaternion.identity);
    // }

    IEnumerator SpawnCharacters()
    {
        while (spawnCount < 10)
        {
            if (canSpawn)
            {
                Instantiate(CharacterPrefab, transform.position, Quaternion.identity);
                spawnCount++;
                if (spawnCount == 10)
                {
                    canSpawn = false; // Stop spawning when spawnCount reaches 10
                }
            }

            yield return new WaitForSeconds(2.0f); // Wait for 5 seconds before the next spawn
        }
    }

}
