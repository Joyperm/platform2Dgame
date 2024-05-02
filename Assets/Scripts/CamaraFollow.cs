using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CamaraFollow : MonoBehaviour
{
    // Start is called before the first frame update

    //current position of player
    public Transform target;

    //camara smoothe when display
    public float smooth;

    //current point of player (camara view to follow)
    private Vector3 targetPosition;



    // Update is called once per frame
    void Update()
    {
        // x axis = player position
        // y & z  = main camara position

        targetPosition = new Vector3(
            target.position.x,
            transform.position.y,
            transform.position.z);

        //change main camara position
        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            smooth * Time.deltaTime);

    }
}

    
