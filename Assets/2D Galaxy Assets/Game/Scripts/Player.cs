using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{

    public int lifePlayer = 3;

    [SerializeField]
    private GameObject _laserPrefab;

    public GameObject TripleShot;

    [SerializeField]
    private GameObject _player_Explosion;

    [SerializeField]
    private GameObject _player_Shield;

    [SerializeField]
    private float _fireRate = 0.25f;

    private float _canFire = 0.0f; 

    [SerializeField]
    private float _speed = 10f;

    [SerializeField ]
    private GameObject[] _engines;

    public bool canTripleShot = false;
    public bool speedBoost = false;
    public bool shield = false;

    private int _hitCount = 0;

    private UImanager _UImanager;
    private GameController _gameController;
    private SpawnManager _spawnManager;
    private AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {

        transform.position = new Vector3(0, 0, 0);

        _UImanager = GameObject.Find("Canvas").GetComponent<UImanager>();
        _gameController = GameObject.Find("GameManager").GetComponent<GameController>();
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _audioSource = GetComponent<AudioSource>();
        if (_spawnManager != null)
        {
            _spawnManager.StartSpawnRoutines();

        }


        if (_UImanager != null)
        {
            _UImanager.UpdateLives(lifePlayer);
             
        }
        _hitCount = 0;
    }
    // Update is called once per frame
    void Update()
    {
        Movement();
#if UNITY_ANDROID
        if (Input.GetKeyDown(KeyCode.Space) || CrossPlatformInputManager.GetButtonDown("Fire"))
        {
            FireLaser();
        }

#elif UNITY_IOS
        // can shot with Space key or mouse left button
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            FireLaser();
        }
#else
        // can shot with Space key or mouse left button
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            FireLaser();
        }

   
#endif
    }
    private void Movement()
    {
        float horizontalInput = CrossPlatformInputManager.GetAxis("Horizontal"); //Input.GetAxis("Horizontal");
        float verticalInput = CrossPlatformInputManager.GetAxis("Vertical"); //Input.GetAxis("Vertical");


        transform.Translate(Vector3.right * _speed * horizontalInput * Time.deltaTime);
        transform.Translate(Vector3.up * _speed * verticalInput * Time.deltaTime);

        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);

        }
        else if (transform.position.y < -4.2)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);

        }
        if (transform.position.x > 9.5)
        {
            transform.position = new Vector3(-9.5f, transform.position.y, 0);

        }

        else if (transform.position.x < -9.5f)
        {
            transform.position = new Vector3(9.5f, transform.position.y, 0);

        }
    }

    private void FireLaser()
    {
        if (Time.time > _canFire)
        {
            _audioSource.Play();
            //triple shot verification
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0) || CrossPlatformInputManager.GetButtonDown("Fire")) && canTripleShot == true)
            {
                Instantiate(TripleShot, transform.position,Quaternion.identity);
                
            }
            else
            //single shot
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.11f, 0), Quaternion.identity);
                
            }
            _canFire = Time.time + _fireRate;
            

        }

    }

    public void Damage()
    {
       
        if (shield == true) 
        {
            shield = false;
            _player_Shield.SetActive(false);
            return;
             
        }
        _hitCount++;
        if (_hitCount == 1)
        {
            _engines[0].SetActive(true);
        }
        else if (_hitCount == 2)
        {
            _engines[1].SetActive(true);
        }
        lifePlayer--; // player get damage        
        _UImanager.UpdateLives(lifePlayer);

        if (lifePlayer < 1)
        {
            Instantiate(_player_Explosion, transform.position, Quaternion.identity);
            _gameController.gameOver = true;
            _UImanager.ShowTitleScreen();
            Destroy(this.gameObject);

            
            

        }

    }

    public void TripleShotPowerUpON()
    {
        canTripleShot = true;          
        StartCoroutine(TripleShotPOwerDownRoutine());
    }
    public IEnumerator TripleShotPOwerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canTripleShot = false;
    }

    public void speedBoostON()
    {
        speedBoost = true;
        _speed += 10.0f;
        StartCoroutine(SpeedBoostRoutine());       
    }
    public IEnumerator SpeedBoostRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _speed -= 10.0f;
        speedBoost = false;
    }

    public void ShieldPowerUpON()
    {
        shield = true;
        _player_Shield.SetActive(true);
    }
    
    

}
