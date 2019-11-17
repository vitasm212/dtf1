using UnityEngine;
using UnityEditor;
using DTF;
using UnityEngine.UI;

public class CellView : MonoBehaviour
{
    [SerializeField] GameObject[] _hp;
 //   [SerializeField] Text text;

    public void UpdateInfo(Unit unit)
    {
        Clear();
        if (unit.hp == 0)
            return;
      //  text.text = "";
        var hp = unit.hp;
        if (hp >= _hp.Length)
            hp = _hp.Length;

        _hp[hp-1].SetActive(true);

    }

    public void Clear()
    {
        foreach (var go in _hp)
            go.SetActive(false);
    //    text.text = "";
    }
}