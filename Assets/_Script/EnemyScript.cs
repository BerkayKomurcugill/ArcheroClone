using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;
using DG.Tweening;

public class EnemyScript : MonoBehaviour
{
    public float Health=100;
    float actualHealth=100;
    Image health;
    PlayerScript getClosestEnemy;
    public GameObject bullet;
    public ParticleSystem particle;
    public Transform BulletPos;
    public float attackSpeed=1f;
    float startDelay = 1.5f;
     float attackSpeedVar;
   
    public float speed;
    public float maxChangeTime;
    public TMP_Text DamageText;
    private NavMeshAgent agent;
    public float lastChangeTime;
    Transform player;
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        attackSpeedVar = attackSpeed;
        
        getClosestEnemy = FindObjectOfType<PlayerScript>();
        Health = actualHealth;
        health = this.transform.GetChild(0).GetChild(1).gameObject.GetComponent<Image>();
        // Generate a random direction.
        Vector3 direction = new Vector3(Random.Range(-2, 2), Random.Range(-1, 1), Random.Range(-5, 5));

        // Set the agent's destination to a random point in that direction.
        agent.SetDestination(transform.position + direction * speed);
    }

    // Update is called once per frame
    void Update()
    {
        
        this.transform.LookAt(player);
        if (Time.time - lastChangeTime > maxChangeTime)
        {
            // Generate a random direction.
            Vector3 direction = new Vector3(Random.Range(-2, 2), Random.Range(-1, 1), Random.Range(-5, 5));

            // Set the agent's destination to a random point in that direction.
            agent.SetDestination(transform.position + direction * speed);

            // Reset the timer.
            lastChangeTime = Time.time;
        }

        if (startDelay>0)
        {
            startDelay -= Time.deltaTime;
            return;
        }
        attackSpeed -= Time.deltaTime;
        if (attackSpeed < 0)
        {

            Shoot();
            attackSpeed = attackSpeedVar;
        }
    }
    public void Shoot()
    {

        particle.Play();
        GameObject go = Instantiate(bullet, BulletPos.position, Quaternion.identity);






    }
    public void TakeDamage(float dmg)
    {
        if (actualHealth > 0)
        {
            actualHealth -= dmg;
            DamageText.gameObject.SetActive(true);
            DamageText.text = "-" + dmg.ToString("0");
            DamageText.transform.DOPunchScale(Vector3.one,0.5f).OnComplete(() =>
            {
                DamageText.gameObject.SetActive(false);
            });
            health.fillAmount = Mathf.Clamp(actualHealth / Health, 0, 1f);
            
        }
        if (actualHealth <= 0)
        {
            GameObject go = GameObject.FindWithTag("ExpoParticle");
           go.transform.position = this.transform.position;
            go.GetComponent<ParticleSystem>().Play();
            //DEAD ANIM
            getClosestEnemy.enemyList.Remove(this.gameObject.transform);
            
            getClosestEnemy.CheckEnemiesCount();
            FindObjectOfType<CameraShake>().StartShake();
            GameObject.FindObjectOfType<EXPSystem>().DropExp(this.transform);
            Destroy(this.gameObject, 0.2f);
        }


    }
}
