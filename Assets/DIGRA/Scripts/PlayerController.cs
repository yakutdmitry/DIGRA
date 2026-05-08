using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera camera;
    public float maxRayDistance;

    private GameManager gameManager;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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
        
        
        
        if (Physics.Raycast(r,out RaycastHit hit, maxRayDistance) && hit.collider.tag == "Ghost")
        { 
            Debug.Log(hit.collider.name);
            gameManager.DestroyGhost(hit.collider.gameObject);
        }
        Debug.DrawRay(r.origin, r.direction * 100, Color.blueViolet, 10f);
    }
}
