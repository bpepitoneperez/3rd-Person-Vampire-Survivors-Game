using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    private GameObject player;
    private Rigidbody objectRb;
    private gameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        gameManager = GameObject.Find("Game Manager").GetComponent<gameManager>();
        objectRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive)
        {
            objectRb.constraints = RigidbodyConstraints.None;
            transform.forward = player.transform.position - transform.position;

            objectRb.AddForce(transform.forward * moveSpeed, ForceMode.Force);
        }
        else
        {
            objectRb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}
