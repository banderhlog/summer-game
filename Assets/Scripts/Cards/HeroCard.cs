using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class HeroCard : MonoBehaviour {/*
    Сила          /Strength
    Осмотрительность/Discretion
    Скорость      /Speed
    Безумие       /Madness
    Справедливость/Justice
    Алчность      /Greed
    Гибель        /Death
    Опустошение   /Devastation
    Восстановление/Recovery
    Благородство  /Nobleness
    Гордость      /Pride
    Знания        /Knowledge
    */
    public enum Card : int{
        Strength,
        Discretion,
        Speed,
        Madness,
        Justice,
        Greed,
        Death,
        Devastation,
        Recovery,
        Nobleness,
        Pride,
        Knowledge
    }
    public  Card        BaffCardName;
    private UnityEngine.Events.UnityEvent Baff = new UnityEngine.Events.UnityEvent();
    private UnityAction CardAction;
    private void Start() {
        GetComponent<Collider>().isTrigger = true;
    }
    public void StrenghtenHeroByCard() {
        switch ((int)BaffCardName) {
            case (int)Card.Strength:
                CardAction += Strength.StrengthBaff;
                Baff.AddListener(CardAction);
                break;
            case (int)Card.Discretion:
                CardAction += Discretion.DiscretionBaff;
                Baff.AddListener(CardAction);
                break;
            case (int)Card.Speed:
                CardAction += Speed.SpeedBaff;
                Baff.AddListener(CardAction);
                break;
            case (int)Card.Greed:
                CardAction += Greed.GreedBaff;
                Baff.AddListener(CardAction);
                break;
            case (int)Card.Devastation:
                CardAction += Devastation.DevastationBaff;
                Baff.AddListener(CardAction);
                break;
            case (int)Card.Recovery:
                CardAction += Recovery.RecoveryBaff;
                Baff.AddListener(CardAction);
                break;
            default:
                print("ErrorCard");
                break;
        }
    }
    private void OnTriggerEnter(Collider Creature) {
        if (Creature.gameObject.tag == "Player") {
            StrenghtenHeroByCard();
            Baff.Invoke();
            Destroy(this.gameObject);
        }
    }
}
