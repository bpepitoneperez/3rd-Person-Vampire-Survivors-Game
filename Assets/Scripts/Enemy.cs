using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float hp = 15;
    public float attack = 2;
    public float armor = 1;
    public float xpGiven = 6;

    private bool deathExplosionPlayed;

    public ParticleSystem bloodParticles;
    public ParticleSystem deathParticles;

    private GameObject gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager");
        //bloodParticles = gameObject.transform.Find("Blood").GetComponent<ParticleSystem>();
        //deathParticles = gameObject.transform.Find("Death").GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);

            //if (!deathExplosionPlayed)
            //{
            //    deathExplosionPlayed = true;
            //    deathParticles.Play();
            //}
            gameManager.GetComponent<gameManager>().AddExperienceToPlayer(xpGiven);
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerLogic>().HitPlayer(attack);
        }

        if (collision.gameObject.CompareTag("Ability"))
        {
            bloodParticles.Play();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

        }

        if (collision.gameObject.CompareTag("Ability"))
        {

        }
    }

    public void SetDefaults()
    {
        hp = 15;
        attack = 2;
        armor = 1;
        xpGiven = 6;
    }

    public void HitEnemy(float attackPower)
    {
        hp -= (attackPower / armor);
    }
}
