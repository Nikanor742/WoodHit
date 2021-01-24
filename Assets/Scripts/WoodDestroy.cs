using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodDestroy : MonoBehaviour
{
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * Random.Range(5,10), ForceMode2D.Impulse);
        if (Random.Range(0, 2) == 0)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.left * Random.Range(2, 5), ForceMode2D.Impulse);
        }
        else
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * Random.Range(2, 5), ForceMode2D.Impulse);
        }
        transform.Rotate(0, 0, Random.Range(-200, 200) * Time.deltaTime);
    }
}
