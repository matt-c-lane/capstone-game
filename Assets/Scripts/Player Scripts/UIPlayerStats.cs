using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIPlayerStats : MonoBehaviour
{
    public TMP_Text body;
    public TMP_Text mind;
    public TMP_Text luck;

    public void UpdateBody(int value) { body.text = value.ToString(); }
    public void UpdateMind(int value) { mind.text = value.ToString(); }
    public void UpdateLuck(int value) { luck.text = value.ToString(); }
    public void UpdateStats(int b, int m, int l) { UpdateBody(b); UpdateMind(m); UpdateLuck(l); }
}