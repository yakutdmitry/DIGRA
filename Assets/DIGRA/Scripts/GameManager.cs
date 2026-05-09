using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int maxNumberOfGhosts;
    public float spawnCooldown;
    public GameObject ghostPrefab;
    
    
    [SerializeField] private List<Transform> Anchors;
    
    private float timer = 0;
    private int spawned= 0;
    private bool canSpawn = true;
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
                SpawnGhost(spawned);
            }
        }
    }

    private List<Transform> FindSpawnPositions(int GhostsQuantity)
    {
        return GameObject.FindGameObjectsWithTag("Anchor").OrderBy(x => Random.value).Take(GhostsQuantity).Select(x => x.transform).ToList();
    }

    private void SpawnGhost(int spawnedGhostsQuantity)
    {
        canSpawn = false;
        if (spawnedGhostsQuantity < Anchors.Count)
        {
            int index = Random.Range(0, Anchors.Count);
            spawnedGhostsQuantity++;
            spawned = spawnedGhostsQuantity;

            Instantiate(ghostPrefab, (Anchors[index]).localPosition, (Anchors[index]).rotation);
            SpawnGhostWait(15);

            timer = 0;
        }
        else
        {
            Debug.Log("End Game!");
            Application.Quit();
        }
    }

    public void DestroyGhost(GameObject ghost)
    {
        Anchors.Remove(ghost.transform);
        Destroy(ghost);
        canSpawn = true;
    }

    IEnumerator SpawnGhostWait(float time)
    {
        yield return new WaitForSeconds(time);
        Instantiate(ghostPrefab, Anchors[Random.Range(0, Anchors.Count)]); 
        timer = 0f;
    }
}
