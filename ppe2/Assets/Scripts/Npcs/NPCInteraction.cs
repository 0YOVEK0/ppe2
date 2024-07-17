using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public Dialogue dialogue;
    private DialogueManager dialogueManager;
    public float activationDistance = 3f;
    private GameObject player;
    private bool isPlayerNear = false;

    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < activationDistance)
        {
            isPlayerNear = true;
            if (Input.GetKeyDown(KeyCode.E) && !dialogueManager.isDialogueActive)
            {
                dialogueManager.StartDialogue(dialogue);
            }
        }
        else
        {
            isPlayerNear = false;
        }
    }
}
