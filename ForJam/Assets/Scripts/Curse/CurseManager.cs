using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using DG.Tweening;

public class CurseManager : MonoBehaviour
{
    [SerializeField] private CharacterData player;
    
    //[SerializeField] private bool 
    [SerializeField] private float teleportTime;
    public bool isTeleporting;
    private List<TeleportZone> teleportZones;

    [Header("God Mode")] 
    public GameObject shield;

    public float cooldown = 3f;
    [HideInInspector] public float cooldownTimer;

    public bool curse_1;
    public bool curse_2;
    public bool curse_3;
    public bool tutorialComplete;
    public GameObject tutorialWall;

    private void Start()
    {
        teleportZones = new List<TeleportZone>(FindObjectsOfType<TeleportZone>());

        cooldownTimer = cooldown;
        // d
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isTeleporting)
        {
            Vector3 mousePosition = MainCam.instance.cam.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            if (IsMouseOverAnyTeleportZone(mousePosition))
            {
                Teleport(mousePosition);
            }
            else
            {
                Debug.Log("Invalid teleport zone");
                player.isPlaying = true;
                isTeleporting = false;
                HideTeleportZones();
            }
        }

        cooldownTimer += Time.deltaTime;
    }
    
    public void InitiateTeleport()
    {
        if (cooldown <= cooldownTimer)
        {
            Debug.Log("Teleportation initiated");
            isTeleporting = true;
            player.isPlaying = false;
            ShowTeleportZones();
            cooldownTimer = 0;
        }
    }
    
    private bool IsMouseOverAnyTeleportZone(Vector3 mousePosition)
    {
        foreach (var zone in teleportZones)
        {
            if (zone.IsMouseOverZone(mousePosition))
            {
                return true;
            }
        }
        return false;
    }

    public void Teleport(Vector3 targetPosition)
    {
        player.transform.DOMove(targetPosition, teleportTime).OnComplete(() => 
        {
            Debug.Log("Teleportation complete");
            player.isPlaying = true;
            isTeleporting = false;
            HideTeleportZones();
        });
    }

    public void ChangeSize()
    {
        if (cooldown <= cooldownTimer)
        {
            player.stats.sizeMultiplier = 0.65f;
            player.stats.ConfigureMassAndSize();
            Invoke("ChangeSizeToDefault", 3f);
            cooldownTimer = 0;
        }
    }

    public void ActivateShield()
    {
        if (cooldown <= cooldownTimer)
        {
            shield.gameObject.SetActive(true);
            player.health.isGod = true;
            Invoke("DeactivateShield", 3f);
            cooldownTimer = 0;
        }
    }

    public void DeactivateShield()
    {
        shield.gameObject.SetActive(false);
        player.health.isGod = false;
    }

    private void ChangeSizeToDefault()
    {
        player.stats.sizeMultiplier = 0.81f;
        player.stats.ConfigureMassAndSize();
    }
    
    private void ShowTeleportZones()
    {
        foreach (var zone in teleportZones)
        {
            zone.ShowZone();
        }
    }

    private void HideTeleportZones()
    {
        foreach (var zone in teleportZones)
        {
            zone.HideZone();
        }
    }

    public void Tutorial()
    {
        if (curse_1 && curse_2 && curse_3)
        {
            tutorialComplete = true;
            tutorialWall.gameObject.transform.DOMove(new Vector3(-20.52f, -20f, 0f), 2f);
        }
    }
}
