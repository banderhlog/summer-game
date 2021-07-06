using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : MonoBehaviour {//КОПИПАСТА
    static public void LowFire() {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        GameObject GameUI = GameObject.Find("GameUI/HealthBar");
        if (Player != null) {
            var Health = Player.GetComponent<Health>();
            var EffectPrinter = GameUI.GetComponent<GameUI>();
            Health.PeriodicDamage(Constant.DurationEffect.LowFireDuration, Constant.DamageEffect.LowFireDamage);
            EffectPrinter.AddEffect(ActiveEffect.Effect.Fire, Constant.DurationEffect.LowFireDuration);
        }
        else print("Player cannot be found (LowFire)");
    }
    static public void MediumFire() {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        GameObject GameUI = GameObject.Find("GameUI/HealthBar");
        if (Player != null) {
            var Health = Player.GetComponent<Health>();
            var EffectPrinter = GameUI.GetComponent<GameUI>();
            Health.PeriodicDamage(Constant.DurationEffect.MediumFireDuration, Constant.DamageEffect.MediumFireDamage);
            EffectPrinter.AddEffect(ActiveEffect.Effect.Fire, Constant.DurationEffect.MediumFireDuration);
        }
        else print("Player cannot be found (MediumFire)");
    }
    static public void HardFire() {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        GameObject GameUI = GameObject.Find("GameUI/HealthBar");
        if (Player != null) {
            var Health = Player.GetComponent<Health>();
            var EffectPrinter = GameUI.GetComponent<GameUI>();
            Health.PeriodicDamage(Constant.DurationEffect.HardFireDuration, Constant.DamageEffect.HardFireDamage);
            EffectPrinter.AddEffect(ActiveEffect.Effect.Fire, Constant.DurationEffect.HardFireDuration);
        }
        else print("Player cannot be found (HardFire)");
    }
}
