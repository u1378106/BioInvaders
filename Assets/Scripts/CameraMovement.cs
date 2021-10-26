using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	public float cameraSpeed = 0.04f;

    SpawnManager spawnManager;

    private void Start()
    {
        spawnManager = GameObject.FindObjectOfType<SpawnManager>();

        InvokeRepeating("IncreaseSpeed", 15, 10);
    }

    private void IncreaseSpeed()
    {
        cameraSpeed += 0.02f;

        while(spawnManager.respawnTimeblackVirus == 0.25f)
        spawnManager.respawnTimeblackVirus -= 0.2f;

        while (spawnManager.respawnTimeRedVirus == 1f)
            spawnManager.respawnTimeRedVirus -= 0.5f;

        while (spawnManager.respawnTimeHorizontal == 3f)
            spawnManager.respawnTimeHorizontal -= -1f;

    }

    private void FixedUpdate () {
		Camera.main.transform.position = new Vector3 (Camera.main.transform.position.x, Camera.main.transform.position.y + cameraSpeed, Camera.main.transform.position.z);
	}

}