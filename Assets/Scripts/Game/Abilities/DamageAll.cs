using System.Collections;
using System.Linq;
using UnityEngine;


public class DamageAll : Ability
{
    private Save _save;
    private Player _player;

    private int _lvlAbility;
    
    public override void DoAbility(int lvlAbility, Player player)
    {
        _lvlAbility = lvlAbility;
        _player = player;
        
        foreach (GameObject card in GameObject.FindGameObjectsWithTag("Poison"))
        {
            Cards damagedCard = card.GetComponent<Cards>();
            damagedCard.damage -= _lvlAbility;

            if (damagedCard.damage <= 0)
            {
                damagedCard.damage = 0;
                StartCoroutine(CreateCard(damagedCard));
            }
        }
    }
    private IEnumerator CreateCard(Cards damagedCard)
    {
        yield return StartCoroutine(damagedCard.DeathCard());
        Spawner.CreateCard(damagedCard.x, damagedCard.y);
        _player.isMove = false;
    }
}