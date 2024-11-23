using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Camera PlayerCamera;
    public int GunDamage;

    //Shooting Gun
    public bool isFiring, Ready2Fire;
    bool allowReset = true;
    public float FiringDelay = 1f;

    //MultiPellet Mode
    public int PelletsPerMultimode = 3;
    public int MultiMode;

    //Spread
    public float SpreadIntensity;

    //Pellet Properties
    public GameObject pelletPrefab;
    public Transform pelletSpawn;
    public float PelletV = 30;
    public float PelletPrefabT = 3f;

    public enum ShootMode
    {
        once,
        multi,
        rapidfire
    }

    public ShootMode CurrentFiringMode;

    private void Awake()
    {
        Ready2Fire = true;
        MultiMode = PelletsPerMultimode;
    }

    void Update()
    {
        if (CurrentFiringMode == ShootMode.rapidfire)
        {
            //Hold for rapid fire
            isFiring = Input.GetKey(KeyCode.Mouse0);
        }
        else if(CurrentFiringMode == ShootMode.once || CurrentFiringMode == ShootMode.multi)
        { 
            isFiring = Input.GetKeyDown(KeyCode.Mouse0);
        }

        if (Ready2Fire && isFiring)
        {
            MultiMode = PelletsPerMultimode;
            ShootGun();
        }
    }

    private void ShootGun()
    {
        Ready2Fire = false;
        Vector3 FiringDirection = CalculateDirectionAndSpeed().normalized;

        //Spawn and shoot the bullet
        GameObject pellet = Instantiate(pelletPrefab, pelletSpawn.position, Quaternion.identity);
        PelletScrpt pel = pellet.GetComponent<PelletScrpt>();
        pel.PelletDamage = GunDamage;

        // Pointing the pellet to face the direction 
        pellet.transform.forward = FiringDirection;

        //Shoot the pellet
        pellet.GetComponent<Rigidbody>().AddForce(FiringDirection * PelletV, ForceMode.Impulse);

        //Destroy the bullet after some time
        StartCoroutine(DestroyPelletAfterTime(pellet, PelletPrefabT));

        //Checking if the shooting is finished
        if (allowReset)
        {
            Invoke("ResetShot", FiringDelay);
            allowReset = false;
        }

        //Multi Mode
        if (CurrentFiringMode == ShootMode.multi && MultiMode > 1)
        {
            MultiMode--;
            Invoke("ShootWeapon", FiringDelay);
        }
    }
     private void ResetShot()
    {
        Ready2Fire = true;
        allowReset = true;
    }

    public Vector3 CalculateDirectionAndSpeed()
    {
        Ray R = PlayerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 TargetSpot;
        if (Physics.Raycast(R,out hit))
        {
            TargetSpot = hit.point;
        }
        else
        {
            TargetSpot = R.GetPoint(100);
        }

        Vector3 direction = TargetSpot - pelletSpawn.position;

        float x = UnityEngine.Random.Range(-SpreadIntensity, SpreadIntensity);
        float y = UnityEngine.Random.Range(-SpreadIntensity, SpreadIntensity);

        //Return the shooting position and the spread
        return direction + new Vector3(x, y, 0);
    }


    private IEnumerator DestroyPelletAfterTime(GameObject pellet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(pellet);
    }
}
