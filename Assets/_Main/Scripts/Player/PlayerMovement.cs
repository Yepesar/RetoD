using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private SOPlayerStats playerStats;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }      
    }

    private void Move(float x, float z)
    {
        Vector3 dir = new Vector3(x, 0 ,z);
        rb.velocity = dir * playerStats.MovementSpeed;
        transform.forward = Vector3.Lerp(transform.forward, rb.velocity, playerStats.RotationSpeed);   
    }
}
