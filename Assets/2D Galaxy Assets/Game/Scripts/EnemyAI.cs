using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private float _speed = 6f;

    public GameObject enemyExplosion;

    private UImanager _UImanager;
    // Start is called before the first frame update
    void Start()
    {
        _UImanager = GameObject.Find("Canvas").GetComponent<UImanager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);

        if (transform.position.y <= -6.20f)
        {
            float randomX = Random.Range(-7.85f, 7.85f);
            transform.position = new Vector3(randomX, 7f, 0);
        }

    }

    // check if enemy collides with the enemy
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
                Destroy(this.gameObject);
                _UImanager.UpdateScore();
                Instantiate(enemyExplosion, transform.position, Quaternion.identity);

            }
        }
        else if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
            _UImanager.UpdateScore();
            Instantiate(enemyExplosion, transform.position, Quaternion.identity);


        }



    }



}
