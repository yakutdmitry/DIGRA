using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera camera;
    public float maxRayDistance;
    public GameObject device;
    public float deviceBattery = 10;
    
    
    private Animator _animator;

    private GameManager gameManager;
    bool deviceActive = true;
    float deviceTimer;
    void Start()
    {
        _animator = device.GetComponent<Animator>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButton(0))
        {
            Fire();
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
            deviceTimer += Time.deltaTime;
        }

        if (deviceTimer >= deviceBattery)
        {
            Debug.Log("End Game");
        }
    }
    
    
    
    public void Fire()
    {
        Ray r = camera.ViewportPointToRay(new Vector3(0.5f, .5f, 0f));
        
        
        
        if (Physics.Raycast(r,out RaycastHit hit, maxRayDistance) && hit.collider.tag == "Ghost")
        { 
            Debug.Log(hit.collider.name);
            gameManager.DestroyGhost(hit.collider.gameObject);
        }
        Debug.DrawRay(r.origin, r.direction * 100, Color.blueViolet, 10f);
    }
}
