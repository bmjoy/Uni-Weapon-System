using UnityEngine;

public class DrawFrameRate : MonoBehaviour
{
    [SerializeField,Range(5, 100)] private int samplingFrequency = 25;
    private float _fps;
    private int _count;

    private void Update()
    {
        _count++;
        if (_count % samplingFrequency == 0)
        {
            _fps = 1f / Time.deltaTime;
        }
    }

    private void OnGUI() => GUILayout.Label($"{Mathf.FloorToInt(_fps).ToString()} fps");
}