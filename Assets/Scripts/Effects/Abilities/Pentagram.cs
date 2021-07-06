using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pentagram : MonoBehaviour {
    public ActiveEffect.EffectPower BaffEffectPower;
    private void OnTriggerEnter(Collider collision) {
        if (collision.gameObject.tag == "Enemy") {
            switch ((int)BaffEffectPower) {
                case (int)ActiveEffect.EffectPower.Low:
                    collision.gameObject.GetComponent<Health>().PeriodicDamage(Constant.DurationEffect.LowFireDuration, Constant.DamageEffect.LowFireDamage);
                    break;
                case (int)ActiveEffect.EffectPower.Medium:
                    collision.gameObject.GetComponent<Health>().PeriodicDamage(Constant.DurationEffect.MediumFireDuration, Constant.DamageEffect.MediumFireDamage);
                    break;
                case (int)ActiveEffect.EffectPower.Hard:
                    collision.gameObject.GetComponent<Health>().PeriodicDamage(Constant.DurationEffect.HardFireDuration, Constant.DamageEffect.HardFireDamage);
                    break;
                default:
                    print("Error Pentagram");
                    break;
            }
        }
    }
}
