using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject  player;//will reference the player game object's position
    private Vector3 offSet;//will set offset position from the player to camera

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //calculate the offset position between the camera and the player at the start of the game.
        //it subtracts the player's position 
        offSet = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + offSet;
    }
}
