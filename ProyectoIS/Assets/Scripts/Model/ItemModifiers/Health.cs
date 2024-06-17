using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterStatHealthModifierSO : CharacterStatModifierSO
{
    public override void AffectCharacter(GameObject character, float val)
    {
        ControladorDeAtaque currenthealth = character.GetComponent<ControladorDeAtaque>();
        if (currenthealth != null)
            currenthealth.AddHealth((int)val);
    }
}