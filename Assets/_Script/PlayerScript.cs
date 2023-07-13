using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class PlayerScript : MonoBehaviour
{
    public static float exp;
    public float expShow;
    private float expTolevel = 100;
    public Image expBar;
    public int currentlevel = 0;
    public Canvas playerCanvas;
    public float Health = 100;
    float actualHealth = 100;
    public Image health;
    public Canvas upgradeCanvas;
    public List<Transform> enemyList = new List<Transform>();
    public GameObject bullet;
    public Transform BulletPos;
    public ParticleSystem particle;
    public Transform closestEnemy;
    public ParticleSystem Stepparticle;
    Animator anim;
    public Transform enemyParent;
    public TMP_Text DamageText;
    public bool doOnceCollect = false;
    public static bool dontshoot=false;
    ParticleSystem rightFoot;
    ParticleSystem leftFoot;

    private void Start()
    {
        rightFoot = GameObject.FindWithTag("RightFoot").GetComponent<ParticleSystem>();
        leftFoot = GameObject.FindWithTag("LeftFoot").GetComponent<ParticleSystem>();
        enemyParent = GameObject.FindWithTag("EnemyParent").transform;
        enemyList.Clear();
        for (int i = 0; i < enemyParent.childCount; i++)
        {
            enemyList.Add(enemyParent.GetChild(i));
        }
        anim = GetComponent<Animator>();
    }
    public void ClosestVariable()
    {

        if (enemyList.Count > 0)
        {
            closestEnemy = ClosestEnemy(enemyList);
        }

    }

    public void LookAtClosetEnemy()
    {

        this.transform.LookAt(ClosestEnemy(enemyList).position);

    }
    private void Update()
    {
        expShow = exp;
        playerCanvas.transform.position = new Vector3(this.transform.position.x, playerCanvas.transform.position.y, this.transform.position.z);
        if (enemyList.Count <= 0)
        {
            if (doOnceCollect == false)
            {
                GameObject.FindObjectOfType<EXPSystem>().CollectExp();
                doOnceCollect = true;
            }
        }
        expBar.fillAmount = Mathf.Clamp(exp / expTolevel, 0, 1f);
        if (exp >= expTolevel)
        {
            exp = 0;
            expTolevel *= 1.5f;
            currentlevel++;
            upgradeCanvas.gameObject.SetActive(true);
            Time.timeScale = 0;

        }

    }
    public void AttackSpeedIncrease()
    {
        anim.SetFloat("speed", anim.GetFloat("speed") * 1.2f);
        Time.timeScale = 1;
        upgradeCanvas.gameObject.SetActive(false);
    }
    public void FastBullet()
    {
        Time.timeScale = 1;
        upgradeCanvas.gameObject.SetActive(false);
    }
    public void HealthPlus()
    {
        Health = +50;
        health.fillAmount = Mathf.Clamp(actualHealth / Health, 0, 1f);
        Time.timeScale = 1;
        upgradeCanvas.gameObject.SetActive(false);
    }

    public void CheckEnemiesCount()
    {
        if (enemyList.Count == 0)
        {
            anim.SetBool("Run", false);
            anim.SetBool("Fire", false);


        }
    }
    public void RightFootParticle()
    {
        rightFoot.Play();
    }
    public void LeftFootParticle()
    {
        leftFoot.Play();
    }
    public void Shoot()
    {
        if (dontshoot==true)
        {
            return;
        }
        if (enemyList.Count < 0)
        {
            return;
        }
        ClosestVariable();
        //  Debug.Log(closestEnemy.name);
        this.transform.LookAt(closestEnemy);
        particle.Play();
        GameObject go = Instantiate(bullet, BulletPos.position, Quaternion.identity);




    }
    public void StepEffect()
    {
        Stepparticle.Play();
    }
    public void TakeDamage(float dmg)
    {
        if (actualHealth >= 0)
        {
            actualHealth -= dmg;
            DamageText.gameObject.SetActive(true);
            DamageText.text = "-" + dmg;
            DamageText.transform.DOPunchScale(Vector3.one, 0.5f).OnComplete(() =>
            {
                DamageText.gameObject.SetActive(false);
            });
            health.fillAmount = Mathf.Clamp(actualHealth / Health, 0, 1f);

        }


    }
    Transform ClosestEnemy(List<Transform> enemyListt)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (Transform potentialTarget in enemyList)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }

        return bestTarget;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Finish")
        {
            GameObject.FindObjectOfType<LevelManager>().NextLevel();
        }

    }
}
