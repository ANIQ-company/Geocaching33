using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargerTooltip : MonoBehaviour
{
    private void Start()
    {
        // Subscribe to the event preparation of tooltip style.
        OnlineMapsGUITooltipDrawer.OnPrepareTooltipStyle += OnPrepareTooltipStyle;
    }

    private void OnPrepareTooltipStyle(ref GUIStyle style)
    {
        // Change the style settings.
        style.fontSize = Screen.width / 20;
    }
}
