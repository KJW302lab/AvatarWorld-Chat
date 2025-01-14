using System.Collections;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAvatar : MonoBehaviourPun
{
    [SerializeField] private Text       txtNickname;
    [SerializeField] private Text       txtChat;
    [SerializeField] private GameObject chatBubble;

    private AvatarController _controller;
    private string _nickname;
    private int    _actorNumber;

    private void Awake()
    {
        _controller = GetComponent<AvatarController>();
        
        chatBubble.SetActive(false);
    }

    [PunRPC]
    public void SetNickname(string nickname, int actorNumber)
    {
        _nickname = nickname;
        _actorNumber = actorNumber;
        
        txtNickname.text = nickname;
    }

    public void SetMovable(bool value)
    {
        _controller.canMove = value;
    }

    public bool IsTargetAvatar(int actorNumber) => _actorNumber == actorNumber;

    public void ShowChat(string chat)
    {
        if (_chatCoroutine != null)
            StopCoroutine(_chatCoroutine);

        _chatCoroutine = ChatCoroutine(chat);
        StartCoroutine(_chatCoroutine);
    }

    private IEnumerator _chatCoroutine;
    private IEnumerator ChatCoroutine(string chat)
    {
        chatBubble.SetActive(true);

        txtChat.text = chat;

        yield return new WaitForSeconds(3f);

        txtChat.text = null;

        chatBubble.SetActive(false);
    }
}
