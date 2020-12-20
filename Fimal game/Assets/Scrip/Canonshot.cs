using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canonshot : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject canonS;
    public GameObject canonL;
    bool canShoot = true;
    void Start()
    {
       
    }
    public void Shoot()
    {
        Instantiate(canonS, canonL.transform.position, canonS.transform.rotation);
        canShoot = false;
        StartCoroutine(shotback(2f));
    }

    // Update is called once per frame
    IEnumerator shotback(float time)
    {
        yield return new WaitForSeconds(time);
        canShoot = true;
    }
    void Update()
    {
        if (canShoot == true) {
            Shoot();
        }
    }
}
