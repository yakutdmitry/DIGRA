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
                SpawnGhost(spawned, index);
            }
            if (timer >= (spawnCooldown / 2) && canSpawnProps)
            {
                Debug.Log("HINTING");
                hint();
            }
        }
        
        
    }

    private List<Transform> FindSpawnPositions(int GhostsQuantity)
    {
        return GameObject.FindGameObjectsWithTag("Anchor").OrderBy(x => Random.value).Take(GhostsQuantity).Select(x => x.transform).ToList();
    }

    private void SpawnGhost(int spawnedGhostsQuantity, int newIndex)
    {
        canSpawn = false;
        if (spawnedGhostsQuantity < Anchors.Count)
        {
            
            spawnedGhostsQuantity++;
            spawned = spawnedGhostsQuantity;

            Instantiate(ghostPrefab, (Anchors[newIndex]).position, (Anchors[newIndex]).rotation);

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


    private void hint()
    {
        index = Random.Range(0, Anchors.Count);
        Debug.Log(Anchors[index].name);
        Anchors[index].GetComponent<AudioSource>().Play();
        Instantiate(propsPrefab, Anchors[index].position, Anchors[index].rotation);
        canSpawnProps = false;
    }
}
