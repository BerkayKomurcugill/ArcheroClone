using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 0.1f;
    public void StartShake()
    {
        StartCoroutine(Shake());
    }
    public IEnumerator Shake()
    {
        
        Vector3 shakeOffset = new Vector3(0, 0, 0);

       
        shakeOffset.x += Random.Range(-shakeMagnitude, shakeMagnitude);
        shakeOffset.y += Random.Range(-shakeMagnitude, shakeMagnitude);
        shakeOffset.z += Random.Range(-shakeMagnitude, shakeMagnitude);

       
        transform.position += shakeOffset;

       
        yield return new WaitForSeconds(shakeDuration);

       
        transform.position = Vector3.zero;
    }

}
