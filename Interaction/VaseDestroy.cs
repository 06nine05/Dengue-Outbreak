using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaseDestroy : MonoBehaviour
{
    public Attack Ak;
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Weapon" && Ak.IsAttacking)
        {
            Debug.Log("Working");
            Destroy(gameObject);
        }
    }
}
