using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnmanager : MonoBehaviour
{
      [SerializeField]  private GameObject _enemyPrefab;
      [SerializeField] private GameObject _enemyContainer;
      [SerializeField] private GameObject _powerUpPrefab;
      [SerializeField] private GameObject _speedPrefab;
      [SerializeField] private GameObject _shieldPrefab;
      private bool _stopSpawning = false;
      public GameObject[] powerups;

    // Start is called before the first frame update
    void Start()
    {

        //StartCoroutine(SpawnSpeedRoutine());
        //StartSpawning();
        

    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnpowerupRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f);
       
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject newEnemy =   Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator SpawnpowerupRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            int randomPowerup = Random.Range(0, 3);
            Instantiate(powerups[randomPowerup],posToSpawn, Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(3.0f,8.0f));
        }          
    }

    /*IEnumerator SpawnSpeedRoutine()
    {
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            Instantiate(powerups[1], posToSpawn, Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(3.0f, 8.0f));
        }

    }*/

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
