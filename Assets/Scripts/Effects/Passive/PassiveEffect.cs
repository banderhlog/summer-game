using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PassiveEffect : MonoBehaviour {/*
    Вампиризм
Дает восстановление здоровья от нанесенного урона.Минимум 0, максимум 100 вампиризма
Когда атака с вампиризмом восстанавливает здоровье, то вокруг персонажа появляются зеленые полосы(~как северное сияние, только частицы реже, но количественно больше)

Уклонение
Дает шанс уклониться от наносимой атаки.Минимум 0, максимум 100 процентов
Если уклонение было удачное, то на игроке отображается эффект блюра

Проклятье
Эффект, что копится на игроке при столкновении с некоторыми видами врагов, при заполнении на 100%, дает эффект “Смертельная болезнь”. После он не заполняется.Медленно спадает вне боя. (По 1% в 2 секунды)

Критический урон
С каким-то шансом наш урон будет увеличен в какое-то число раз
    */
    public enum Effect : int {
        Vampirism = 0,
        Evasion,
        Curse,
        CriticalDamage
    }
    public Effect BaffEffectName;
    private UnityEvent Baff = new UnityEvent();
    private UnityAction BaffAction;
    private bool Cooldown = false;
    private float CooldownTimer;
    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;
    }
    public void StrenghtenEffect()
    {
        
    }
    private void OnTriggerEnter(Collider Creature)
    {
        if (!Cooldown)
        {
            Cooldown = true;
            StrenghtenEffect();
            Baff.Invoke();
            StartCoroutine(WaitCooldownAvoid(CooldownTimer));
        }
    }
    IEnumerator WaitCooldownAvoid(float CooldownTime)
    {
        yield return new WaitForSeconds(CooldownTime);
        Cooldown = false;
    }
}
