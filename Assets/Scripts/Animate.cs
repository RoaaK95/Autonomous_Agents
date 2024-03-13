using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animate : MonoBehaviour
{
    public bool _isWalking = true;
    [SerializeField] GameObject _FrontLegL, _FrontLegR, _RearLegL, _RearLegR;
    private Vector3 legStartPosA = new Vector3(10.0f, 0f, 0f);
    private Vector3 legEndPosA = new Vector3(-10.0f, 0f, 0f);
    private Vector3 legStartPosB = new Vector3(-10.0f, 0f, 0f);
    private Vector3 legEndPosB = new Vector3(10.0f, 0f, 0f);
    float _legsRotSpeed = 4.0f;

    public void WalkAnimation()
    {
        if (_isWalking)
        {
            Quaternion legAngleFromA = Quaternion.Euler(legStartPosA);
            Quaternion legAngleToA = Quaternion.Euler(legEndPosA);
            Quaternion legAngleFromB = Quaternion.Euler(legStartPosB);
            Quaternion legAngleToB = Quaternion.Euler(legEndPosB);
            float lerp = 0.5f * (1.0f + Mathf.Sin(Mathf.PI * Time.realtimeSinceStartup * _legsRotSpeed));
            _FrontLegL.transform.localRotation = Quaternion.Lerp(legAngleFromA, legAngleToA, lerp);
            _FrontLegR.transform.localRotation = Quaternion.Lerp(legAngleFromB, legAngleToB, lerp);
            _RearLegL.transform.localRotation = Quaternion.Lerp(legAngleFromB, legAngleToB, lerp);
            _RearLegR.transform.localRotation = Quaternion.Lerp(legAngleFromA, legAngleToA, lerp);
        }
        else if (!_isWalking)
        {
           
        }

    }

}
