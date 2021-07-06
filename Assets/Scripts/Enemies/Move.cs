using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
    public  float speed     = 5.0f;
    private float gravity   = 20.0f;
    private CharacterController Controller;
    void Start() {
        Controller = GetComponent<CharacterController>();
    }
    public void MovingToward(Vector3 MoveDirection, float BonusSpeed = 1f) {
        MoveDirection      *= speed / MoveDirection.magnitude * BonusSpeed;
        MoveDirection.y    -= gravity * Time.deltaTime;
        Controller.Move(MoveDirection * Time.deltaTime);
    }
    public void RotationToward(Vector3 RotationDirection) {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(RotationDirection), 3f * Time.deltaTime);
    }
    public void AvoidAttack(Vector3 MoveDirection) {
        MoveDirection *= 150.0f / MoveDirection.magnitude;
        MoveDirection.y -= gravity * Time.deltaTime;
        Controller.Move(-MoveDirection * Time.deltaTime);
    }
}
