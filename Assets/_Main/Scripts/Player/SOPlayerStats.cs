using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Scriptable Objects/Player Stats")]
public class SOPlayerStats : ScriptableObject
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;

    public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
    public float RotationSpeed { get => rotationSpeed; set => rotationSpeed = value; }
}
