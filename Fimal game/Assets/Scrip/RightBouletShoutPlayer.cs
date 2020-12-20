using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightBouletShoutPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    private float speed = 30;
    PlayerContreller player;
    void Start()
    {
        player = GameObject.Find("PolyartCharacter").GetComponent<PlayerContreller>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}