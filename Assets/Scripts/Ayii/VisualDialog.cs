using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using Unity.VisualScripting;

public class VisualDialog : MonoBehaviour
{
    public enum Character { Player, NPC };
    public enum EkspresiNPC { Ekspresi1, Ekspresi2, Ekspresi3, Ekspresi4, Ekspresi5};
    public enum EkspresiPlayer { Ekspresi1, Ekspresi2, Ekspresi3, Ekspresi4, Ekspresi5 };

    [System.Serializable]
    public class DialogData
    {
        public Character character;

        public EkspresiPlayer player;
        public EkspresiNPC npc;
        [TextArea(5, 6)]
        public string dialog;
        public List<string> boldSentences;
        public bool sebutPlayer = false;
    }

    [Header ("Nama Karakter")]
    public string PlayerName;
    public string NPCName;
    public NamaPlayer script;

    [Header ("Active Dialog")]
    [TextArea(5, 3)]
    public string activeDialog;
    public bool typewriting;
    public float delay;

    [Header ("Visual Setting")]
    public GameObject ParentObject;
    public Image dialogPanel;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogText;

    [Header("Ekspresi Player")]
    public GameObject PlayerEkspresi1;
    public GameObject PlayerEkspresi2;
    public GameObject PlayerEkspresi3;
    public GameObject PlayerEkspresi4;
    public GameObject PlayerEkspresi5;
    
    [Header("Ekspresi NPC")]
    public GameObject NPCEkspresi1;
    public GameObject NPCEkspresi2;
    public GameObject NPCEkspresi3;
    public GameObject NPCEkspresi4;
    public GameObject NPCEkspresi5;

    [Header ("Dialog Setting")]
    public bool AutoStartDialog;
    public bool AutoFinishDialog;
    public bool Selesai = false;
    public bool NoOpsi = false;
    public bool MouseActive = false;
    public bool MouseActiveSlsai = false;

    [Header ("Dialog Yang Muncul")]
    public bool isJawaban1Dialog = false;
    public bool isJawaban2Dialog = false;
    public bool isJawaban3Dialog = false;

    [Header ("Mulai Dialog")]
    public List<DialogData> mulaiDialog = new List<DialogData>();
    public GameObject JawabanPanel;

    [Header ("Dialog Opsi 1")]
    public List<DialogData> DialogJawaban1 = new List<DialogData>();
    public UnityEvent Opsi1;

    [Header ("Dialog Opsi 2")]
    public List<DialogData> DialogJawaban2 = new List<DialogData>();
    public UnityEvent Opsi2;

    [Header ("Dialog Opsi 3")]
    public List<DialogData> DialogJawaban3 = new List<DialogData>();
    public UnityEvent Opsi3;

    [Header ("Selesai Interaksi")]
    public List<DialogData> DialogAkhir = new List<DialogData>();

    [Header("Event Dialog Setting")]
    public UnityEvent StartDialogEvent;
    public UnityEvent FinishDialogEvent;

    string currentText = "";
    private int currentDialogIndex = 0;
    private Coroutine DialogCoroutine;
    private bool isDialogRunning = false;

    public void StartDialog()
    {
        currentDialogIndex = 0;

        if (Selesai)
        {
            // Jika sudah berinteraksi
            ActivateDialog(DialogAkhir[currentDialogIndex]);
        }
        else
        {
            // Jika belum berinteraksi
            ActivateDialog(mulaiDialog[currentDialogIndex]);
        }

        StartDialogEvent?.Invoke();
    }

    void Start()
    {
        if (AutoStartDialog)
        {
            StartDialog();
        }
        if (MouseActive)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void Update()
    {
        PlayerName = script.Nama;
        if (Input.GetKeyDown(KeyCode.Mouse0) && isDialogRunning)
        {
            if (typewriting && DialogCoroutine != null)
            {
                StopCoroutine(DialogCoroutine);
                dialogText.text = activeDialog;
                typewriting = false;
            }
            else
            {
                typewriting = true;
                NextDialog();
            }
        }
    }

    void NextDialog()
    {
        isDialogRunning = false;
        currentDialogIndex++;
        currentText = "";
        dialogText.text = "";

        if (Selesai) // Selesai
        {
            JawabanPanel.SetActive(false);
            if (currentDialogIndex < DialogAkhir.Count)
            {
                ActivateDialog(DialogAkhir[currentDialogIndex]);
            }
            else if (AutoFinishDialog)
            {
                SetChildStatus(ParentObject, false);
            }
            FinishDialogEvent?.Invoke();
        }
        else if (isJawaban1Dialog && !NoOpsi)
        {
            if (currentDialogIndex < DialogJawaban1.Count)
            {
                ActivateDialog(DialogJawaban1[currentDialogIndex]);
            }

            else
            {
                SetChildStatus(ParentObject, false);
                Opsi1?.Invoke();
                Selesai = true;
            }
        }
        else if (isJawaban2Dialog && !NoOpsi)
        {
            if (currentDialogIndex < DialogJawaban2.Count)
            {
                ActivateDialog(DialogJawaban2[currentDialogIndex]);
            }
            else
            {
                SetChildStatus(ParentObject, false);
                Opsi2?.Invoke();
                Selesai = true;
            }
        }
        else if (isJawaban3Dialog && !NoOpsi)
        {
            JawabanPanel.SetActive(false);
            if (currentDialogIndex < DialogJawaban3.Count)
            {
                ActivateDialog(DialogJawaban3[currentDialogIndex]);
            }
            else
            {
                SetChildStatus(ParentObject, false);
                Opsi3?.Invoke();
                Selesai = true;
            }
        }
        else
        {
            if (currentDialogIndex < mulaiDialog.Count)
            {
                ActivateDialog(mulaiDialog[currentDialogIndex]);
            }
            else
            {
                if (AutoFinishDialog && NoOpsi)
                {
                    SetChildStatus(ParentObject, false);
                    FinishDialogEvent?.Invoke();
                    if(MouseActiveSlsai)
                    {
                        Cursor.lockState = CursorLockMode.Locked;
                        Cursor.visible = false;
                        MouseActive = false;
                    }
                    else
                    {
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                    }
                }
                else
                {
                    JawabanPanel.SetActive(true);
                }
            }
        }
    }

    void ActivateDialog(DialogData dialogData)
    {
        SetChildStatus(ParentObject, true);
        /* PlayerName = targetKunci.NamaPlayer; */
        if (dialogData.character == Character.Player)
        {
            SetPlayerEkspresi(dialogData.player);
            MatikanEkspresiNPC();
            nameText.text = PlayerName;
        }
        else if (dialogData.character == Character.NPC)
        {
            SetNPCEkspresi(dialogData.npc);
            MatikanEkspresiPlayer();
            nameText.text = NPCName;
        }

        string editedString = dialogData.dialog;
        if (dialogData.sebutPlayer)
        {
            editedString += $" {PlayerName}";
        }

        // Bold Kata
        editedString = ApplyBoldFormatting(editedString, dialogData.boldSentences);
        activeDialog = editedString;

        if (typewriting)
        {
            DialogCoroutine = StartCoroutine(TypeText(editedString));
        }
        else
        {
            dialogText.text = editedString;
        }
        isDialogRunning = true;
    }

    // Interaksi Respon NPC
    public void PilihDialogJawaban(int jawaban)
    {
        JawabanPanel.SetActive(false);

        switch (jawaban)
        {
            case 1:
                if (DialogJawaban1.Count > 0)
                {
                    currentDialogIndex = 0;
                    ActivateDialog(DialogJawaban1[currentDialogIndex]);
                    isJawaban1Dialog = true;
                }
                break;
            case 2:
                if (DialogJawaban2.Count > 0)
                {
                    currentDialogIndex = 0;
                    ActivateDialog(DialogJawaban2[currentDialogIndex]);
                    isJawaban2Dialog = true;
                }
                break;
            case 3:
                if (DialogJawaban3.Count > 0)
                {
                    currentDialogIndex = 0;
                    ActivateDialog(DialogJawaban3[currentDialogIndex]);
                    isJawaban3Dialog = true;
                }
                break;
        }
    }

    public void UlangDialog()
    {
        Selesai = false;
        isJawaban1Dialog = false;
        isJawaban2Dialog = false;
        isJawaban3Dialog = false;
    }

    // Untuk Mempertebal Text Penting
    string ApplyBoldFormatting(string dialog, List<string> boldSentences)
    {
        foreach (string sentence in boldSentences)
        {
            dialog = dialog.Replace(sentence, $"<b>{sentence}</b>");
        }
        return dialog;
    }

    // Animasi Dialog
    IEnumerator TypeText(string fullString)
    {
        for (int i = 0; i < fullString.Length; i++)
        {
            currentText += fullString[i];
            dialogText.text = currentText;
            yield return new WaitForSeconds(delay);
        }
        typewriting = false;
    }

    string EditString(string originalString, string targetWord, string replacementWord)
    {
        string[] words = originalString.Split(' ');

        for (int i = 0; i < words.Length; i++)
        {
            if (words[i] == targetWord)
            {
                words[i] = replacementWord;
                break;
            }
        }

        return string.Join(" ", words);
    }

    public void SetChildStatus(GameObject parentObject, bool aValue)
    {
        Transform[] childTransforms = parentObject.GetComponentsInChildren<Transform>(true);

        foreach (Transform childTransform in childTransforms)
        {
            if (childTransform.gameObject != parentObject)
            {
                childTransform.gameObject.SetActive(aValue);
            }
        }
    }

    void SetNPCEkspresi(EkspresiNPC ekspresi)
    {
        NPCEkspresi1.SetActive(false);
        NPCEkspresi2.SetActive(false);
        NPCEkspresi3.SetActive(false);
        NPCEkspresi4.SetActive(false);
        NPCEkspresi5.SetActive(false);

        switch (ekspresi)
        {
            case EkspresiNPC.Ekspresi1:
                NPCEkspresi1.SetActive(true);
                break;
            case EkspresiNPC.Ekspresi2:
                NPCEkspresi2.SetActive(true);
                break;
            case EkspresiNPC.Ekspresi3:
                NPCEkspresi3.SetActive(true);
                break;
            case EkspresiNPC.Ekspresi4:
                NPCEkspresi4.SetActive(true);
                break;
            case EkspresiNPC.Ekspresi5:
                NPCEkspresi5.SetActive(true);
                break;
        }
    }

    void SetPlayerEkspresi(EkspresiPlayer ekspresi)
    {
        PlayerEkspresi1.SetActive(false);
        PlayerEkspresi2.SetActive(false);
        PlayerEkspresi3.SetActive(false);
        PlayerEkspresi4.SetActive(false);
        PlayerEkspresi5.SetActive(false);

        switch (ekspresi)
        {
            case EkspresiPlayer.Ekspresi1:
                PlayerEkspresi1.SetActive(true);
                break;
            case EkspresiPlayer.Ekspresi2:
                PlayerEkspresi2.SetActive(true);
                break;
            case EkspresiPlayer.Ekspresi3:
                PlayerEkspresi3.SetActive(true);
                break;
            case EkspresiPlayer.Ekspresi4:
                PlayerEkspresi4.SetActive(true);
                break;
            case EkspresiPlayer.Ekspresi5:
                PlayerEkspresi5.SetActive(true);
                break;
        }
    }
    void MatikanEkspresiPlayer()
    {
        PlayerEkspresi1.SetActive (false);
        PlayerEkspresi2.SetActive (false);
        PlayerEkspresi3.SetActive (false);
        PlayerEkspresi4.SetActive (false);
        PlayerEkspresi5.SetActive (false);
    }
    void MatikanEkspresiNPC()
    {
        NPCEkspresi1.SetActive(false);
        NPCEkspresi2.SetActive(false);
        NPCEkspresi3.SetActive(false);
        NPCEkspresi4.SetActive(false);
        NPCEkspresi5.SetActive(false);
    }
}
