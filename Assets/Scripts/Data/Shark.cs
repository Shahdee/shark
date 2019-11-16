using UnityEngine;
using System.Collections;

// move on map 
// eat swimmers 
// die because of mines 

public class Shark : Object 
{
    public Rigidbody2D m_Rigidbody;

    Transform m_Transform;

    public float m_MaxVelocity = 3;

    public float m_RotationSpeed = 3;

    void Start(){
        m_Transform = transform;
    }

    float m_yAxis;
    float m_xAxis;
    Vector2 m_Force;
   
    void Update ()
    {
       m_xAxis = Input.GetAxis("Horizontal");
       m_yAxis = Input.GetAxis("Vertical");
       
       ThrustForward(m_yAxis);
       Rotate(m_xAxis * -m_RotationSpeed);
    }

    float m_VelocityX;
    float m_VelocityY;

    Vector2 m_CurrVelocity;

    void ClampVelocity(){

        m_VelocityX = Mathf.Clamp(m_Rigidbody.velocity.x, -m_MaxVelocity, m_MaxVelocity);
        m_VelocityY = Mathf.Clamp(m_Rigidbody.velocity.y, -m_MaxVelocity, m_MaxVelocity);

        m_CurrVelocity.x = m_VelocityX;
        m_CurrVelocity.y = m_VelocityY;

        m_Rigidbody.velocity = m_CurrVelocity;
    }

    void ThrustForward(float amount){
        m_Force = m_Transform.up * amount;

        m_Rigidbody.AddForce(m_Force);

        ClampVelocity();
    }

    void Rotate(float amount){
        m_Transform.Rotate(0,0, amount);

    }
}