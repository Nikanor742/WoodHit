using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{

    [SerializeField]
    private GameObject appleHalf;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Knife")
        {
            GameController.Instance.AppleHit();
            Instantiate(appleHalf, transform.position, transform.rotation);
            Instantiate(appleHalf, transform.position, transform.rotation);
            Destroy(gameObject);
        }

    }
}
