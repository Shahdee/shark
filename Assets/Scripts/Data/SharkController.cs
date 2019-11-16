// using UnityEngine;
// using System.Collections;

// public class SharkController : MonoBehaviour 
// {
//     public Rigidbody2D m_Rigidbody;

//     Transform m_Transform;

//     public float m_MaxVelocity = 3;

//     public float m_RotationSpeed = 3;

//     void Start(){
//         m_Transform = transform;
//     }

//     float m_yAxis;
//     float m_xAxis;
//     Vector2 m_Force;
   
//     void Update ()
//     {
//        m_xAxis = Input.GetAxis("Horizontal");
//        m_yAxis = Input.GetAxis("Vertical");
       
      
//     }

//     float m_VelocityX;
//     float m_VelocityY;

//     Vector2 m_CurrVelocity;

//     void Move(){
//         m_Rigidbody.AddForce();
//     }
// }