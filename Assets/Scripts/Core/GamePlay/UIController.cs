using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : SingleTon<UIController>
{
    public TMP_Text infortxt;
    public CanvasGroup infoCanGrp;
    public TMP_Text killtxt;
    private Coroutine coroutine;
    public Slider playerHealthSlider;
    void OnEnable()
    {
        GameController.waveInfoEvent += ShowWaveInfo;
    }

    private void ShowWaveInfo(int obj)
    {
        infortxt.text = "Wave " + obj + " is starting..";
        coroutine=StartCoroutine(FadeWaveInfo());
    }
    public void UpdatePlayerHealth(float health, float total)
    {
        playerHealthSlider.value = health / total;
    }
    public void UpdateKillInfo(int kill)
    {
        killtxt.text = "kill->" + kill;
    }
    public void ResetPlayer()
    {
        infortxt.text = "ReSpawn player";
        coroutine=StartCoroutine(FadeWaveInfo());
    }
    private IEnumerator FadeWaveInfo()
    {
        float t = 0;
        while (t <= 1)
        {
            t += Time.deltaTime * (1 / 0.5f);
            infoCanGrp.alpha = t;
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        t = 1;
        while (t >= 0)
        {
            t -= Time.deltaTime * (1 / 0.5f);
            infoCanGrp.alpha = t;
        }
        StopCoroutine(coroutine);
    }

    void OnDisable()
    {
        GameController.waveInfoEvent -= ShowWaveInfo;
    }
}
