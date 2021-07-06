using System;
using UnityEngine;

public class Teleporter : MonoBehaviour{
    public Teleporter Other;
    private void OnTriggerStay(Collider LoggedInPortal) {
        //Если мы прошли портал, т.е. мы оказались "за ним" – PassedPortal < 0, хоть это и менее оптимально, но переход за то более гладкий
        if (LoggedInPortal.gameObject.tag != "Weapon") {
            float PassedPortal = transform.worldToLocalMatrix.MultiplyPoint3x4(LoggedInPortal.transform.position).z;
            if (PassedPortal < 0) Teleport(LoggedInPortal.transform);
        }
    }
    private void Teleport(Transform Player) {
        Vector3 LocalPositionPlayer = transform.worldToLocalMatrix.MultiplyPoint3x4(Player.position);
                LocalPositionPlayer = new Vector3(-LocalPositionPlayer.x, LocalPositionPlayer.y, -LocalPositionPlayer.z);
        Player.position = Other.transform.localToWorldMatrix.MultiplyPoint3x4(LocalPositionPlayer);

        Quaternion difference = Other.transform.rotation * Quaternion.Inverse(transform.rotation * Quaternion.Euler(0, 180, 0));
              Player.rotation = difference * Player.rotation;
    }
    /*
    private void OnTriggerEnter(Collider other) {
        other.gameObject.layer = 9;
    }

    private void OnTriggerExit(Collider other)
    {
        other.gameObject.layer = 8;
    }*/

    /*
      private Portal _portal1;
    private Portal _portal2;

    private Collider _collider;

    private Transform CameraVision;

    private void Awake() {
        _collider = GetComponent<Collider>();
        CameraVision = gameObject.transform.GetChild(2);
    }

    public void EnterPortal(Portal enterPortal, Portal exitPortal, Collider[] wallColliders) {
        _portal1 = enterPortal;
        _portal2 = exitPortal;
        for (int i = 0; i < wallColliders.Length; i++)
            Physics.IgnoreCollision(_collider, wallColliders[i]);
    }

    public void ExitPortal(Collider[] wallColliders) {
        for (int i = 0; i < wallColliders.Length; i++)
            Physics.IgnoreCollision(_collider, wallColliders[i], false);
    }

    public void Teleport() {
        // Position
        Vector3 localPos = transform.worldToLocalMatrix.MultiplyPoint3x4(transform.position);
        localPos = new Vector3(-localPos.x, localPos.y, -localPos.z);
        transform.position = _portal2.transform.transform.localToWorldMatrix.MultiplyPoint3x4(localPos);

        // Rotation
        Quaternion difference = _portal1.transform.transform.rotation * Quaternion.Inverse(transform.rotation * Quaternion.Euler(0, 180, 0));
        transform.rotation = difference * transform.rotation;

        //var p1 = _portal1.transform;
        //var p2 = _portal2.transform;

        //transform.MirrorPosition(p1, p2);
        //var RotateSukaEtuHuetuPozgalusta = p2.rotation.eulerAngles;
        //RotateSukaEtuHuetuPozgalusta.y = -RotateSukaEtuHuetuPozgalusta.y;
        //CameraVision.transform.rotation = Quaternion.Euler(RotateSukaEtuHuetuPozgalusta);
        //CameraVision.transform.MirrorRotation(p1, p2);
    }
     */
}
