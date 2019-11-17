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
      //  text.text = "";
        var hp = unit.hp;
        if (hp >= _hp.Length)
            hp = _hp.Length - 1;

        _hp[hp].SetActive(true);

    }

    public void Clear()
    {
        foreach (var go in _hp)
            go.SetActive(false);
    //    text.text = "";
    }
}