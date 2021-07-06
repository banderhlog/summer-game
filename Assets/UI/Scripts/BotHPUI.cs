using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotHPUI : MonoBehaviour { 
    MaterialPropertyBlock matBlock;
    MeshRenderer          meshRenderer;
    Camera                mainCamera;
    Health                damageable;
    private void Awake() {
        meshRenderer = GetComponent<MeshRenderer>();
        matBlock = new MaterialPropertyBlock();
        damageable = GetComponentInParent<Health>();
    }
    private void Start() {
        mainCamera = Camera.main;
    }
    private void Update() {
        if (damageable.HealthPoint < damageable.MaxHP()) {
            meshRenderer.enabled = true;
            AlignCamera();
            UpdateParams();
        }
        else {
            meshRenderer.enabled = false;
        }
    }
    private void UpdateParams() {
        meshRenderer.GetPropertyBlock(matBlock);
        matBlock.SetFloat("_Fill", damageable.HealthPoint / (float)damageable.MaxHP());
        meshRenderer.SetPropertyBlock(matBlock);
    }
    private void AlignCamera() {
        if (mainCamera != null) {
            var camXform = mainCamera.transform;
            var forward = transform.position - camXform.position;
            forward.Normalize();
            var up = Vector3.Cross(forward, camXform.right);
            transform.rotation = Quaternion.LookRotation(forward, up);
        }
    }
}
