using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    [SerializeField] private int _speed = 8;
    private bool _isEnemyLaser = false;


    // Update is called once per frame
    void Update()
    {
        if (_isEnemyLaser == false)
        {
            MoveUp();
        }
        else
        {
            MoveDown();
        }
    }

    void MoveUp()
    {
        //float verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.y > 8f)
        {
            if (transform.parent != null)
            {
                Destroy(this.transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

        void MoveDown()
        {
            //float verticalInput = Input.GetAxis("Vertical");

            transform.Translate(Vector3.down * _speed * Time.deltaTime);

            if (transform.position.y < -8f)
            {
                if (transform.parent != null)
                {
                    Destroy(this.transform.parent.gameObject);
                }
                Destroy(this.gameObject);
            }
        }

    public void AssignEnemyLaser()
    {
        _isEnemyLaser = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && _isEnemyLaser == true)
        {
            Player player = other.GetComponent<Player>();

            if(player != null)
            {
                player.Damage();
            }
        }
    }
}


