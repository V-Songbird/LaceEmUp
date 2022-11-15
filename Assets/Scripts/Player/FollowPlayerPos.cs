using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JamOff.Scripts.Managers;

public class FollowPlayerPos : MonoBehaviour
{

    void Update()
    {
        transform.position = GamePlayManager.Instance.Player.transform.position;
    }
}
