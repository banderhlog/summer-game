using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MovingRig : MonoBehaviour {
    public float MoveSpeed     = 12.5f;
    public float RotationSpeed = 3f;
    public float JumpForce     = 8f;
    public float DashForce     = 17.5f;
    public float DashCooldown  = 3f;
    public float DashTime      = 1.25f;
    public float Gravity       = 10.0f;
    public float BonusSpeed    = 1f;//ВРЕМЕННАЯ ХРЕНЬ, ЕЕ НУЖНО БУДЕТ СПИЛИТЬ НАХУЙ БЛЯТЬ

    private CharacterController HeroController;
    private Vector3   MoveVector = Vector3.zero;
    private Vector3   DashVector = Vector3.zero;
    private Camera    HeroCamera;
    private float     RealDashCooldown;
    void Start() {
        HeroController   = GetComponent<CharacterController>();
        HeroCamera       = GameObject.Find("HeroCamera").GetComponent<Camera>();
        RealDashCooldown = DashCooldown;
    }
    void Update() {
        if (Time.timeScale != 0) {//Если не пауза
            SetMoveVector();
            if (HeroController.isGrounded) {
                if (Input.GetKeyDown(KeyCode.Space))//Прыжок
                    MoveVector.y = JumpForce;
            }
            else if ( MoveVector.y > -10f) MoveVector.y -= Gravity * Time.deltaTime;
                 else MoveVector.y = -10f;//костыль, который не фиксится чето, когда мы ходим по земле все рано ебашим -Gravity и есл сойти с уступа пидарасит вниз люто
            //Рывок
            if (Input.GetKeyUp(KeyCode.LeftShift) && RealDashCooldown <= 0)
                DashDirection();
            //Обновление положения и кулдаунов
            RealDashCooldown -= Time.deltaTime;
            HeroController.Move((MoveVector + DashVector) * Time.deltaTime);
        }
    }
    public void SetMoveVector() {
                   transform.Rotate(0, Input.GetAxis("Mouse X") * RotationSpeed, 0);
        //HeroCamera.transform.Rotate(  -Input.GetAxis("Mouse Y") * RotationSpeed, 0, 0); лаконичная версия, но башкой на 360 крутить можно
        Vector3 Rotation    = new Vector3(-Input.GetAxis("Mouse Y") * RotationSpeed, 0, 0);
        Vector3 TargetEuler = HeroCamera.transform.eulerAngles + Rotation;
            if (TargetEuler.x > 180.0f)       TargetEuler.x -= 360.0f;
                TargetEuler.x   = Mathf.Clamp(TargetEuler.x, -65.0f, 65.0f);
        HeroCamera.transform.rotation = Quaternion.Euler(TargetEuler);

        if (HeroController.isGrounded)
            MoveVector = new Vector3(Input.GetAxis("Horizontal") * MoveSpeed, MoveVector.y, Input.GetAxis("Vertical") * MoveSpeed);
        else
            MoveVector = new Vector3(Input.GetAxis("Horizontal") * MoveSpeed * 0.75f, MoveVector.y, Input.GetAxis("Vertical") * MoveSpeed * 0.75f);
            MoveVector = transform.TransformDirection(MoveVector);
        //Если персонаж перевернулся, также нужно выравнивать камеру, но с ней какой-то трабл при выравнивании
        if (transform.rotation.eulerAngles.x != 0 || transform.rotation.eulerAngles.z != 0)
            transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0));
    }
    public void DashDirection(bool DashAnimation = false, bool Satana = true) {//ПЕРЕДЕЛАТЬ ЭТN КРИНЖ КОСТЫЛИ
        if (!(MoveVector.magnitude < 0.3f) && !DashAnimation) {//если мы мало сдвинулись, то нельзя дешится
            DashVector = new Vector3(MoveVector.x, 0f, MoveVector.z);
            DashVector /= DashVector.magnitude;
        }
        else if (Satana)
            DashVector = HeroCamera.transform.forward / HeroCamera.transform.forward.magnitude;
        
             if (Satana) StartCoroutine(Dash());
        RealDashCooldown = DashCooldown;
    }
    public float DashCurve(float t, float time) {
        return 4 * (t / time) * (1 - (t / time));
    }
    IEnumerator Dash() {
        float StartTime = Time.time;
        float  RealTime = StartTime;
        Vector3 DashVectorDirection = DashVector;
        while (RealTime < StartTime + DashTime) {
            RealTime += Time.deltaTime;
            float f   = DashCurve(RealTime - StartTime, DashTime);
            HeroCamera.fieldOfView = 105f * f + (1 - f) * 80f;
            DashVector = f * DashForce * DashVectorDirection + (1 - f) * Vector3.zero;
            yield return null; 
        }
        HeroCamera.fieldOfView = 80f;
                    DashVector = Vector3.zero;
    }
}