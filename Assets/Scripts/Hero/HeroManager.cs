using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroManager : MonoBehaviour {
    public MenuManager ManagerMenu;
    public int         Health;

    private Health HealthHero;
    private float  LowerLevelBoundary = -15f;
    bool pause = false;
    void Awake() {
          gameObject.AddComponent<MovingRig>();
          gameObject.AddComponent<Health>();
        HealthHero = GetComponent<Health>(); HealthHero.SetHealthPoint(Health);

        StartCoroutine(SoundSteps());
    }
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            pause = !pause;
            if (pause) ManagerMenu.StartPause();
            else       ManagerMenu.EndPause();
        }
        if (transform.position.y <= LowerLevelBoundary)
            HealthHero.Died = true;
        if (HealthHero.Died)
            ManagerMenu.PlayerIsDied();
    }
    [SerializeField]
    private float       DelaySoundSteps;
    [SerializeField]
    private AudioClip[] Steps;
    [SerializeField]
    private AudioSource HeroAudioSource;
    IEnumerator SoundSteps() {
        while (true) {
            if (GetComponent<CharacterController>().isGrounded)
                if (Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) {
                    HeroAudioSource.clip = Steps[Random.Range(0, Steps.Length)];
                    HeroAudioSource.Play();
                }
                else HeroAudioSource.Stop();
            yield return new WaitForSeconds(DelaySoundSteps);
        }
    }
}