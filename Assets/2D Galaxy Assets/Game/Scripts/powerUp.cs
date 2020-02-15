using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2.5f;
    [SerializeField]
    private int powerupID; // 0 = tripleShot, 1 - speed, 2 - shields 

    [SerializeField]
    private AudioClip _audioClip;
    

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);

        if (transform.position.y < -7)
        {
            Destroy(this.gameObject);
        }

        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(_audioClip, Camera.main.transform.position, 1f);

            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                if (powerupID == 0) // triple shot
                {
                    player.TripleShotPowerUpON();
                }
                else if (powerupID == 1) // speed
                {
                    player.speedBoostON();

                }
                else if (powerupID == 2) // shields
                    player.ShieldPowerUpON();

            }
            Destroy(this.gameObject);
            
        }
    }
}
