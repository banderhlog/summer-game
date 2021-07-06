using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActiveEffect : MonoBehaviour {
    public enum Effect : int {
        Fire = 0,
        Poison,
        Water,
        Stun,
        Bleeding
    }
    public enum EffectPower : int {
        Low,
        Medium,
        Hard
    }
    public  Effect      BaffEffectName;
    public  EffectPower BaffEffectPower;
    private UnityEvent  Baff     = new UnityEvent();
    private UnityAction BaffAction;
    private bool        Cooldown = false;
    private float       CooldownTimer;
    private void Start() {
        GetComponent<Collider>().isTrigger = true;
    }
    public void StrenghtenEffect() {
        switch ((int)BaffEffectPower) {
            case (int)EffectPower.Low:
                switch ((int)BaffEffectName) {
                    case (int)Effect.Fire:
                        BaffAction   += Fire.LowFire;
                        CooldownTimer = Constant.DurationEffect.LowFireDuration;
                        Baff.AddListener(BaffAction);
                        break;
                    case (int)Effect.Poison:
                        BaffAction += Poison.LowPoison;
                        CooldownTimer = Constant.DurationEffect.LowPoisonDuration;
                        Baff.AddListener(BaffAction);
                        break;
                    case (int)Effect.Water:
                        BaffAction += Water.LowWater;
                        CooldownTimer = Constant.DurationEffect.LowWaterDuration;
                        Baff.AddListener(BaffAction);
                        break;
                    default:
                        print("ErrorEffectLow");
                        break;
                }
                break;

            case (int)EffectPower.Medium:
                switch ((int)BaffEffectName) {
                    case (int)Effect.Fire:
                        BaffAction += Fire.MediumFire;
                        CooldownTimer = Constant.DurationEffect.MediumFireDuration;
                        Baff.AddListener(BaffAction);
                        break;
                    case (int)Effect.Poison:
                        BaffAction += Poison.MediumPoison;
                        CooldownTimer = Constant.DurationEffect.MediumPoisonDuration;
                        Baff.AddListener(BaffAction);
                        break;
                    case (int)Effect.Water:
                        BaffAction += Water.MediumWater;
                        CooldownTimer = Constant.DurationEffect.MediumWaterDuration;
                        Baff.AddListener(BaffAction);
                        break;
                    default:
                        print("ErrorEffectMedium");
                        break;
                }
                break;

            case (int)EffectPower.Hard:
                switch ((int)BaffEffectName) {
                    case (int)Effect.Fire:
                        BaffAction += Fire.HardFire;
                        CooldownTimer = Constant.DurationEffect.HardFireDuration;
                        Baff.AddListener(BaffAction);
                        break;
                    case (int)Effect.Poison:
                        BaffAction += Poison.HardPoison;
                        CooldownTimer = Constant.DurationEffect.HardPoisonDuration;
                        Baff.AddListener(BaffAction);
                        break;
                    case (int)Effect.Water:
                        BaffAction += Water.HardWater;
                        CooldownTimer = Constant.DurationEffect.HardWaterDuration;
                        Baff.AddListener(BaffAction);
                        break;
                    default:
                        print("ErrorEffectHard");
                        break;
                }
                break;

            default:
                print("ErrorBaffEffectPower");
                break;
        }
    }
    private void OnTriggerEnter(Collider Creature) {
        if (!Cooldown) {
            Cooldown = true;
            StrenghtenEffect();
            Baff.Invoke();
            StartCoroutine(WaitCooldownAvoid(CooldownTimer));
        }
    }
    IEnumerator WaitCooldownAvoid(float CooldownTime) {
        yield return new WaitForSeconds(CooldownTime);
        Cooldown = false;
    }
}