using UnityEngine;

public class AutoLight : MonoBehaviour
{
    public GameObject map_root;
    Light light_setting;
    float intensity;
    float range;
    float[] root_scale;
    void Start()
    {
        light_setting = GetComponent<Light>();
        intensity = light_setting.intensity;
        range = light_setting.range;

        if (map_root != null)
        {
            for (int i = 0; i < 3; i++)
            {
                root_scale[i] = map_root.transform.localScale[i];
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
