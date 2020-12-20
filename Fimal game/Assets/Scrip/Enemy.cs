using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float speed = 6;
    private bool colid = false;
    private bool faceRight = true;
    private bool canShoot = true;
    private float gravityForce = 4;
    private float zRange = -3f;
    private float yRange;
    public GameObject bouletPrefab;
    public GameObject leftbouletPrefab;
    public GameObject shoutLoc;
    PlayerContreller player;

    // Start is called before the first frame update
    void Start()
    {

        yRange = transform.position.y;
        player = GameObject.Find("PolyartCharacter").GetComponent<PlayerContreller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.isDead == false) {
            if (transform.position.z < zRange || transform.position.z > zRange)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, zRange);
            }
            if (transform.position.z < yRange || transform.position.z > yRange)
            {
                transform.position = new Vector3(transform.position.x, yRange, transform.position.z);
            }
            transform.Translate(0f, 0f, 1 * speed * Time.deltaTime);
            if (colid == true && faceRight == true)
            {
                moveLeft();
            }
            if (faceRight == false && colid == true)
            {
                moveRight();
            }
            if (canShoot == true)
            {
                Shoot();
            }
        }
    }
    public void moveRight()
    {
        transform.localEulerAngles = new Vector3(transform.rotation.x, 90, transform.rotation.z);
        colid = false;
        faceRight = true;
    }
    public void moveLeft()
    {
        transform.localEulerAngles = new Vector3(transform.rotation.x, -90, transform.rotation.z);
        colid = false;
        faceRight = false;
    }
    public void Shoot()
    {
        if (faceRight == true)
        {
            Instantiate(bouletPrefab, shoutLoc.transform.position, bouletPrefab.transform.rotation);
        }
        else
        {
            Instantiate(leftbouletPrefab, shoutLoc.transform.position, leftbouletPrefab.transform.rotation);
        }
        canShoot = false;
        StartCoroutine(shotback(2f));
    }
    IEnumerator shotback(float time)
    {
        yield return new WaitForSeconds(time);
        canShoot = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("wall"))
        {
            colid = true;
        }
    }
}
