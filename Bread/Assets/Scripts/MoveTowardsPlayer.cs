using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsPlayer : MonoBehaviour
{
    public Component player;

    private AStarPathfinding aStarPathfinding;
    
    // Start is called before the first frame update
    void Start()
    {
        aStarPathfinding = GetComponent<AStarPathfinding>();
    }

    // Update is called once per frame
    void Update()
    {
        aStarPathfinding.FindPath(transform.position, player.transform.position); 
    }
}
