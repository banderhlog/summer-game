using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : MonoBehaviour {
    static public void LowPoison() {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        GameObject GameUI = GameObject.Find("GameUI/HealthBar");
        if (Player != null) {
            var Health        = Player.GetComponent<Health>();
            var EffectPrinter = GameUI.GetComponent<GameUI>();
            Health.PeriodicDamage(Constant.DurationEffect.LowPoisonDuration, Constant.DamageEffect.LowPoisonDamage);
            EffectPrinter.AddEffect(ActiveEffect.Effect.Poison, Constant.DurationEffect.LowPoisonDuration);
        }
        else print("Player cannot be found (LowPoison)");
    }
    static public void MediumPoison() {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        GameObject GameUI = GameObject.Find("GameUI/HealthBar");
        if (Player != null) {
            var Health        = Player.GetComponent<Health>();
            var EffectPrinter = GameUI.GetComponent<GameUI>();
            Health.PeriodicDamage(Constant.DurationEffect.MediumPoisonDuration, Constant.DamageEffect.MediumPoisonDamage);
            EffectPrinter.AddEffect(ActiveEffect.Effect.Poison, Constant.DurationEffect.MediumPoisonDuration);
        }
        else print("Player cannot be found (MediumPoison)");
    }
    static public void HardPoison() {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        GameObject GameUI = GameObject.Find("GameUI/HealthBar");
        if (Player != null) {
            var Health        = Player.GetComponent<Health>();
            var EffectPrinter = GameUI.GetComponent<GameUI>();
            Health.PeriodicDamage(Constant.DurationEffect.HardPoisonDuration, Constant.DamageEffect.HardPoisonDamage);
            EffectPrinter.AddEffect(ActiveEffect.Effect.Poison, Constant.DurationEffect.HardPoisonDuration);
        }
        else print("Player cannot be found (HardPoison)");
    }
}
