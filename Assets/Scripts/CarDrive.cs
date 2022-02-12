using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDrive : MonoBehaviour
{
 public float speed;
 public float turnSpeed;
 public float boostSpeed;
 public float brakingSpeed;
 public float gravityMultiplier;
 private Rigidbody rb;

 // Start is called before the first frame update
 void Start()
 {
  rb = GetComponent<Rigidbody>();
 }

 // Update is called once per frame
 void FixedUpdate()
 {
  Accelerate();
  Turn();
  Fall();
  Boost();
  //Brake();
 }

 void Accelerate()
 {
  if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
  {
   Vector3 forceToAdd = transform.forward;
   forceToAdd.y = 0;
   rb.AddForce(forceToAdd * speed * 10);
  }
  else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
  {
   Vector3 forceToAdd = -transform.forward;
   forceToAdd.y = 0;
   rb.AddForce(forceToAdd * speed * 10);
  }

  Vector3 locVel = transform.InverseTransformDirection(rb.velocity);
  locVel = new Vector3(0, locVel.y, locVel.z);
  rb.velocity = new Vector3(transform.TransformDirection(locVel).x, rb.velocity.y, transform.TransformDirection(locVel).z);
 }

 void Turn()
 {
  if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
  {
   rb.AddTorque(-Vector3.up * turnSpeed * 10);
  }
  else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
  {
   rb.AddTorque(Vector3.up * turnSpeed * 10);
  }
 }

 void Fall()
 {
  rb.AddForce(Vector3.down * gravityMultiplier * 10);
 }

 void Boost()
 {
  // if (Input.GetKey(KeyCode.Space))
  // {
  //  rb.velocity = Vector3.zero;
  //  rb.angularVelocity = Vector3.zero;
  // }
  Vector3 zeroVector;
  zeroVector.x = zeroVector.y = zeroVector.z = 0;

  if (rb.velocity.y > zeroVector.y && Input.GetKey(KeyCode.LeftShift))
  {
   Vector3 forceToAdd = -transform.forward;
   forceToAdd.y = 0;
   rb.AddForce(forceToAdd * boostSpeed * 10);
  }
  else if (rb.velocity.y <= zeroVector.y && Input.GetKey(KeyCode.LeftShift))
  {
   Vector3 forceToAdd = transform.forward;
   forceToAdd.y = 0;
   rb.AddForce(forceToAdd * boostSpeed * 10);
  }

  Vector3 locVel = transform.InverseTransformDirection(rb.velocity);
  locVel = new Vector3(0, locVel.y, locVel.z);
  rb.velocity = new Vector3(transform.TransformDirection(locVel).x, rb.velocity.y, transform.TransformDirection(locVel).z);
 }

 void Brake()
 {
  Vector3 zeroVector = Vector3.zero;

  if (rb.velocity.z < zeroVector.z && Input.GetKey(KeyCode.Space))
  {
   Debug.Log("if condition : " + rb.velocity);
   Vector3 forceToAdd = transform.forward;
   forceToAdd.z = 0;
   rb.AddForce(forceToAdd * brakingSpeed * 10);
  }
  else if (rb.velocity.z > zeroVector.z && Input.GetKey(KeyCode.Space))
  {
   Debug.Log("if else condition : " + rb.velocity);
   Vector3 forceToAdd = -transform.forward;
   forceToAdd.z = 0;
   rb.AddForce(forceToAdd * brakingSpeed * 10);
  }
  Vector3 locVel = transform.InverseTransformDirection(rb.velocity);
  locVel = new Vector3(locVel.x, locVel.y, locVel.z);
  Debug.Log("locVel : " + rb.velocity);
  if (locVel.z != 0)
  {
   if (locVel.z <= -100)
   {
    locVel = zeroVector;
   }
   rb.velocity = new Vector3(transform.TransformDirection(locVel).x, rb.velocity.y, transform.TransformDirection(locVel).z);
  }
 }
}
