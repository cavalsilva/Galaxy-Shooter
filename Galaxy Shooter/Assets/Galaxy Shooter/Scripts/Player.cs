using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public bool canTripleShot = false;
    public bool canSpeedPower = false;
    public bool isShieldActive = false;

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private GameObject _tripleLaserPrefab;

    [SerializeField]
    private float _fireRate = 0.25f;

    [SerializeField]
    private float _speed = 5.0f;

    [SerializeField]
    private int _lifes = 3;

    [SerializeField]
    private GameObject _explosionPrefab;

    [SerializeField]
    private GameObject _shieldGameObject;

    private float _canFire = 0.0f;

    // Use this for initialization
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (Time.time > _canFire)
        {
            if (canTripleShot)
            {
                Instantiate(_tripleLaserPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.88f, 0), Quaternion.identity);
            }
            _canFire = Time.time + _fireRate;
        }
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (canSpeedPower)
        {
            //Add 1.5x from normal speed
            transform.Translate(Vector3.right * _speed * 1.5f *  horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * _speed * 1.5f * verticalInput * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.right * _speed * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * _speed * verticalInput * Time.deltaTime);

        }

        //Player bounds
        if (transform.position.y > 4.2f)
        {
            transform.position = new Vector3(transform.position.x, 4.2f, 0);

        }
        else if (transform.position.y < -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);
        }

        //Wrapping
        if (transform.position.x > 9.5f)
        {
            transform.position = new Vector3(-9.5f, transform.position.y, 0);
        }
        else if (transform.position.x < -9.5f)
        {
            transform.position = new Vector3(9.5f, transform.position.y, 0);
        }
    }

    public void Damage()
    {
        if (isShieldActive)
        {
            DisableShieldPowerOn();
            return;
        }
        else
        {
            _lifes--;

            if (_lifes < 1)
            {
                Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }

    }

    public void TrippeShotPowerUpOn()
    {
        canTripleShot = true;
        StartCoroutine(TrippeShotPowerDownRoutine());
    }

    public void SpeedBoostPowerOn()
    {
        canSpeedPower = true;
        StartCoroutine(SpeedBoostDownRoutine());
    }

    public void EnableShieldPowerOn()
    {
        isShieldActive = true;
        _shieldGameObject.SetActive(true);
    }

    public void DisableShieldPowerOn()
    {
        isShieldActive = false;
        _shieldGameObject.SetActive(false);
    }

    IEnumerator TrippeShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canTripleShot = false;
    }

    IEnumerator SpeedBoostDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canSpeedPower = false;
    }
}