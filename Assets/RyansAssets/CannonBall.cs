﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public Camera mainCamera;
    public float angle;
    public GameObject cannonBall;

    private Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                target = hit.point;
                GameObject projectile = Instantiate(cannonBall, transform.position, Quaternion.identity);
                projectile.GetComponent<Rigidbody>().velocity = BalisticVelocity(target, angle);
                //projectile.GetComponent<Rigidbody>().useGravity = false;
                Destroy(projectile, 10);
            }
        }
    }

    public Vector3 BalisticVelocity(Vector3 target, float fireangle)
    {
        Vector3 dir = target - transform.position;
        float h = dir.y;
        //dir.y = 0;
        float dist = dir.magnitude;
        float a = fireangle * Mathf.Deg2Rad;
        dir.y = dist * Mathf.Tan(a);
        dist += h / Mathf.Tan(a);
        float vel = Mathf.Sqrt(dist * Physics.gravity.magnitude / Mathf.Sin(2 * a));
        return vel * dir.normalized;
    }
}
