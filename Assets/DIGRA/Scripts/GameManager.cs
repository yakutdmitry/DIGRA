using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int maxNumberOfGhosts;
    public float spawnCooldown;
    public GameObject ghostPrefab;
    public GameObject propsPrefab;
    
    
    [SerializeField] private List<Transform> Anchors;
    
    private float timer = 0;
    private int spawned= 0;
    private bool canSpawn = true;
    private bool canSpawnProps = true;
    private int index;

    void Start()
    {
        Anchors = FindSpawnPositions(Random.Range(3, maxNumberOfGhosts));
        // SpawnGhost(spawned);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (canSpawn)
        {
            timer += Time.deltaTime;
            if (timer >= spawnCooldown)
            {
                SpawnHint(spawned, index);
            }
            if (timer >= (spawnCooldown / 1.5) && canSpawnProps)
            {
                Debug.Log("HINTING");
                SpawnGhost();
            }
        }
        
        
    }

    private List<Transform> FindSpawnPositions(int GhostsQuantity)
    {
        return GameObject.FindGameObjectsWithTag("Anchor").OrderBy(x => Random.value).Take(GhostsQuantity).Select(x => x.transform).ToList();
    }

    private void SpawnHint(int spawnedGhostsQuantity, int newIndex)
    {
        canSpawn = false;
        if (spawnedGhostsQuantity < Anchors.Count)
        {
            
            
            
            Instantiate(propsPrefab, (Anchors[newIndex]).position, (Anchors[newIndex]).rotation);
            Anchors[index].GetComponent<AudioSource>().Play();
            timer = 0;
            Debug.Log(Anchors[newIndex].name);
        }
        else
        {
            Debug.Log("End Game!");
            Application.Quit();
        }
    }

    public void DestroyGhost(GameObject ghost)
    {
        Anchors.Remove(Anchors[index]);
        Destroy(ghost);
        canSpawn = true;
        canSpawnProps = true;
    }


    private void SpawnGhost()
    {
        
        
        index = Random.Range(0, Anchors.Count);
        Debug.Log(Anchors[index].name);
        
        Instantiate(ghostPrefab, Anchors[index].position, Anchors[index].rotation);
        canSpawnProps = false;
    }
}
