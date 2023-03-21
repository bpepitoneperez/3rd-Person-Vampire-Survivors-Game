using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    private GameObject gameManager;
    private PlayerLogic playerLogic;
    public float healAmount = 5f;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager");
        playerLogic = GameObject.Find("Player").GetComponent<PlayerLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnCollisionEnter(Collision collision)
    //{

    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player Object"))
        {
            Destroy(gameObject);
            playerLogic.Heal(healAmount);
        }
    }
}
