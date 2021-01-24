using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Knife : MonoBehaviour
{
    [SerializeField]
    private bool inWood = false;
    [SerializeField]
    private bool trigerKnife = false;
    public bool isActive;
    
    public AudioSource hitSound;
    public AudioSource knifeHitSound;
    public GameObject knifeSparks;
    public float force = 5f;
    public AudioSource knifeSound;



    private void Start()
    {
        Vibration.Init();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isActive)
        {
            knifeSound.Play();
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * force, ForceMode2D.Impulse);
            GameController.Instance.GameUI.DecrementDisplayedKnifeCount();
            isActive = false;
        }
    }
     private void PhysicsKnife()
    {
        GetComponent<Rigidbody2D>().freezeRotation = false;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        transform.Rotate(0, 0, Random.Range(-1000, 1000) * Time.deltaTime);
        GetComponent<Rigidbody2D>().AddForce(Vector2.down * Random.Range(5, 10), ForceMode2D.Impulse);
        
        if (Random.Range(0, 2) == 0)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.left * Random.Range(2, 5), ForceMode2D.Impulse);
        }
        else
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * Random.Range(2, 5), ForceMode2D.Impulse);
        }
        
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Knife")
        {
            trigerKnife = true;
            if (col.GetComponent<Knife>().inWood == true && inWood!=true)
            {
                transform.SetParent(null);
                GetComponent<BoxCollider2D>().isTrigger = false;
                GameController.Instance.game = false;
                GameController.Instance.StartGameOver(false);
                Vibration.Vibrate(300);
                Instantiate(knifeSparks, transform.position, transform.rotation);
                knifeHitSound.Play();
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                GetComponent<Rigidbody2D>().gravityScale = 1;
                PhysicsKnife();
            }
        }
        else if (col.tag == "KnifeInWood")
        {
            trigerKnife = true;
            if (col.GetComponent<Knife>().inWood == true && inWood != true)
            {
                transform.SetParent(null);
                GetComponent<BoxCollider2D>().isTrigger = false;
                GameController.Instance.game = false;
                GameController.Instance.StartGameOver(false);
                Vibration.Vibrate(300);
                Instantiate(knifeSparks, transform.position, transform.rotation);
                knifeHitSound.Play();
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                GetComponent<Rigidbody2D>().gravityScale = 1;
                PhysicsKnife();
            }
        }
        else if (col.tag == "Wood" &&!trigerKnife)
        {
            GetComponent<BoxCollider2D>().isTrigger = false;
            inWood = true;
            GameController.Instance.KnivesScore();
            GameController.Instance.OnKnifeHit();
            
            hitSound.Play();
            isActive = false;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            transform.parent = col.transform;
            
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
            inWood = true;
            
            Vibration.Vibrate(50);
            
            
        }
    }
}
