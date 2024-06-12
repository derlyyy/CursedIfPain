using System;
using System.Collections;
using System.Collections.Generic;
using Sonity;
using UnityEngine;
using Random = UnityEngine.Random;

public class BugManager : MonoBehaviour
{
    public float upStrengthBugs;
    
    public float bugInterval;
    private float nextBugTime;

    [Header("Bug Upgrade")] 
    public float timeInterval;
    private float elapsedTime;
    private int bugLevel = 0;

    public List<VisualBug> visualBugs;
    public List<SoundEvent> audioBugs;
    
    private void Start()
    {
        
    }

    private void Update()
    {
        if (nextBugTime >= bugInterval)
        {
            TriggerNewBug();
            nextBugTime = 0;
        }

        if (elapsedTime >= bugInterval)
        {
            elapsedTime = 0;
            IncreaseBugLevel();
        }

        nextBugTime += Time.deltaTime;
        elapsedTime += Time.deltaTime;
    }

    public void TriggerNewBug()
    {
        int bugType = Random.Range(0, 2);

        if (bugType == 0 && visualBugs.Count > 0)
        {
            int index = Random.Range(0, visualBugs.Count);
            visualBugs[index].ActivateBug();
        }
        else if (bugType == 1 && audioBugs.Count > 0)
        {
            int index = Random.Range(0, audioBugs.Count);
            SoundManager.Instance.Play(audioBugs[index], transform);
        }
        
        Debug.Log("новый баг активирован");
    }

    public void IncreaseBugLevel()
    {
        bugLevel++;
        ApplyBugEffects();
        Debug.Log("Уровень багов увеличен до: " + bugLevel);
    }

    private void ApplyBugEffects()
    {
        bugInterval = Mathf.Max(0.5f, bugInterval - 0.1f);
        upStrengthBugs += 0.1f;
    }
}
