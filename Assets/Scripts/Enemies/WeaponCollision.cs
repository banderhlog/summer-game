using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollision : MonoBehaviour {
    public bool IsAttack     = false;
    public int  AttackDamage = 0;
    private void OnTriggerEnter(Collider collision) {
        if (collision.gameObject.tag == "Player" && IsAttack)
            collision.gameObject.GetComponent<Health>().DealDamage(AttackDamage);
    }
}
