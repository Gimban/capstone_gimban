using UnityEngine;
using System.Collections.Generic;

public class LightSwitch : Interactable
{
    private Animator animator;

    public GameObject lights;               // ���� �׷�
    private Light[] lightCP;                // Light ������Ʈ��
    private Renderer[] renderers;           // MeshRenderer��

    private Dictionary<Renderer, Color> originalEmissionColors = new();

    private void Start()
    {
        animator = GetComponent<Animator>();

        if (lights != null)
        {
            lightCP = lights.GetComponentsInChildren<Light>(true);
            renderers = lights.GetComponentsInChildren<Renderer>(true);

            // Emission ���� ����
            foreach (Renderer rend in renderers)
            {
                if (rend.material.HasProperty("_EmissionColor"))
                {
                    Color color = rend.material.GetColor("_EmissionColor");
                    originalEmissionColors[rend] = color;
                }
            }
        }
    }

    public override void OnLookAt()
    {
        
    }

    public override void OnLookAway()
    {
       
    }

    public override void OnInteract()
    {
        bool toggle = !animator.GetBool("light_toggle");
        animator.SetBool("light_toggle", toggle);

        if (lights != null)
        {
            // Light ������Ʈ ����/�ѱ�
            foreach (Light light in lightCP)
            {
                light.enabled = toggle;
            }

            // Emission ����
            foreach (Renderer rend in renderers)
            {
                if (rend.material.HasProperty("_EmissionColor"))
                {
                    if (toggle && originalEmissionColors.TryGetValue(rend, out Color origColor))
                    {
                        rend.material.SetColor("_EmissionColor", origColor);
                        DynamicGI.SetEmissive(rend, origColor);
                    }
                    else
                    {
                        rend.material.SetColor("_EmissionColor", Color.black);
                        DynamicGI.SetEmissive(rend, Color.black);
                    }
                }
            }

            // Debug.Log("Lights toggled: " + toggle);
        }
    }
}
