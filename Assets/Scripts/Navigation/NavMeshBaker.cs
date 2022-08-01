//Utilizing the experimental "NavMeshComponents" package, which can be found here: https://github.com/Brackeys/NavMesh-Tutorial/tree/master/NavMesh%20Example%20Project/Assets/NavMeshComponents + Brackey's Tutorial: https://www.youtube.com/watch?v=CHV1ymlw-P8
using UnityEngine;
using UnityEngine.AI;

public class NavMeshBaker : MonoBehaviour
{
    //Private Variables
    private NavMeshSurface _navMeshSurface;

    void Awake()
    {
        GameManager.OnGameStateChanged += GenerateNavMesh;
    }

    void Start()
    {
        _navMeshSurface = GetComponent<NavMeshSurface>();
    }

    private void GenerateNavMesh(GameState state)
    {
        if (state == GameState.PrepareEnemySpawning)
        {
            //Bake the NavMesh at runtime
            _navMeshSurface.BuildNavMesh();
        }
    }

    void OnDisable()
    {
        GameManager.OnGameStateChanged -= GenerateNavMesh;
    }
}
