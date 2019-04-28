using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsPlayer : MonoBehaviour
{
    public Component player;
    public float speed;
    
    private Animator animator;
    private AStarPathfinding aStarPathfinding;
    
    // Start is called before the first frame update
    void Start()
    {
        aStarPathfinding = GetComponent<AStarPathfinding>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        var findPath = aStarPathfinding.FindPath(transform.position, player.transform.position);

        // Nowhere to path-find.
        if (findPath.Count == 0) return;
        
        // Find the next cell to move into.
        var worldPoint = aStarPathfinding.WorldPointFromNode(findPath[0]);
        var target = worldPoint - transform.position;

        // If the next cell is too close and there are more cells, move towards the second-closest cell.
        if (target.magnitude < 1 && findPath.Count > 1)
        {
            worldPoint = aStarPathfinding.WorldPointFromNode(findPath[1]);
            target = worldPoint - transform.position;
        }
            
        // Rotate towards next cell.
        transform.up = Vector3.Lerp(transform.up, target, 0.1f);
        
        // Move towards next cell.
        var movement = Vector3.ClampMagnitude(target, 1);
        
        var moving = (target.magnitude > 1);
        animator.SetBool("moving", moving);

        transform.position += movement * speed * Time.deltaTime;
    }
}
