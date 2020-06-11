using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Vector3 shootdirection;

    public void Setup(Vector3 shootdirection)
    {
        this.shootdirection = shootdirection;
    }

    void Update() 
    {
        transform.position += shootdirection * Time.deltaTime * 10;
    }

    private void OnCollisionEnter(Collision other) 
    {
        Destroy(this.gameObject);
    }
}
