using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterStatHealthModifierSO : CharacterStatModifierSO
{
    public override void AffectCharacter(GameObject character, float val)
    {
        ControladorDeAtaque player = character.GetComponent<ControladorDeAtaque>();
        if (player != null)
            player.AddHealth((int)val);
    }
    
}