using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    [SerializeField]
    private float _speed = 5.0f;

    [SerializeField]
    private GameObject _enemyExplosionPrefab;

	// Use this for initialization
	void Start () {
        //Random position
        transform.position = new Vector3(Random.Range(-7.5f, 7.5f), 7, 0);
    }
	
	// Update is called once per frame
	void Update ()
    {
        //when off the screen
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
