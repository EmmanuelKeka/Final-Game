using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocketBomB : MonoBehaviour
{
    // Start is called before the first frame update
    private float speed = 30;
    PlayerContreller player;
    ParticleSystem explosion;
    void Start()
    {
        player = GameObject.Find("PolyartCharacter").GetComponent<PlayerContreller>();
        explosion = GameObject.Find("Explosion").GetComponent<ParticleSystem>();
        explosion.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.gameObject.CompareTag("Player"))
        {
            player.takeoffheart(true);
            explosion.Play();
            if (player.getnumOf() <= 0)
            {
                player.setNumOf(0);
                player.isDead = true;
                player.gameOver = true;
            }
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}