using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class Strength : CharacterStatModifierSO
{
    // Start is called before the first frame update
    public override void AffectCharacter(GameObject character, float val)
    {
        ControladorDeAtaque player = character.GetComponent<ControladorDeAtaque>();
        if (player != null)
            player.addDamage();
            
    }
}
