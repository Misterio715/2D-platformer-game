using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private const float DISTANCE_SPAWN = 20f;
    private const float DISTANCE_DELETE = 20f;

    [SerializeField] private Transform levelPartStart;
    [SerializeField] private List<Transform> levelPartList;
    [SerializeField] private Transform playerTransform;

    private Vector3 lastEndPosition;
    //private Queue<Transform> levelPartQueue = new Queue<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        lastEndPosition = levelPartStart.Find("EndPosition").position;
        //levelPartQueue.Enqueue(levelPartStart);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("SPAWN: " + (Vector3.Distance(playerTransform.position, lastEndPosition)));
        if (Vector2.Distance(playerTransform.position, lastEndPosition) < DISTANCE_SPAWN)
        {
            SpawnLevelPart();
        }
        //Debug.Log("DELETE: " + Vector3.Distance(playerTransform.position, levelPartQueue.Peek().Find("EndPosition").position));
        //if (levelPartQueue.Peek() != null && playerTransform.position.x - levelPartQueue.Peek().Find("EndPosition").position.x > DISTANCE_DELETE)
        //{
        //    Destroy(levelPartQueue.Dequeue().gameObject);
        //}
    }

    private void SpawnLevelPart()
    {
        Transform randomLevelPart = levelPartList[Random.Range(0, levelPartList.Count)];
        Transform lastLevelPartTransform = SpawnLevelPart(randomLevelPart, lastEndPosition);
        lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
        //levelPartQueue.Enqueue(lastLevelPartTransform);
    }

    private Transform SpawnLevelPart(Transform levelPart ,Vector3 spawnPosition)
    {
        spawnPosition = spawnPosition + new Vector3(Random.Range(0, 5), Random.Range(-5, 5));
        Transform levelPartTransform = Instantiate(levelPart, spawnPosition, Quaternion.identity);
        return levelPartTransform;
    }
}
