using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed = 3.0f;

   [SerializeField] private GameObject _explosionPrefab;
    private Spawnmanager _spawManager;



    // Start is called before the first frame update
    void Start()
    {
        _spawManager = GameObject.Find("Spawn_manager").GetComponent<Spawnmanager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Laser")
        {
            Instantiate(_explosionPrefab,transform.position, Quaternion.identity );
            Destroy(collision.gameObject);
            _spawManager.StartSpawning();
            Destroy(this.gameObject, 0.5f);
        }

        
    }
}
