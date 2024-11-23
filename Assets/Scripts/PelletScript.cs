using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletScrpt : MonoBehaviour
{

    public int PelletDamage;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            print("shot " + collision.gameObject.name);
            Destroy(gameObject);
 
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            print("walled");
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().DealDamage(PelletDamage);
            Destroy(gameObject);
        }
    }
}
