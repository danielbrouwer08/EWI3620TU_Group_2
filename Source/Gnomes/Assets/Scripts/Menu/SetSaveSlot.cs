using UnityEngine;
using System.Collections;

public class SetSaveSlot : MonoBehaviour
{

    public void SetSave(int slot)
    {
        PlayerPrefs.SetInt("saveslot", slot);
    }
}
