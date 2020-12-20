using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerContreller : MonoBehaviour
{
    // Start is called before the first frame update
    private float speed = 10;
    private Rigidbody playerRb;
    public float jumpForce = 400;
    private float gravityForce = 4;
    private float zRange = -3f;
    private GameObject mCamera;
    private Animator playeranim;
    public bool isOnGround = false;
    public bool faceRight = true;
    public bool gameOver = false;
    public bool canShoot= true;
    public bool canMove = true;
    public bool isDead = false;
    public bool powerUp = false;
    private int numberOfB = 5;
    private int numberOfHeart = 8;
    private int numberOfHeart1 = 8;
    public Text DisplaynumberOfHeart;
    public GameObject shoutLoc;
    public GameObject bouletPrefab;
    public GameObject leftbouletPrefab;
    public GameObject poweracc;
    public Text numberDisplay;
    public Text numberofEnemy;
    AudioSource audiopower;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        mCamera = GameObject.Find("cameraControl");
        poweracc = GameObject.Find("powerac");
        shoutLoc = GameObject.Find("shootLocation");
        audiopower = GameObject.Find("PolyartCharacter").GetComponent<AudioSource>();
        audiopower.Stop();
        poweracc.SetActive(false);
        playeranim = GetComponent<Animator>();
        gameOver = false;
        if (Physics.gravity.y > -30) {
            Physics.gravity *= gravityForce;
        } 
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 characterScale = transform.localScale;
        mCamera.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        if (mCamera.transform.position.y < -4.02) {
            mCamera.transform.position = new Vector3(transform.position.x, -4,transform.position.z);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && isOnGround == true && gameOver == false && canMove == true && isDead == false)
        {
            Jump();
        }
        if (transform.position.z < zRange || transform.position.z > zRange )
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zRange);
        }
        if (Input.GetAxis("Horizontal") > 0 && gameOver == false && canMove == true && isDead == false)
        {
            moveRight();
        }
        else if (Input.GetAxis("Horizontal") < 0 && gameOver == false && canMove == true && isDead == false)
        {
            moveLeft();
        }
        else {
            playeranim.SetBool("moving_b", false);
        }
        if (Input.GetKeyDown(KeyCode.Space) && gameOver == false && canShoot == true && numberOfB > 0 && isDead == false) {
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.R) && numberOfB <5 && isOnGround == true && isDead == false)
        {
            ReloadGun();
        }
        if (numberOfB > 0) {
            numberDisplay.text = "boolet: " + numberOfB.ToString();
        }
        if (numberOfB == 0) {
            numberDisplay.text = "boolet: 0 Press R";
        }
        if (isDead == true)
        {
            dead();
        }
        numberofEnemy.text = "Number of Enemy: " + GameObject.FindGameObjectsWithTag("Enemy").Length;
        DisplaynumberOfHeart.text = numberOfHeart.ToString();
    }
    IEnumerator Cooldown(float time)
    {
        yield return new WaitForSeconds(time);
        canShoot = true;
        canMove = true;
        numberOfB += (5 - numberOfB);
    }
    IEnumerator shotback(float time)
    {
        yield return new WaitForSeconds(time);
        canShoot = true;
    }
    IEnumerator coolDownpower (float time)
    {
        yield return new WaitForSeconds(time);
        powerUp = false;
        if (numberOfB>5)
        {
            numberOfB = 5;
        }
        StartCoroutine(shotback(0.6f));
        poweracc.SetActive(false);
    }

    public void dead()
    {
        playeranim.SetTrigger("dead_t");

    }
    public void moveRight()
    {
        transform.localEulerAngles = new Vector3(transform.rotation.x, 90, transform.rotation.z);
        transform.Translate(0f, 0f, Input.GetAxis("Horizontal") * speed * Time.deltaTime);
        faceRight = true;
        playeranim.SetBool("moving_b", true);
    }
    public void Jump()
    {
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        playeranim.SetTrigger("jump_t");
        isOnGround = false;
    }
    public void moveLeft()
    {
        transform.localEulerAngles = new Vector3(transform.rotation.x, -90, transform.rotation.z);
        transform.Translate(0f, 0f, -Input.GetAxis("Horizontal") * speed * Time.deltaTime);
        faceRight = false;
        playeranim.SetBool("moving_b", true);
    }
    public void Shoot()
    {
        playeranim.SetTrigger("shoot_t");
        if (faceRight == true)
        {
            Instantiate(bouletPrefab, shoutLoc.transform.position, bouletPrefab.transform.rotation);
        }
        else
        {
            Instantiate(leftbouletPrefab, shoutLoc.transform.position, leftbouletPrefab.transform.rotation);
        }
        numberOfB -= 1;
        canShoot = false;
        if (powerUp == false)
        {
            StartCoroutine(shotback(0.6f));
        }
        else if (powerUp == true){
            canShoot = true;
        }
    }
    public void ReloadGun()
    {
        canShoot = false;
        canMove = false;
        playeranim.SetTrigger("reload_t");
        StartCoroutine(Cooldown(1f));
    }
   
    public void takeoffheart(bool numof)
    {
        if (numof==true) {
            numberOfHeart -= 1;
        }
    }
    public int getnumOf()
    {
       return numberOfHeart;
    }
    public void setNumOf(int setnumOf)
    {
       numberOfHeart = 0;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("world") || collision.gameObject.CompareTag("wall"))
        {
            isOnGround = true;
        }
        if (collision.gameObject.CompareTag("underGround"))
        {
            gameOver = true;
        }
        if (collision.gameObject.CompareTag("powerUp"))
        {
            powerUp = true;
            StartCoroutine(coolDownpower(5f));
            Destroy(collision.gameObject);
            numberOfB += 15;
            poweracc.SetActive(true);
            audiopower.Play();
        }
        if (collision.gameObject.CompareTag("heartUP"))
        {
            numberOfHeart += 12;
            Destroy(collision.gameObject);
            audiopower.Play();
        }
    }
}
