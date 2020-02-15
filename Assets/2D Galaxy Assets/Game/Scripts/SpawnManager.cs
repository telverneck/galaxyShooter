using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _spawnEnemyPrefab;

    [SerializeField]
    private GameObject[] _powerUps;

    [SerializeField]
    private GameObject _player;

    private GameController _gameController;
    
    // Start is called before the first frame update
    void Start()
    {
        _gameController = GameObject.Find("GameManager").GetComponent<GameController>();
        
    }

    public void StartSpawnRoutines()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpsRoutine());

    }

    public IEnumerator SpawnEnemyRoutine()
    {
        while (_gameController.gameOver == false)
        {
            Instantiate(_spawnEnemyPrefab,new Vector3(Random.Range(-7.85f, 7.85f), 7f, 0), Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }

    }

    public IEnumerator SpawnPowerUpsRoutine()
    {
        while (_gameController.gameOver == false)
        {
            int randomPowerUp = Random.Range(0, 3);
            Instantiate(_powerUps[randomPowerUp], new Vector3(Random.Range(-7.85f, 7.85f), 7f, 0), Quaternion.identity);
            yield return new WaitForSeconds(10.0f);

        }
    }      
   
}
