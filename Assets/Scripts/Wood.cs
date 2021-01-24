using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : MonoBehaviour
{
    public float speed=50f;
    Animator woodHit;
    void Start()
    {
        woodHit = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        transform.Rotate(0, 0, speed * Time.deltaTime);
    }

    IEnumerator TimeAnimate()
    {
        yield return new WaitForSeconds(0.5f);
        woodHit.SetBool("HitWood", false);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Knife")
        {
            GetComponent<ParticleSystem>().Play();
            woodHit.SetBool("HitWood", true);
            StartCoroutine("TimeAnimate");
            
        }
    }

}
