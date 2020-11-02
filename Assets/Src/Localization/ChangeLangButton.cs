using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLangButton : MonoBehaviour
{
    public SystemLanguage language;
    public void ChangeLangBtn() {
        LocalizationService.Instance.ChangeLang(language);
    }
}
