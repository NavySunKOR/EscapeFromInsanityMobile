using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

    public Weapon weaponInfo;
    public bool fireReady;
    public bool isHaving;
    private const float MAX_MUZZLE_INTERVAL = 0.05f;
    private Transform firePos;
    private float currentRecoilRadius;
    private float fireInterval;
    private float fireTime = 0f;
    private int ammosToReload;

    // Use this for initialization
    private void Start () {
        fireReady = true;
        fireInterval = 60f / weaponInfo.rpm;
        firePos = GameObject.FindGameObjectWithTag("MainCamera").transform;
        ammosToReload = 0;
    }
    private void FireWeapon()
    {
        fireTime = Time.time;
        // Check WeaponType
        // FIre Different weapontype and play animation;
        switch(weaponInfo.name)
        {
            case ItemName.Handgun: FireSingleShot(); break;
            case ItemName.AssaultRifle: FireSingleShot(); break;
            case ItemName.Shotgun: FireShotgun(); break;
            default: Debug.LogError("There's no weapon!"); break;
        }
    }

    private void FireSingleShot()
    {
        StartCoroutine(OpenFireEffect());

        Vector3 randomVector = Random.insideUnitCircle * currentRecoilRadius;
        Ray ray = new Ray(firePos.position + firePos.forward * 1f + randomVector, firePos.forward);
        RaycastHit hit;
        if(Physics.Raycast(ray,out  hit, weaponInfo.range))
        {
            AIController aIController = hit.collider.GetComponentInParent<AIController>();
            if(aIController != null)
            {
                aIController.TookHit(hit, weaponInfo.damage);
            }
            else
            {
                //TODO: Make a bullet hole
            }
        }
        
        DecreaseMagazine();
        IncreaseRecoilRadius();
    }

    private void FireShotgun()
    {
        StartCoroutine(OpenFireEffect());

        for (int i = 0; i < weaponInfo.GetShotgunPalletCount(); i++)
        {
            Vector3 randomVector = Random.insideUnitCircle * weaponInfo.GetShotgunSpread();
            Ray ray = new Ray(firePos.position + firePos.forward * 1f + randomVector, firePos.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, weaponInfo.range))
            {
                AIController aIController = hit.collider.GetComponentInParent<AIController>();
                if (aIController != null)
                {
                    aIController.TookHit(hit, weaponInfo.damage);
                }
                else
                {
                    //TODO: Make a bullet hole
                }
            }
        }

        DecreaseMagazine();
        IncreaseRecoilRadius();
    }

    IEnumerator OpenFireEffect()
    {
        weaponInfo.muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(MAX_MUZZLE_INTERVAL);
        weaponInfo.muzzleFlash.SetActive(false);
    }

    private void DecreaseMagazine()
    {
        weaponInfo.currentMagazine--;
    }

    private void IncreaseRecoilRadius()
    {
        currentRecoilRadius += weaponInfo.recoilIncreament;

        if (currentRecoilRadius > weaponInfo.maxRecoilRadius)
        {
            currentRecoilRadius = weaponInfo.maxRecoilRadius;
        }
    }

    private void DecreaseRecoilRadius()
    {
        currentRecoilRadius -= weaponInfo.recoilIncreament * Time.deltaTime;
        if (currentRecoilRadius < 0f)
        {
            currentRecoilRadius = 0f;
        }
    }

   private void ReloadWeapon()
   {
       //TODO : Implement Reload login after test.
   }
    
   private void ReloadAmmo()
   {
        weaponInfo.currentMagazine += ammosToReload;
        ammosToReload = 0;
   }
    public void WieldWeapon()
    {
        gameObject.SetActive(true);
    }

    public void UnWieldWeapon()
    {
        gameObject.SetActive(false);
    }
}
