using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Linq;

public class CharacterClickInteractor : MonoBehaviour, IPointerClickHandler
{
    public event Action OnPanelClicked;
    [SerializeField] GameObject characterInfoPanel;
    [HideInInspector] public bool revealedAlready;
    Image myBorder;

    private void Start()
    {
        myBorder = gameObject.GetComponentsInChildren<Image>().First(x => x.name == "Border");
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        OnPanelClicked?.Invoke();
    }

    public void RevealPaenl()
    {
        characterInfoPanel.SetActive(true);
        revealedAlready = true;
    }

    public void HighlightMyself()
    {
        myBorder.color = Color.yellow;
    }

    public void HintMyself()
    {
        myBorder.color = Color.cyan;
    }

    public void RemoveMyHighlight()
    {
        myBorder.color = Color.black;
    }
}
