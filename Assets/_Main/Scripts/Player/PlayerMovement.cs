using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private SOPlayerStats playerStats;
    [SerializeField] private Transform body;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
       
    }

    private void Move(float x, float z)
    {
        Vector3 dir = new Vector3(x, 0, z);
        Vector3 rot = dir - transform.position;
        rot.Normalize();       
        rb.velocity = dir * playerStats.MovementSpeed;
        body.rotation = Quaternion.LookRotation(rot);
        
    }
}
