using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShotTypeFlagToggle : MonoBehaviour, IPointerClickHandler
{
    public Feeder Feeder;
    public Button Button;

    public Feeder.EShotType ShotType;

    public void OnPointerClick(PointerEventData eventData)
    {
        Feeder.Toggle(ShotType);
    }

    private void Update()
    {
        if (Feeder.ShotTypeFilters.HasFlag(ShotType))
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