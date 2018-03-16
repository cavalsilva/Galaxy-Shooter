using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    [SerializeField]
    private float _speed = 5.0f;

    [SerializeField]
    private GameObject _enemyExplosionPrefab;

    private UIManager _uIManager;
    private GameManager _gameManager;

    // Use this for initialization
    void Start () {

        _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        //Random position
        transform.position = new Vector3(Random.Range(-7.5f, 7.5f), 7, 0);
    }
	
	// Update is called once per frame
	void Update ()
    {
        //when off the screen
        if (_gameManager.gameOver == false)
        { 
            if (transform.position.y < -7)
            {
                //Random position
                float randomX = Random.Range(-7.5f, 7.5f);
                transform.position = new Vector3(randomX, 7, 0);
            }
            else
            {
                //Movemente enemy
                transform.Translate(Vector3.down * _speed * Time.deltaTime);
            }
        }
        else if (_gameManager.gameOver == true)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Laser")
        {
            if(other.transform.parent != null)
            {
                Destroy(other.transform.parent.gameObject);
            }

            Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);

            Destroy(other.gameObject);
            Destroy(this.gameObject);

            _uIManager.UpdateScore(10);
        }
        else if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if(player != null)
            {
                player.Damage();

            }

            Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }

    }
}
