using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MyJoystick : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{

    [HideInInspector]
    public bool pressed;
    public Animator player;

    PlayerScript getClosestEnemy;
    public ParticleSystem particle;
    
    
    private void Start()
    {
       
        getClosestEnemy = FindObjectOfType<PlayerScript>();
        particle.gameObject.SetActive(false);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        // Debug.Log("Pressed");
        pressed = true;
        player.SetBool("Run", true);
        player.SetBool("Fire", false);
        particle.gameObject.SetActive(false);
        //getClosestEnemy.isLooking = true;
        particle.Stop();
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        // Debug.Log("Released");
        pressed = false;
        player.SetBool("Run", false);
        if (getClosestEnemy.enemyList.Count>0)
        {
            particle.gameObject.SetActive(true);
            player.SetBool("Fire", true);
            getClosestEnemy.LookAtClosetEnemy();
           // getClosestEnemy.isLooking = false;
           
        }
     

    }
    

}
