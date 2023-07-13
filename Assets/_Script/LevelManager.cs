using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelManager : MonoBehaviour
{
    public List<GameObject> levels;
    public GameObject player;
    int levelcount = 0;
    private void Start()
    {
       
        levels[levelcount].transform.GetChild(0).gameObject.SetActive(true);
    }
    public void NextLevel()
    {
        
      StartCoroutine(NextLevelAnim());
        




    }
    void OpenFinish()
    {
        levels[levelcount].transform.GetChild(0).gameObject.SetActive(true);
    }
    
    IEnumerator NextLevelAnim()
    {
        PlayerScript.dontshoot = true;
        GameObject.FindWithTag("FinishCanvas").transform.GetChild(0).gameObject.SetActive(true);
        if (levelcount >= 2)
        {
           
        }
        else
        {
            levels[levelcount].SetActive(false);
            levelcount++;
            levels[levelcount].SetActive(true);
            player.transform.DOMove(Vector3.zero, 1f).SetEase(Ease.Flash);
            player.GetComponent<PlayerScript>().doOnceCollect = false;
            player.GetComponent<PlayerScript>().enemyParent = GameObject.FindWithTag("EnemyParent").transform;
            player.GetComponent<PlayerScript>().enemyList.Clear();
            for (int i = 0; i < player.GetComponent<PlayerScript>().enemyParent.childCount; i++)
            {
                player.GetComponent<PlayerScript>().enemyList.Add(player.GetComponent<PlayerScript>().enemyParent.GetChild(i));
            }
        }
        
             
        

        yield return new WaitForSeconds(2f);
        GameObject.FindWithTag("FinishCanvas").transform.GetChild(0).gameObject.SetActive(false);
        OpenFinish();
        PlayerScript.dontshoot = false;
    }



}
