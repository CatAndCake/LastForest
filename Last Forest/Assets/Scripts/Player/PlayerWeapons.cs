using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    PlayerBattle playerBattle;

    // Start is called before the first frame update
    void Start()
    {
        playerBattle = this.gameObject.GetComponentInParent<PlayerBattle>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == ("Enemy"))
        {
            playerBattle.WeaponCollision(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == ("Enemy"))
        {
            playerBattle.EndWeaponCollision();
        }
    }
}
