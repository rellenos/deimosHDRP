using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialog : MonoBehaviour
{
    public GameObject panel;

    public string[] dialogAlythea;
    public string[] dialogIR;

    public TextMeshProUGUI txtDialog;
    public bool isDialogActive;

    Coroutine auxCoroutine;

    public void OpenBox(int valor)
    {
        if (isDialogActive)
        {
            CloseDialog();
            StartCoroutine(waitDialog(valor));
        }
        else
        {
            isDialogActive = false;
            auxCoroutine = StartCoroutine(showDialog(valor));
        }
    }

    IEnumerator showDialog(int valor, float time = 0.1f)
    {
        panel.SetActive(true);
        string[] dialog;
        if (valor == 0) dialog = dialogAlythea;
        else dialog = dialogIR;

        int total = dialog.Length;
        string res = "";
        isDialogActive = true;
        yield return null;
        for (int i = 0; i < total; i++)
        {
            res = "";
            if (isDialogActive)
            {
                for (int s = 0; s < dialog[i].Length; s++)
                {
                    if (isDialogActive)
                    {
                        res = string.Concat(res, dialog[i][s]);
                        txtDialog.text = res;
                        yield return new WaitForSeconds(time);
                    }
                    else yield break;
                }
                yield return new WaitForSeconds(0.4f);
            }
            else yield break;
        }
        yield return new WaitForSeconds(0.4f);
        CloseDialog();
    }

    IEnumerator waitDialog(int valor)
    {
        yield return new WaitForEndOfFrame();
        auxCoroutine = StartCoroutine(showDialog(valor));
    }

    public void CloseDialog()
    {
        isDialogActive = false;
        if (auxCoroutine != null)
        {
            StopCoroutine(auxCoroutine);
            auxCoroutine = null;
        }

        txtDialog.text = "";
        panel.SetActive(false);
    }

    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            OpenBox(0);
        }
    }*/

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OpenBox(0);
        }
    }
}
