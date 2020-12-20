using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startship : MonoBehaviour
{
    // Start is called before the first frame update
    public bool gameWin = false;
    public bool canMove = true;
    private float speed = 30;
    public GameObject spawnLoc;
    public GameObject player1;
    void Start()
    {
        player1 = GameObject.Find("PolyartCharacter");
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove == true)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("startline"))
        {
            canMove = false;
            player1.SetActive(true);
        }
    }
}
