using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Camera camera;
    public AudioSource shot;
    public GameObject device;
    public TextMeshProUGUI Battery;
    
    public float deviceBattery = 10;
    public float maxRayDistance;
    
    
    private Animator _animator;
    private GameManager gameManager;
    bool deviceActive = true;
    float deviceTimer;
    private bool canShoot;
    private float shootDelay = 2.5f;
    private float shootTimer = 0;
    void Awake()
    {
        _animator = device.GetComponent<Animator>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
                
        if (Input.GetMouseButton(0) && canShoot)
        {
            Fire();
        }

        if (!canShoot)
        {
            shootTimer += Time.deltaTime;
            if (shootTimer >= shootDelay)
            {
                canShoot = true;
                shootTimer = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (deviceActive == false)
            {
                _animator.SetTrigger("Up");
                deviceActive = true;
            }
            else
            {
                _animator.SetTrigger("Back");
                deviceActive = false;
            }
        }
        
        if (deviceActive == true)
        {
            var battery = (1 - (deviceTimer / deviceBattery)) * 100;
            deviceTimer += Time.deltaTime;
            Battery.text = "Battery: " + MathF.Floor(battery) + "%";
            if (battery <= 20)
            {
                Battery.color = Color.red;
            }
        }

        if (deviceTimer >= deviceBattery)
        {
            SceneManager.LoadScene(sceneBuildIndex: 3);
        }
    }
    
    
    
    public void Fire()
    {
        if (deviceActive)
        {
            canShoot = false;
            deviceTimer +=  (deviceBattery / 100) * 5f;
            shot.Play();
            Ray r = camera.ViewportPointToRay(new Vector3(0.5f, .5f, 0f));

            int mask = 1 << 3;
        
            if (Physics.Raycast(r,out RaycastHit hit, maxRayDistance, mask) && hit.collider.tag == "Ghost")
            { 
                Debug.Log(hit.collider.name);
                gameManager.DestroyGhost(hit.collider.gameObject);
            }

            if (Physics.Raycast(r, out RaycastHit hitInfo, maxRayDistance))
            {
                Debug.Log(hitInfo.collider.name);
            }
            Debug.DrawRay(r.origin, r.direction * 100, Color.blueViolet, 2);
        }
        
    }
}
