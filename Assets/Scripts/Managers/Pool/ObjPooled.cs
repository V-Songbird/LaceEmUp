using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JamOff.Scripts.Managers;

public class ObjPooled : MonoBehaviour
{


    public virtual void TerminateImmediatelyAndSilently()
    {
        transform.parent = GamePlayManager.Instance.PoolSystem.ObjectPoolParent;
        gameObject.SetActive(false);
    }
}
