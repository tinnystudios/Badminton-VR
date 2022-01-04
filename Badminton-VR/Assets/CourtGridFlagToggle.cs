using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CourtGridFlagToggle : MonoBehaviour, IPointerClickHandler
{
    public Feeder Feeder;
    public Button Button;

    public int Index;

    public void OnPointerClick(PointerEventData eventData)
    {
        Feeder.TogglePosition(Index);
    }

    private void Update()
    {
        if (Feeder.PositionFilters.Contains(Index))
        {
            var cb = Button.colors;
            cb.normalColor = Color.white;
            Button.colors = cb;
        }
        else
        {
            var cb = Button.colors;
            cb.normalColor = Color.gray;
            Button.colors = cb;
        }
    }
}