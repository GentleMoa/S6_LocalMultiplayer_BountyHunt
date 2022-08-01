using UnityEngine;

public class WorldSpaceUIOrientator : MonoBehaviour
{
    //Private Variables
    private Canvas _canvas;
    private Camera _arCamera;

    // Start is called before the first frame update
    void Start()
    {
        //referencing the canvas component
        _canvas = GetComponent<Canvas>();
        //referencing the arCamera
        _arCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        //setting the canvas' render mode to world space
        _canvas.renderMode = RenderMode.WorldSpace;
        //assigning the correct event camera
        _canvas.worldCamera = _arCamera;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(_arCamera.transform);
    }
}
