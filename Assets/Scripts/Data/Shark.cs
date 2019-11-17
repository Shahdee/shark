using UnityEngine;
using System.Collections;
using UnityEngine.Events;

// eat swimmer 
// collide with bomb
// move in environment

public class Shark : GObject 
{
    public Rigidbody2D m_Rigidbody;
    public float m_RotationSpeed = 3;
    public float m_Velocity = 1;

    public enum MovementType{
        Slide,
        ManualRotation,
        AdaptiveRotation,
    }

    public MovementType m_MovementType = MovementType.Slide;

    InputManager inputManager;

    public override void Init(MainLogic logic){

       inputManager = MainLogic.GetMainLogic().GetInputManager();

    } 

#region Events

    UnityAction<GObject> m_OnSwimmerCollide;

    public void AddSwimmerCollide(UnityAction<GObject> listener){
        m_OnSwimmerCollide += listener;
    }

    public void RemoveSwimmerCollide(UnityAction<GObject> listener){
        m_OnSwimmerCollide -= listener;
    }

    void OnSwimmerCollide(GObject swimmer){

        if (m_OnSwimmerCollide != null)
            m_OnSwimmerCollide(swimmer);
    }

    UnityAction<GObject> m_OnMineCollide;

    public void AddMineCollide(UnityAction<GObject> listener){
        m_OnMineCollide += listener;
    }

    public void RemoveMineCollide(UnityAction<GObject> listener){
        m_OnMineCollide -= listener;
    }

    void OnMineCollide(GObject mine){
           if (m_OnMineCollide != null)
            m_OnMineCollide(mine);
    }

#endregion

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("enter " + collision.gameObject.name);

        GObject obj = collision.transform.GetComponent<GObject>();
        if (obj == null)
            return;
        else{
            switch(obj.GetObjType()){
                 case GObject.ObjectType.SwimmerStatic:
                    OnSwimmerCollide(obj);
                break;

                case GObject.ObjectType.Swimmer:
                    OnSwimmerCollide(obj);
                break;

                case GObject.ObjectType.Mine:
                    OnMineCollide(obj);
                break;
            }
        }
    }



#region Movements

    void Update(){  
        switch(m_MovementType){
            case MovementType.Slide:
                UpdateSlide();
            break;

            case MovementType.ManualRotation:
                UpdateManualRotation();
            break;

            case MovementType.AdaptiveRotation:
                UpdateAdaptiveRotation();
            break;
        }

        UpdateCamera();
    }

    void UpdateSlide(){
        m_Rigidbody.AddForce(inputManager.GetHorizontal() * Vector2.right * m_Velocity * Time.deltaTime); 
        m_Rigidbody.AddForce(inputManager.GetVertical() * Vector2.up * m_Velocity * Time.deltaTime); 
    }

    float rotationAmount;

    void UpdateManualRotation(){
        m_Rigidbody.AddForce(inputManager.GetVertical() * m_Transform.up * m_Velocity * Time.deltaTime);
        rotationAmount  = m_RotationSpeed * Time.deltaTime * inputManager.GetHorizontal();
        m_Rigidbody.AddTorque(-rotationAmount, ForceMode2D.Force);
    }

    Vector2 direction = new Vector2();
    Vector2 normDirection = new Vector2();
    float thrust;
    float rotation;

    void UpdateAdaptiveRotation(){
        direction.x = inputManager.GetHorizontal();
        direction.y = inputManager.GetVertical() ;
        normDirection = direction.normalized;

        thrust = Vector2.Dot(normDirection, m_Transform.up);
        rotation = Vector2.Dot(normDirection, m_Transform.right);

        m_Rigidbody.AddForce(thrust * direction.magnitude * m_Transform.up * m_Velocity * Time.deltaTime);

        rotationAmount  = m_RotationSpeed * Time.deltaTime * rotation;
        m_Rigidbody.AddTorque(-rotationAmount, ForceMode2D.Force);
    }

    Vector3 m_CameraPos;

    // hacky..
    void UpdateCamera(){

        m_CameraPos = Camera.main.transform.position;
        m_CameraPos.x = m_Transform.position.x;
        m_CameraPos.y = m_Transform.position.y;

        Camera.main.transform.position = m_CameraPos;

    }

#endregion 
}