using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Animal : MonoBehaviour
{
    private NavMeshAgent _agent;
    [SerializeField] GameObject _target;
    //Animation
    [SerializeField] GameObject _FrontLegL,_FrontLegR,_RearLegL,_RearLegR;
    private Vector3 legStartPosA = new Vector3(10.0f, 0f, 0f);
    private Vector3 legEndPosA = new Vector3(-10.0f, 0f, 0f);
    private Vector3 legStartPosB = new Vector3(-10.0f, 0f, 0f);
    private Vector3 legEndPosB = new Vector3(10.0f, 0f, 0f);
    float _LegsRotSpeed = 4.0f;
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        
    }

    private void Update()
    {
        Seek(_target.transform.position);
    }
    private void Seek(Vector3 location)
    {
        _agent.stoppingDistance = 2f;
        WalkAnimation();
        _agent.SetDestination(location);
        
    }

    void WalkAnimation()
    {
        
        Quaternion legAngleFromA = Quaternion.Euler(legStartPosA);       
        Quaternion legAngleToA = Quaternion.Euler(legEndPosA);           

        Quaternion legAngleFromB = Quaternion.Euler(legStartPosB);       
        Quaternion legAngleToB = Quaternion.Euler(legEndPosB);            

        float lerp = 0.5f * (1.0f + Mathf.Sin(Mathf.PI * Time.realtimeSinceStartup *_LegsRotSpeed));

        _FrontLegL.transform.localRotation = Quaternion.Lerp(legAngleFromA, legAngleToA, lerp);
        _FrontLegR.transform.localRotation = Quaternion.Lerp(legAngleFromB, legAngleToB, lerp);

        _RearLegL.transform.localRotation = Quaternion.Lerp(legAngleFromB, legAngleToB, lerp);
        _RearLegR.transform.localRotation = Quaternion.Lerp(legAngleFromA, legAngleToA, lerp);
    }

 



}


