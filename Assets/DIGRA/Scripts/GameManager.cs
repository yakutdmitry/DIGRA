using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int maxNumberOfGhosts;
    public float spawnCooldown;
    public GameObject ghostPrefab;
    public GameObject propsPrefab;
    public ParticleSystem particles;
    
    
    [SerializeField] private List<Transform> Anchors;
    [SerializeField] private PlayerController player;
    
    private float timer = 0;
    private int spawned= 0;
    private bool canSpawn = true;
    private bool canSpawnProps = true;
    private int index;
    private int ghostsQuantity;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        ghostsQuantity = Random.Range(3, maxNumberOfGhosts);
        player.deviceBattery = 30f * ghostsQuantity;
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
        canSpawnProps = false;
            Instantiate(propsPrefab, (Anchors[newIndex]).position, (Anchors[newIndex]).rotation);
            Anchors[index].GetComponent<AudioSource>().Play();
            timer = 0;
            Debug.Log(Anchors[newIndex].name);
            
    }

    public void DestroyGhost(GameObject ghost)
    {
        Anchors.Remove(Anchors[index]);
        Instantiate(particles, ghost.transform.position, ghost.transform.rotation);
        Destroy(ghost);
        canSpawn = true;
        canSpawnProps = true;
        timer = 0f;

        if (spawned == ghostsQuantity)
        {
            SceneManager.LoadScene(sceneBuildIndex: 2);
        }
    }


    private void SpawnGhost()
    {
        if (Anchors.Count != 0)
        {
            index = Random.Range(0, Anchors.Count);
            // Debug.Log(Anchors[index].name);
        
            Instantiate(ghostPrefab, Anchors[index].position, Anchors[index].rotation);
            spawned++;
            canSpawn = false;
        }
        else
        {
            SceneManager.LoadScene(sceneBuildIndex: 2);
        }

    }
}
