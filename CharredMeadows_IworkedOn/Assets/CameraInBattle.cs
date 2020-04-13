using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInBattle : MonoBehaviour
{

    Skill skill;

    [SerializeField] private GameObject enemyController;
    [SerializeField] private GameObject playerController;
    private Vector3 velocity = Vector3.zero;
    public float smoothSpeed = 0.125f;

  

    public void LookAtPlayer()
    {
        Camera.main.transform.LookAt(Vector3.SmoothDamp(playerController.transform.position, enemyController.transform.position, ref velocity, 1f));
        Invoke("CameraToOrgPos", 1f);
    }

    public void LookAtEnemy()
    {

        transform.position = velocity;
        Camera.main.transform.LookAt(Vector3.SmoothDamp(enemyController.transform.position, playerController.transform.position, ref velocity, +1f));
        Camera.main.depth += 10;
        Invoke("CameraToOrgPos", 1f);
    }

    public void CameraToOrgPos()
    {
        Camera.main.transform.rotation = Quaternion.Euler(32f, 46f, 0f);
        Camera.main.depth -= 10;
    }

}
