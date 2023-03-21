using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public bool active = true;
    public float coolDown = 2.5f;
    public float attackPower = 10.0f;
    public float projectileSpeed = 10.0f;
    public float projectileDuration = 1.0f;
    public float sizeModifier = 1.0f;

    private float firedTime = 0;

    private GameObject playerObject;

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.Find("PlayerObject");
        transform.localScale *= sizeModifier;
    }

    // Update is called once per frame
    void Update()
    {
        firedTime += Time.deltaTime;
        if (firedTime > projectileDuration)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.position += transform.forward * Time.deltaTime * projectileSpeed;
        }
    }

    public void setDefaults()
    {
        attackPower = 10.0f;
        projectileSpeed = 10.0f;
        projectileDuration = 0.5f;
        sizeModifier = 1.0f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().HitEnemy(attackPower);
        }

        //if (collision.gameObject.CompareTag("Walls"))
        //{
        //    Debug.Log("BOUNCE THE BLADE");
        //    Debug.Log(transform.forward);
        //    transform.forward *= -1;
        //    Debug.Log(transform.forward);
        //}
    }
}
