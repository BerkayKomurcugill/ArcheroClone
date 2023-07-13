using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EXPSystem : MonoBehaviour
{
    public GameObject expCubePrefab;
    public List<Transform> expCubeList = new List<Transform>();
    Transform player;
    public float getexpValue;
    public GameObject door;
    
    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }
    public void DropExp(Transform enemy)
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject go = Instantiate(expCubePrefab, enemy.transform.position, Quaternion.identity);
            go.transform.parent = this.gameObject.transform;
            go.transform.DOJump(enemy.position+new Vector3(i*0.2f,0,i*0.2f), 1, 1, 0.5f);
            expCubeList.Add(go.transform);
        }
        
    }
    
    public void CollectExp()
    {
       StartCoroutine(CollectExpCorrect());
    }
    IEnumerator CollectExpCorrect()
    {
        for (int i = 0; i < expCubeList.Count; i++)
        {
            Transform tr = expCubeList[i];
            tr.parent = player;

            tr.DOLocalMove(Vector3.zero, 0.5f).SetEase(Ease.Linear).OnComplete(() =>
            {
             
                tr.gameObject.SetActive(false);
                PlayerScript.exp += getexpValue;

            });
            yield return new WaitForSeconds(0.1f);
            //  expCubeList.Remove(tr);
        }
        GameObject.FindWithTag("SF_Door").GetComponent<Animator>().SetBool("character_nearby", true);
        
    }
}
