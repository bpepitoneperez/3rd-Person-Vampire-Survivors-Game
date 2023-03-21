using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    public float hp = 100;
    public float maxHp = 100;

    public float level = 1;
    public float xp = 0;
    public float xpNeeded = 20;

    public GameObject sawBlade;
    private float sawBladeTimePassed = 0f;
    private Ability sawBladeAbility;
    public float armor = 1;

    public bool isDead;

    public GameObject player;
    public GameObject playerObj;
    public gameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        sawBladeAbility = sawBlade.GetComponent<Ability>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive)
        {
            if (hp <= 0)
            {
                isDead = true;
                gameManager.GameOver();
            }

            if (!isDead)
            {
                sawBladeTimePassed += Time.deltaTime;
                if (sawBladeAbility.active && sawBladeTimePassed > sawBladeAbility.coolDown)
                {
                    sawBladeTimePassed = 0f;
                    Vector3 playerPos = playerObj.transform.position;
                    Vector3 playerDirection = playerObj.transform.forward;
                    float spawnDistance = 1.2f;

                    Vector3 spawnPos = playerPos + playerDirection * spawnDistance;
                    Instantiate(sawBlade, spawnPos, playerObj.transform.rotation);
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit by enemy");
        }

        //if (collision.gameObject.CompareTag("Powerup"))
        //{
        //    Heal(5);
        //}
    }

    public void HitPlayer(float attackPower)
    {
        hp -= (attackPower / armor);

        gameManager.UpdateHealthText();
    }

    public void Heal(float health)
    {
        hp += health;
        if (hp >= maxHp)
        {
            hp = maxHp;
        }

        gameManager.UpdateHealthText();
    }

    public void GainExperience(float experience)
    {
        xp += experience;
        gameManager.UpdateExperienceText();

        if (xp >= xpNeeded)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        level += 1;
        xpNeeded *= 1.6f;
        gameManager.PlayerLeveledUp();
    }
}
