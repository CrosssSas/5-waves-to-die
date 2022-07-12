using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[AddComponentMenu("Nokobot/Modern Guns/Simple Shoot")]
public class SimpleShoot : MonoBehaviour
{
    [Header("Prefab Refrences")]
    public GameObject bulletPrefab;
    public GameObject casingPrefab;
    public GameObject muzzleFlashPrefab;

    [Header("Location Refrences")]
    [SerializeField] private Animator gunAnimator;
    [SerializeField] private Transform barrelLocation;
    [SerializeField] private Transform casingExitLocation;

    [Header("Settings")]
    [Tooltip("Specify time to destory the casing object")] [SerializeField] private float destroyTimer = 2f;
    [Tooltip("Bullet Speed")] [SerializeField] private float shotPower = 500f;
    [Tooltip("Casing Ejection Speed")] [SerializeField] private float ejectPower = 150f;

    public AudioSource source;
    public AudioClip fireSound;
    public AudioClip reload;
    public AudioClip noAmmo;

    public int damageAmount = 20;
    public Magazine magazine;
    public XRBaseInteractor socketInteractor;
    

    public void AddMagazine(XRBaseInteractable interactable)
    {
        magazine = interactable.GetComponent<Magazine>();
        source.PlayOneShot(reload);

    }

    public void RemoveMagazine(XRBaseInteractable interactable)
    {
        magazine = null;
        source.PlayOneShot(reload);
    }
    
    public void Slide()
    {
        
    }

    void Start()
    {
        if (barrelLocation == null)
            barrelLocation = transform;

        if (gunAnimator == null)
            gunAnimator = GetComponentInChildren<Animator>();

        socketInteractor.onSelectEntered.AddListener(AddMagazine);
        socketInteractor.onSelectExited.AddListener(RemoveMagazine);
    }

    public void PullTheTrigger()
    {
        if(magazine && magazine.numberOfBullet > 0)
        {
            gunAnimator.SetTrigger("Fire");
        }
        else 
        {
            source.PlayOneShot(noAmmo);
        }
    }


    // Эта функция создает поведение пули
    void Shoot()
    {
        magazine.numberOfBullet--;
        source.PlayOneShot(fireSound);
        if (muzzleFlashPrefab)
        {
            // Дульное пламя
            GameObject tempFlash;
            tempFlash = Instantiate(muzzleFlashPrefab, barrelLocation.position, barrelLocation.rotation);

            // Его уничтожение
            Destroy(tempFlash, destroyTimer);
        }

        // Отменяется, если нет префаба пули
        if (!bulletPrefab)
        { return; }

        // Создание пули и придача силы в направлении ствола
        Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation).GetComponent<Rigidbody>().AddForce(barrelLocation.forward * shotPower);

        

    }

    //Эта функция создает гильзу в прорези выброса
    void CasingRelease()
    {
        //Отменяет функцию, если прорезь для выброса не установлена или отсутствует гильза
        if (!casingExitLocation || !casingPrefab)
        { return; }

        //Создание гильзы
        GameObject tempCasing;
        tempCasing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation) as GameObject;
        //Добавляем силу на корпус, чтобы вытолкнуть
        tempCasing.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(ejectPower * 0.7f, ejectPower), (casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f), 1f);
        //Добавим корпус, чтобы она крутилась в произвольном направлении
        tempCasing.GetComponent<Rigidbody>().AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(100f, 1000f)), ForceMode.Impulse);

        //Уничтожение гильзы через время
        Destroy(tempCasing, destroyTimer);
    }

}