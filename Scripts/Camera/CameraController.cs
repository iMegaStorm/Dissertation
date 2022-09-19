using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed;
    public float zoomSpeed;

    public float minZoomDist;
    public float maxZoomDist;

    public float zOffset;
    public float fixedZoomDistance;

    private Camera myCam;

    public static CameraController instance;
    [SerializeField] GameManager gameManager;

    private void Awake()
    {
        myCam = Camera.main;
        instance = this;
    }

    private void Update()
    {
        Move();
        Zoom();

        if(gameManager.isFollowingAStarUnit == true || gameManager.isFollowingDijkstraUnit == true)
            UnitFollower();
    }

    private void Move()
    {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        Vector3 dir = transform.forward * zInput + transform.right * xInput;
        transform.position += dir * moveSpeed * Time.deltaTime;
    }

    void Zoom()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        float dist = Vector3.Distance(transform.position, myCam.transform.position);
        
        if (dist < minZoomDist && scrollInput > 0.0f)
            return;
        else if (dist > maxZoomDist && scrollInput < 0.0f)
            return;

        myCam.transform.position += myCam.transform.forward * scrollInput * zoomSpeed;
    }

    public void FocusOnPosition(Vector3 pos)
    {
        transform.position = pos;
    }

    public void UnitFollower()
    {
        if(gameManager.isFollowingAStarUnit)
        {
            GameObject aStarUnit = GameObject.FindWithTag("Team2");
            myCam.transform.position = new Vector3 (aStarUnit.transform.position.x, fixedZoomDistance, aStarUnit.transform.position.z -zOffset);
            myCam.transform.rotation = Quaternion.Euler(75, 180, 0);
        }
        else
        {
            GameObject DijkstraUnit = GameObject.FindWithTag("Team1");
            myCam.transform.position = new Vector3(DijkstraUnit.transform.position.x, fixedZoomDistance, DijkstraUnit.transform.position.z + zOffset);
        }
    }
}
