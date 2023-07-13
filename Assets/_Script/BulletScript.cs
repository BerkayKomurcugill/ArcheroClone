using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    float moveSpeed = 10f;
    public bool isEnemy;
    Rigidbody rb;
    Transform target;
    PlayerScript getClosestEnemy;
    Vector3 moveDirection;
    public float dealDamage=25;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (isEnemy==false)
        {
            getClosestEnemy = FindObjectOfType<PlayerScript>();
            getClosestEnemy.ClosestVariable();
         
            target = getClosestEnemy.closestEnemy;
            moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;
            rb.velocity = new Vector3(moveDirection.x, moveDirection.y, moveDirection.z);
            Destroy(gameObject, 2f);
        }
        else
        {
            moveDirection = (GameObject.FindWithTag("Player").transform.position - transform.position).normalized * moveSpeed;
            rb.velocity = new Vector3(moveDirection.x, moveDirection.y, moveDirection.z);
            Destroy(gameObject, 2f);
        }
        
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Enemy")
        {
            if(isEnemy==false)
            {
                other.gameObject.GetComponent<EnemyScript>().TakeDamage(dealDamage);
                Destroy(this.gameObject);
            }
            
            
        }
        if (other.gameObject.tag=="Player")
        {
           
            if (isEnemy==true)
            {
               // Debug.Log(other.gameObject.GetComponent<PlayerScript>().DamageText);
                other.gameObject.GetComponent<PlayerScript>().TakeDamage(dealDamage);
                Destroy(this.gameObject);
            }
            
        }
    }
}
