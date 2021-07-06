using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    public bool Died = false;
    public int  HealthPoint = 100;

    private int MaxHealthPoint;
    private void Start() {
        MaxHealthPoint = HealthPoint;
    }
    public void SetHealthPoint(int Health) {
        HealthPoint    = Health;
        MaxHealthPoint = Health;
    }
    public void AddHealthPoint(int BonusHealth) {
        HealthPoint    += BonusHealth;
        MaxHealthPoint += BonusHealth;
    }
    public void Heal(int HPregen) {
        HealthPoint += HPregen;
        if (HealthPoint > MaxHealthPoint) HealthPoint = MaxHealthPoint;
    }
    public void DealDamage(int Damage) {
        if (HealthPoint > 0)
            HealthPoint -= Damage;
        if (HealthPoint <= 0)
            if (gameObject.tag != "Player") Destroy(this.gameObject);
            else Died = true;
    }
    public void PeriodicDamage(float time, int Damage) {
        StartCoroutine(PeriodicDamageCor(time, Damage));
    }
    IEnumerator PeriodicDamageCor(float time, int Damage) {
        while (time > 0) {
            if (time < 1f) Damage /= 2;//если прошло половинчатое время
            if (HealthPoint > 0)
                HealthPoint -= Damage;
            if (HealthPoint <= 0)
                if (gameObject.tag != "Player") Destroy(this.gameObject);
                else Died = true;
            time -= 1f;
            yield return new WaitForSeconds(1f);
        }
    }
    public float PercentOfLife() {
        return (float)HealthPoint / MaxHealthPoint;
    }
    public int MaxHP() {
        return MaxHealthPoint;
    }
}
