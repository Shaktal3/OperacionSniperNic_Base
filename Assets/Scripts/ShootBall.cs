using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBall : MonoBehaviour
{
    [SerializeField]
    Transform hand;
    [SerializeField]
    GameObject ballPrefab;
    [SerializeField]
    float force;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject ball = Instantiate(ballPrefab, hand.position, Camera.main.transform.rotation);
            Rigidbody ballRb = ball.GetComponent<Rigidbody>();
            ballRb.AddForce(ball.transform.forward * force);

        }
    }
}
