using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
    [SerializeField] List<string> dialogInOrder;

    [SerializeField] TextMeshProUGUI textGameObject;

    int dialogIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        textGameObject.text = dialogInOrder[0];
        dialogIndex++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextDialog()
    {
        if(dialogIndex < dialogInOrder.Count)
        {
            textGameObject.text = dialogInOrder[dialogIndex];
            dialogIndex++;
        } else
        {
            return;
        }
    }
    public string getDialog(int dialogIndex)
    {
        return dialogInOrder[dialogIndex].ToString();
    }

    public void setDialog(int Index)
    {
        if (dialogIndex < dialogInOrder.Count)
        {
            textGameObject.text = dialogInOrder[Index];
            dialogIndex = Index;
        }
    }
    public void setText(string text)
    {
        textGameObject.text = text;
    }
}
