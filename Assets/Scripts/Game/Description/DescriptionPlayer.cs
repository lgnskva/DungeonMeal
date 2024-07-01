public class DescriptionPlayer : Description
{
    private void Start()
    {
        Text = DataController.CurrentCharacter.Description.Replace("{}", DataController.CurrentCharacter.LvlAbility.ToString());
    }
}
