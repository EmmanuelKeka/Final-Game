using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ship : MonoBehaviour
{
    // Start is called before the first frame update
    public bool gameWin = false;
    private float speed = 30;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gameWin == true)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(other.gameObject);
            gameWin = true;
        }
    }
}
