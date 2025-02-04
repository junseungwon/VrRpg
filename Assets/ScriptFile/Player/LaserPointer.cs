using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
public class LaserPointer : MonoBehaviour
{
    public int stage = 2;
    public GameObject bossMonster;
    public GameObject player;
    public PlayerCharacterMove playerCharacter;
    public static LaserPointer instance;
    public SteamVR_Input_Sources handType;
    public SteamVR_Behaviour_Pose controllerPose;
    public SteamVR_Action_Boolean teleportAction;
    public GameObject laserPrefab;
    private GameObject laser;
    private Transform laserTransfrom;
    private Vector3 hitPoint;

    public Transform cameraRigTransform;
    public GameObject teleportReticlePrefab;
    private GameObject reticle;
    private Transform teleportReticleTransform;
    public Transform headTransform;
    public Vector3 teleportReticleOffset;
    public LayerMask teleportMask;
    private bool shouldTeleport;
    public Material ma;
    // Start is called before the first frame update
    void Start()
    {
        laser = Instantiate(laserPrefab);
        laser.transform.parent = player.transform;
        laserTransfrom = laser.transform;
        reticle = Instantiate(teleportReticlePrefab);
        reticle.transform.parent = player.transform;
        teleportReticleTransform = reticle.transform;
    }
    // Update is called once per frame
    void Update()
    {
        if (teleportAction.GetState(handType))
        {
            RaycastHit hit;
            if (Physics.Raycast(controllerPose.transform.position, transform.forward,
                out hit, 100, teleportMask))
            {

                hitPoint = hit.point;
                ShowLaser(hit);

                reticle.SetActive(true);
                teleportReticleTransform.position = hitPoint + teleportReticleOffset;
                shouldTeleport = true;
            }
        }
        else
        {

            laser.SetActive(false);
            laser.SetActive(false);
        }

        if (teleportAction.GetStateUp(handType) && shouldTeleport)
        {

            Teleport();
        }
    }

    private void Teleport()
    {
        shouldTeleport = false;
        reticle.SetActive(false);
        Vector3 difference = cameraRigTransform.position - headTransform.position;
        difference.y = 0;
        cameraRigTransform.position = hitPoint + difference;
        if (stage == 2)
        {
            player.transform.LookAt(bossMonster.transform);
        }
    }

    private void ShowLaser(RaycastHit hit)
    {
        laser.SetActive(true);
        laser.transform.position = Vector3.Lerp(controllerPose.transform.position, hitPoint, 0.5f);
        laserTransfrom.LookAt(hitPoint);
        laserTransfrom.localScale = new Vector3(laserTransfrom.localScale.x, laserTransfrom.localScale.y, hit.distance);
    }
}
