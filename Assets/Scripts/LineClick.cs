using System;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(TMP_Text))]
public class LineClick : MonoBehaviour, IPointerClickHandler
{
    public Action<string, int> OnLineClick;
    private TMP_Text m_textMeshPro;
    private Color _startColor;
    private 
    void Start()
    {
        m_textMeshPro = GetComponent<TMP_Text>();
        _startColor = m_textMeshPro.color;
        gameObject.SetActive(false);
    }
 
    public void OnPointerClick(PointerEventData eventData)
    {
        int lineIndex = TMP_TextUtilities.FindIntersectingLine(m_textMeshPro, Input.mousePosition, null);
        if (lineIndex != -1)
        {
            var lineInfo = m_textMeshPro.textInfo.lineInfo[lineIndex];
            var curName = m_textMeshPro.text.Substring(lineInfo.firstCharacterIndex, lineInfo.characterCount);
            curName = Regex.Replace(curName, @"\r\n?|\n|\t", String.Empty);
            OnLineClick?.Invoke(curName, lineIndex);
        }
    }

    public void ColorizeLine(int lineIndex, Color32 color)
    {
        var info = m_textMeshPro.textInfo.lineInfo[lineIndex];

        for (int i = 0; i < info.characterCount; ++i)
        {
            var charInfo = m_textMeshPro.textInfo.characterInfo[info.firstCharacterIndex + i];
            if(!charInfo.isVisible)
                continue;
            
            int meshIndex = charInfo.materialReferenceIndex;
            int vertexIndex = charInfo.vertexIndex;

            Color32[] vertexColors = m_textMeshPro.textInfo.meshInfo[meshIndex].colors32;
            vertexColors[vertexIndex + 0] = color;
            vertexColors[vertexIndex + 1] = color;
            vertexColors[vertexIndex + 2] = color;
            vertexColors[vertexIndex + 3] = color;
        }
 
        m_textMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
    }

    public void Restart()
    {
        foreach (var meshInfo in m_textMeshPro.textInfo.meshInfo)
        {
            for (var i = 0; i < meshInfo.colors32.Length; i++)
            {
                meshInfo.colors32[i] = _startColor;
            }
        }
        m_textMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
    }
}
