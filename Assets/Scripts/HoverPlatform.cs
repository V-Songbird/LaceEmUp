using DG.Tweening;
using UnityEngine;

public class HoverPlatform : MonoBehaviour
{

    void Start()
    {
        transform.DOMoveY(10, 8).SetLoops(-1, LoopType.Yoyo);
    }

}
