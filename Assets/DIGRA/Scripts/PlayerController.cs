using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera camera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Fire();
        }
    }

    public void Fire()
    {
        Ray r = camera.ViewportPointToRay(new Vector3(0.5f, .5f, 0f));
        
        
        
        if (Physics.Raycast(r,out RaycastHit hit, 100f))
        { 
            Debug.Log(hit.collider.name);
        }
        Debug.DrawRay(r.origin, r.direction * 100, Color.blueViolet, 10f);
    }
}
