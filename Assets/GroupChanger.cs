using System.Collections.Generic;
using UnityEngine;

public class GroupChanger : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectGroup;
    [SerializeField] private int enableGroup;

    private void Update()
    {
        for (int i = 0; i < objectGroup.Count; i++) objectGroup[i].SetActive(i == enableGroup);
    }

    public void ChangeAt(int index) => enableGroup = index % objectGroup.Count;
    public void ChangeNext() => ChangeAt(++enableGroup);
}