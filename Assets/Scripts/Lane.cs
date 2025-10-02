using UnityEngine;
using System.Collections.Generic;

public class Lane : MonoBehaviour
{
    [SerializeField]
    private List<Transform> frames;
    public List<Transform> Frames => frames;
}
