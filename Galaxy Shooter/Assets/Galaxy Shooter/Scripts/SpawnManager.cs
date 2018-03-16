using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    [SerializeField]
    private GameObject _enemyShipPrefab;

    [SerializeField]
    private GameObject[] _powerUps;

    private GameManager _gameManager;

	// Use this for initialization
	void Start ()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        StartSpawnCoroutine();

    }

    public void StartSpawnCoroutine()
    {
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerUpSpawnRoutine());
    }

    IEnumerator EnemySpawnRoutine()
    {
        while (_gameManager.gameOver == false)
        {
            Instantiate(_enemyShipPrefab, new Vector3(Random.Range(-7.5f, 7.5f), 7, 0), Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }

    }

    IEnumerator PowerUpSpawnRoutine()
    {
        while (_gameManager.gameOver == false)
        {
            int ramdonPowerUp = Random.Range(0, 3);
            Instantiate(_powerUps[ramdonPowerUp], new Vector3(Random.Range(-7.5f, 7.5f), 7, 0), Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
    }
	
}
