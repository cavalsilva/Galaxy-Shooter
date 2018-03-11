using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour {

    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int _powerUpId; //0 = triple shot, 1 = speed boost, 2 = shield

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if(player != null)
            {
                //Enable triple shot
                if(_powerUpId == 0)
                {
                    player.TrippeShotPowerUpOn();
                }
                //Enable speed boost
                else if (_powerUpId == 1)
                {
                    player.SpeedBoostPowerOn();
                }
                //Enable shield
                else if (_powerUpId == 2)
                {
                    player.EnableShieldPowerOn();
                }
            }

            Destroy(this.gameObject);
        }

    }
}
