using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{

    [SerializeField] private float _speed = 3.5f;
    [SerializeField] private GameObject _laserprefab;
    [SerializeField] private GameObject _tripleShotPrefab;
    [SerializeField] private GameObject _speedPrefab;
    [SerializeField] private GameObject _shieldPrefab;
    [SerializeField] private GameObject _shieldVisualizer;

    [SerializeField] private GameObject _leftEngine;
    [SerializeField] private GameObject _rightEngine;



    private Vector3 _offsetPosition = new Vector3(0,1.05f,0);
    [SerializeField] private float _fireRate = 0.5f;
    private float _canFire = -1f;
    [SerializeField] private int _lives = 3;
    private Spawnmanager _spawnManager;


    [SerializeField] private bool _isTripleShotActive = false;
    [SerializeField] private bool _isSpeedActive = false;
    [SerializeField] private bool _isShieldActive;

    [SerializeField] private int _score;

    private UIManager _uiManager;

    [SerializeField] private AudioClip _fire;
    [SerializeField] private AudioClip _explosion;

    AudioSource myAudioSource;

    // Start is called before the first frame update
    void Start()
    {

        CalculateMovement();
        //take the current position = new position
        transform.position = new Vector3(0, 0, 0);

        _spawnManager = GameObject.Find("Spawn_manager").GetComponent<Spawnmanager>();

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

       myAudioSource = GetComponent<AudioSource>();

        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is null");
        }

        if(_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL.");
        }

    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }

       if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            _speed += 15;
        }
     else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            _speed = 5;
        }

    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        //transform.Translate(new Vector3(1,0,0) * horizontalInput* speed *  Time.deltaTime );
        transform.Translate(direction * _speed * Time.deltaTime);
        

        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);

        }
        else if (transform.position.y < -3.8f)
        {
            transform.position = new Vector3(transform.position.x, -3.8f, 0);
        }

        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
            _canFire = Time.time + _fireRate;
                
            if(_isTripleShotActive == true)
            {
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
            }
        
             else
             {
                Instantiate(_laserprefab, transform.position + _offsetPosition, Quaternion.identity);
             }
        AudioClip clip = _fire;
        myAudioSource.PlayOneShot(clip);
       
    }

    public void Damage()
    {

        if (_isShieldActive == true)
        {
            _isShieldActive = false;
            _shieldVisualizer.SetActive(false);
            return;          
        }

        _lives--;

        if(_lives == 2)
        {
            _rightEngine.SetActive(true);

        } else if(_lives == 1)
        {
            _leftEngine.SetActive(true);
        }

      _uiManager.UpdateLives(_lives);
     
        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }

        
       
    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerRoutine());
    }

    IEnumerator TripleShotPowerRoutine()
    {
       yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }

    public void SpeedActive()
    {
        
        _isSpeedActive = true;
        _speed *=2;
        StartCoroutine(SpeedPowerRoutine());
    }

    IEnumerator SpeedPowerRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedActive = false;
    }

    public void ShieldActive()
    {
        _isShieldActive = true;
        _shieldVisualizer.SetActive(true);     
        StartCoroutine(ShieldPowerRoutine());
    }

    IEnumerator ShieldPowerRoutine()
    {
        yield return new WaitForSeconds(5.0f);      
        _isShieldActive = false;
    }

    public void AddScore(int points)
    {
      _score += points;
        _uiManager.UpdateScore(_score);
    }

   

}
