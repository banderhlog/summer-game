using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour {
    public int AverageDPS;
    [SerializeField]
    private GameObject Weapon;
    private Collider   WeaponCollider;
    private Animation  AttackAnimation;//ставим сюда аниматор произвольного оружия, в будущем
    private WeaponCollision WeaponColl;
    private void Start() {
        WeaponCollider  = Weapon.GetComponent<Collider>();
        AttackAnimation = Weapon.GetComponent<Animation>();
        WeaponColl      = Weapon.GetComponent<WeaponCollision>();
    }
    public void Attack(int Damage) {
        AttackAnimation.Play("EnemyAttack");
        WeaponColl.IsAttack     = true;
        WeaponColl.AttackDamage = Damage;
        StartCoroutine(DefineBehavior());
    }
    IEnumerator DefineBehavior() {
        yield return new WaitForSeconds(0.4f);
        WeaponColl.IsAttack     = false;
        WeaponColl.AttackDamage = 0;
    }
}
