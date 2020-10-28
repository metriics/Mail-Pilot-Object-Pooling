using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class BulletController : MonoBehaviour
{
    public float bulletSpeed = 0.1f;
    public Boundary boundary;

    public GameObject bulletPoolManager;

    void Start()
    {
        boundary.Top = 2.45f;

        // find the pool manager using Manager tag
        bulletPoolManager = GameObject.FindGameObjectWithTag("Manager");
    }


    // Update is called once per frame
    void Update()
    {
        Move();
        CheckBounds();
    }

    private void Move()
    {
        transform.position += new Vector3(0.0f, bulletSpeed * Time.deltaTime, 0.0f);
    }

    private void CheckBounds()
    {
        if (transform.position.y >= boundary.Top)
        {
            bulletPoolManager.GetComponent<BulletPoolManager>().ResetBullet(this.gameObject);
        }
    }
}
