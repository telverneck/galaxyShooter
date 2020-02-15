using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class GameController : MonoBehaviour
{
    public bool gameOver = true;
    public GameObject player;
    private UImanager _uImanager;
    private SpawnManager _spawnManager;

    private void Start()
    {
        _uImanager = GameObject.Find("Canvas").GetComponent<UImanager>();
    }


    void Update()
    {
        if (gameOver == true)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0))
            {
                Instantiate(player,new Vector3(0,0,0),Quaternion.identity);
                gameOver = false;
                _uImanager.DisableTitleScreen();

                //StartCoroutine(_spawnManager.SpawnEnemyRoutine());
                //StartCoroutine(_spawnManager.SpawnPowerUpsRoutine());


            }
        }
    }
}
